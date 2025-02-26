using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OdiApp.DataAccessLayer.Extensions;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs.CVEgitim;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs.CVYetenek;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs.ProfilVideo;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.CVDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeBilgileriDTOs;
using OdiApp.EntityLayer.PerformerModels.PerformerCVModels;
using System.Data.SqlTypes;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerCVs;

public class CVDataService : ICVDataService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public CVDataService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<List<CVDataBasicDTO>> CVDataVerileriGetir()
    {
        return _context.CVDatalar
            .Where(c => c.Deger != null) // Eğer null değerler istenmiyorsa
            .GroupBy(c => new { c.AlanKodu, c.Deger })
            .Select(g => new CVDataBasicDTO
            {
                AlanKodu = g.Key.AlanKodu,
                Deger = g.Key.Deger
            })
            .ToList();
    }

    public async Task<PagedData<string>> FiltrelenenPerformerlarGetir(FiltrelenenPerformerlarInputDTO model, ProjeOutputDTO? proje)
    {
        // KullaniciBasic tablosu ile query başlatılıyor
        var query = _context.KullaniciBasic.AsQueryable();

        // RangeFilters (Min-Max aralığı)
        if (model.RangeFilters != null)
        {
            foreach (var filter in model.RangeFilters)
            {
                if (filter == null) continue;
                if (filter.Min == null && filter.Max == null) continue;

                filter.Min ??= int.MinValue;
                filter.Max ??= int.MaxValue;

                query = query.Join(
                    _context.CVler,
                    user => user.KullaniciId,
                    cv => cv.PerformerId,
                    (user, cv) => new { User = user, CV = cv }
                ).Join(
                    _context.CVDatalar,
                    combined => combined.CV.Id,
                    data => data.CVId,
                    (combined, data) => new { combined.User, combined.CV, Data = data }
                ).Where(q =>
                    q.Data.AlanKodu == filter.AlanKodu &&
                    Convert.ToInt32(q.Data.Deger) >= filter.Min &&
                    Convert.ToInt32(q.Data.Deger) <= filter.Max
                ).Select(q => q.User);
            }
        }

        //Yaş Filter
        if (model.YasFilter != null)
        {
            if (model.YasFilter.CastYasinaGoreUygula)
            {
                if (model.YasFilter.MinYas != null)
                {
                    var rawSqlQueryMin = @"
                        SELECT DISTINCT CVId
                        FROM CVDatalar
                        WHERE AlanKodu = 'CBSY'
                        AND TRY_CAST(Deger AS INT) IS NOT NULL
                        AND TRY_CAST(Deger AS INT) >= @MinYas";

                    var parametersMin = new[]
                    {
                        new SqlParameter("@MinYas", model.YasFilter.MinYas.Value)
                    };

                    var filteredCVIdsMin = await _context.CVDatalar
                        .FromSqlRaw(rawSqlQueryMin, parametersMin)
                        .Select(cv => cv.CVId)
                        .ToListAsync();

                    query = query.Join(
                        _context.CVler,
                        user => user.KullaniciId,
                        cv => cv.PerformerId,
                        (user, cv) => new { User = user, CV = cv }
                    ).Where(q =>
                        filteredCVIdsMin.Contains(q.CV.Id)
                    ).Select(q => q.User);
                }

                if (model.YasFilter.MaxYas != null)
                {
                    var rawSqlQueryMax = @"
                        SELECT DISTINCT CVId
                        FROM CVDatalar
                        WHERE AlanKodu = 'CBSY'
                        AND TRY_CAST(Deger2 AS INT) IS NOT NULL
                        AND TRY_CAST(Deger2 AS INT) <= @MaxYas";

                    var parametersMax = new[]
                    {
                        new SqlParameter("@MaxYas", model.YasFilter.MaxYas.Value)
                    };

                    var filteredCVIdsMax = await _context.CVDatalar
                        .FromSqlRaw(rawSqlQueryMax, parametersMax)
                        .Select(cv => cv.CVId)
                        .ToListAsync();

                    query = query.Join(
                        _context.CVler,
                        user => user.KullaniciId,
                        cv => cv.PerformerId,
                        (user, cv) => new { User = user, CV = cv }
                    ).Where(q =>
                        filteredCVIdsMax.Contains(q.CV.Id)
                    ).Select(q => q.User);
                }
            }
            else
            {
                DateTime? maxDogumTarihi = model.YasFilter?.MinYas != null
                    ? DateTime.Now.AddYears(-model.YasFilter.MinYas.Value)
                    : SqlDateTime.MaxValue.Value;

                DateTime? minDogumTarihi = model.YasFilter?.MaxYas != null
                    ? DateTime.Now.AddYears(-model.YasFilter.MaxYas.Value)
                    : SqlDateTime.MinValue.Value;

                var rawSqlQuery = @"
                    SELECT DISTINCT CVId
                    FROM CVDatalar
                    WHERE AlanKodu = 'DOGT'
                    AND ISDATE(Deger) = 1
                    AND CAST(Deger AS DATETIME) >= @MinDate
                    AND CAST(Deger AS DATETIME) <= @MaxDate";

                var parameters = new[]
                {
                    new SqlParameter("@MinDate", minDogumTarihi),
                    new SqlParameter("@MaxDate", maxDogumTarihi)
                };

                var filteredCVIds = await _context.CVDatalar
                    .FromSqlRaw(rawSqlQuery, parameters)
                    .Select(cv => cv.CVId)
                    .ToListAsync();

                query = query.Join(
                    _context.CVler,
                    user => user.KullaniciId,
                    cv => cv.PerformerId,
                    (user, cv) => new { User = user, CV = cv }
                ).Where(q =>
                    filteredCVIds.Contains(q.CV.Id)
                ).Select(q => q.User);
            }
        }

        // MultiSelectFilters
        if (model.MultiSelectFilters != null)
        {
            foreach (var filter in model.MultiSelectFilters)
            {
                if (filter == null || filter?.Values?.Any() == false)
                    continue;

                query = query.Join(
                    _context.CVler,
                    user => user.KullaniciId,
                    cv => cv.PerformerId,
                    (user, cv) => new { User = user, CV = cv }
                ).Join(
                    _context.CVDatalar,
                    combined => combined.CV.Id,
                    data => data.CVId,
                    (combined, data) => new { combined.User, combined.CV, Data = data }
                ).Where(q =>
                    q.Data.AlanKodu == filter.AlanKodu &&
                    filter.Values.Contains(q.Data.Deger)
                ).Select(q => q.User);
            }
        }

        // Deneyim filtreleri
        if (model.DeneyimKoduList != null && model.DeneyimKoduList.Any())
        {
            query = query.Join(
                _context.CVler,
                user => user.KullaniciId,
                cv => cv.PerformerId,
                (user, cv) => new { User = user, CV = cv }
            ).Join(
                _context.CVDeneyimler,
                combined => combined.CV.Id,
                deneyim => deneyim.CVId,
                (combined, deneyim) => new { combined.User, combined.CV, Deneyim = deneyim }
            ).Where(q => model.DeneyimKoduList.Contains(q.Deneyim.DeneyimKodu))
            .Select(q => q.User);
        }

        // Eğitim filtreleri
        if (model.EgitimTipiIdList != null && model.EgitimTipiIdList.Any())
        {
            query = query.Join(
                _context.CVler,
                user => user.KullaniciId,
                cv => cv.PerformerId,
                (user, cv) => new { User = user, CV = cv }
            ).Join(
                _context.CVEgitimler,
                combined => combined.CV.Id,
                egitim => egitim.CVId,
                (combined, egitim) => new { combined.User, combined.CV, Egitim = egitim }
            ).Where(q => model.EgitimTipiIdList.Contains(q.Egitim.EgitimTipiId))
            .Select(q => q.User);
        }

        // Yetenek filtreleri
        if (model.YetenekFilters != null)
        {
            foreach (var filter in model.YetenekFilters)
            {
                query = query.Join(
                    _context.CVler,
                    user => user.KullaniciId,
                    cv => cv.PerformerId,
                    (user, cv) => new { User = user, CV = cv }
                ).Join(
                    _context.CVYetenekleri,
                    combined => combined.CV.Id,
                    yetenek => yetenek.CVId,
                    (combined, yetenek) => new { combined.User, combined.CV, Yetenek = yetenek }
                ).Where(q =>
                    q.Yetenek.YetenekKodu == filter.YetenekKodu && q.Yetenek.Derece >= filter.MinDerece
                ).Select(q => q.User);
            }
        }

        // PerformerEtiketKoduList Filtrelemesi
        if (model.PerformerEtiketKoduList != null && model.PerformerEtiketKoduList.Any())
        {
            query = query.Join(
                _context.YetenekTemsilcisiPerformerEtiketleri,
                user => user.KullaniciId,
                etiket => etiket.PerformerId,
                (user, etiket) => new { User = user, Etiket = etiket })
            .Where(q => model.PerformerEtiketKoduList.Contains(q.User.KullaniciId))
            .Select(q => q.User);
        }

        // KayitTuruKoduList Filtrelemesi
        if (model.KayitTuruKoduList != null && model.KayitTuruKoduList.Any())
        {
            var sqlQuery = @"
            SELECT KullaniciId 
            FROM KullaniciBasic 
            WHERE " + string.Join(" OR ", model.KayitTuruKoduList.Select(k => $"KayitTuruKodu LIKE '%{k}%'"));

            var kullaniciList = await _context.KullaniciBasic
                .FromSqlRaw(sqlQuery)
                .Select(user => user.KullaniciId)
                .ToListAsync();

            query = query.Where(q => kullaniciList.Contains(q.KullaniciId));
        }

        // PuanFilter Filtrelemesi
        if (model.PuanFilter != null)
        {
            var puanFilter = model.PuanFilter;

            // Genel Puan Filtreleme
            if (puanFilter.GenelPuanMin.HasValue && puanFilter.GenelPuanMax.HasValue)
            {
                int genelPuanMin = puanFilter.GenelPuanMin ?? int.MinValue;
                int genelPuanMax = puanFilter.GenelPuanMax ?? int.MaxValue;

                query = query.Join(
                    _context.PerformerPuanlari,
                    user => user.KullaniciId,
                    puan => puan.PerformerId,
                    (user, puan) => new { User = user, Puan = puan }
                ).Where(joined =>

                                (joined.Puan.İlgiCekicilikPuani + joined.Puan.YetenekPuani + joined.Puan.BasariPuani) / 3.0 >= puanFilter.GenelPuanMin
                             &&

                                (joined.Puan.İlgiCekicilikPuani + joined.Puan.YetenekPuani + joined.Puan.BasariPuani) / 3.0 <= puanFilter.GenelPuanMax

                ).Select(joined => joined.User);
            }

            // İlgi Çekicilik Puan Filtreleme
            if (puanFilter.İlgiCekicilikPuanMin.HasValue && puanFilter.İlgiCekicilikPuanMax.HasValue)
            {
                int ilgiCekicilikPuaniMin = puanFilter.İlgiCekicilikPuanMin ?? int.MinValue;
                int ilgiCekicilikPuaniMax = puanFilter.İlgiCekicilikPuanMax ?? int.MaxValue;

                query = query.Join(
                    _context.PerformerPuanlari,
                    user => user.KullaniciId,
                    puan => puan.PerformerId,
                    (user, puan) => new { User = user, Puan = puan }
                ).Where(joined =>
                    joined.Puan.İlgiCekicilikPuani >= puanFilter.İlgiCekicilikPuanMin &&
                    joined.Puan.İlgiCekicilikPuani <= puanFilter.İlgiCekicilikPuanMax
                ).Select(joined => joined.User);
            }

            // Yetenek Puanı Filtreleme
            if (puanFilter.YetenekPuanMin.HasValue && puanFilter.YetenekPuanMax.HasValue)
            {
                int yetenekPuaniMin = puanFilter.YetenekPuanMin ?? int.MinValue;
                int yetenekPuaniMax = puanFilter.YetenekPuanMax ?? int.MaxValue;

                query = query.Join(
                    _context.PerformerPuanlari,
                    user => user.KullaniciId,
                    puan => puan.PerformerId,
                    (user, puan) => new { User = user, Puan = puan }
                ).Where(joined =>
                    joined.Puan.YetenekPuani >= puanFilter.YetenekPuanMin &&
                    joined.Puan.YetenekPuani <= puanFilter.YetenekPuanMax
                ).Select(joined => joined.User);
            }

            // Başarı Puanı Filtreleme
            if (puanFilter.BasariPuanMin.HasValue && puanFilter.BasariPuanMax.HasValue)
            {
                int basariPuaniMin = puanFilter.BasariPuanMin ?? int.MinValue;
                int basariPuaniMax = puanFilter.BasariPuanMax ?? int.MaxValue;

                query = query.Join(
                    _context.PerformerPuanlari,
                    user => user.KullaniciId,
                    puan => puan.PerformerId,
                    (user, puan) => new { User = user, Puan = puan }
                ).Where(joined =>
                    joined.Puan.BasariPuani >= puanFilter.BasariPuanMin &&
                    joined.Puan.BasariPuani <= puanFilter.BasariPuanMax
                ).Select(joined => joined.User);
            }

        }

        // YasakliKelimelerFilter Filtrelemesi
        if (model.YineDeGosterFilter?.Contains(YineDeGosterFilterType.YasakliKelimeyeTakilanlar) != true)
        {
            if (model.YasakliKelimelerFilter != null && model.YasakliKelimelerFilter.ReklamMi)
            {
                query = query.Join(
                    _context.CVDeneyimler.Join(
                        _context.CVDeneyimDetaylari.Where(d => d.FormAlaniKodu == "SKTL"),
                        deneyim => deneyim.Id,
                        detay => detay.CVDeneyimId,
                        (deneyim, detay) => new { Deneyim = deneyim, Detay = detay }
                    ),
                    user => user.KullaniciId,
                    deneyimDetay => deneyimDetay.Deneyim.CVId,
                    (user, deneyimDetay) => new { User = user, deneyimDetay.Detay }
                ).Where(joined => !model.YasakliKelimelerFilter.YasakliKelimelerList.Contains(joined.Detay.Deger))
                .Select(joined => joined.User);
            }
        }

        //ProfilTipiFilter Filtrelemesi
        if (model.ProfilTipiFilter != null)
        {
            if (model.ProfilTipiFilter.Contains(ProfilTipiFilterType.EksikProfil))
            {
                query = query.Where(q => !_context.ProfilOnaylari.AsNoTracking()
                    .Select(s => s.PerformerId)
                    .Contains(q.KullaniciId));
            }

            if (model.ProfilTipiFilter.Contains(ProfilTipiFilterType.OnayliProfil))
            {
                query = query.Where(q => _context.ProfilOnaylari.AsNoTracking().Any(a => a.PerformerId == q.KullaniciId && !a.Aktif && a.Onay));
            }

            //TODO Premium içni filtreleme eklenecek
        }

        //Yetenek Temsilcisi Filter
        if (model.YetenekTemsilcisiFilter?.Any() == true)
        {
            query = query.Join(
                _context.PerformerYetenekTemsilcisi,
                user => user.KullaniciId,
                performerYetenekTemsilcisi => performerYetenekTemsilcisi.PerformerId,
                (user, performerYetenekTemsilcisi) => new { User = user, PerformerYetenekTemsilcisi = performerYetenekTemsilcisi }
            )
            .Where(joined => model.YetenekTemsilcisiFilter.Contains(joined.PerformerYetenekTemsilcisi.MenajerId))
            .Select(joined => joined.User);
        }

        //Icerige Gore Filter
        if (model.IcerigeGoreFilter?.Any() == true)
        {
            if (model.IcerigeGoreFilter.Contains(IcerigeGoreFilterType.TanitimVideosuOlanlar))
            {
                query = query.Where(x => _context.ProfilVideolari.AsNoTracking().Any(pv => pv.PerformerId == x.KullaniciId && pv.VideoTipiKodu == "TNTM"));
            }

            if (model.IcerigeGoreFilter.Contains(IcerigeGoreFilterType.MimikVideosuOlanlar))
            {
                query = query.Where(x => _context.ProfilVideolari.AsNoTracking().Any(pv => pv.PerformerId == x.KullaniciId && pv.VideoTipiKodu == "MMIK"));
            }

            if (model.IcerigeGoreFilter.Contains(IcerigeGoreFilterType.ShowreeliOlanlar))
            {
                query = query.Where(x => _context.ProfilVideolari.AsNoTracking().Any(pv => pv.PerformerId == x.KullaniciId && pv.VideoTipiKodu == "SRVR"));
            }

            if (model.IcerigeGoreFilter.Contains(IcerigeGoreFilterType.PortreKolajiOlanlar))
            {
                query = query.Where(x => _context.FotoAlbumleri.AsNoTracking().Any(pv => pv.KullaniciId == x.KullaniciId && pv.FotoAlbumTipiId == 6));
            }
        }

        if (proje != null)
        {
            // "Müsait Olmayanlar" filtresi kontrolü
            if (model.YineDeGosterFilter?.Contains(YineDeGosterFilterType.MusaitOlmayanlar) != true &&
                proje.CekimBaslangicTarihi != null && proje.CekimBitisTarihi != null)
            {
                query = query.Where(user => !_context.PerformerTakvim
                    .Any(takvim =>
                        takvim.PerformerId == user.KullaniciId &&
                        takvim.BaslangicTarihi <= proje.CekimBitisTarihi &&
                        takvim.BitisTarihi >= proje.CekimBaslangicTarihi
                    )
                );
            }

            // "Katılım Şehrine Uymayanlar" filtresi kontrolü
            if (model.YineDeGosterFilter?.Contains(YineDeGosterFilterType.KatilimSehrineUymayanlar) != true &&
                !string.IsNullOrEmpty(proje.Sehirler))
            {
                var projeSehirList = proje.Sehirler
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .ToList();

                var sqlQuery = @"
                    SELECT DISTINCT cv.PerformerId
                    FROM CVler cv
                    JOIN CVDatalar cd ON cv.Id = cd.CVId
                    WHERE cd.AlanKodu = 'YSHR' AND (" +
                string.Join(" OR ", projeSehirList.Select(sehir => $"cd.Deger LIKE '%{sehir}%'")) + ")";

                var uygunPerformers = _context.CVler
                    .FromSqlRaw(sqlQuery)
                    .Select(cv => cv.PerformerId)
                    .ToList();

                query = query.Where(user => uygunPerformers.Contains(user.KullaniciId));
            }
        }

        // Performers Id list
        var performerIdsQuery = query.Select(user => user.KullaniciId).Distinct();

        // Pagination
        var pagedResult = await performerIdsQuery.PaginateAsync(model.Page, model.Limit);

        return pagedResult;
    }

    public async Task<CV> CVGetir(string performerId)
    {
        return await _context.CVler.Include(x => x.DataList).FirstOrDefaultAsync(x => x.PerformerId == performerId);
    }

    public async Task<bool> CVVarmi(string performerId)
    {
        return await _context.CVler.AnyAsync(p => p.PerformerId == performerId);
    }

    public async Task<CV> YeniCV(CV cv)
    {
        await _context.CVler.AddAsync(cv);
        await _context.SaveChangesAsync();
        return cv;
    }

    public async Task<CV> CVGuncelle(CV cv)
    {
        _context.CVler.Update(cv);
        await _context.SaveChangesAsync();
        return cv;
    }

    #region CV Deneyim
    public async Task<CVDeneyim> YeniCVDeneyim(CVDeneyim deneyim)
    {
        await _context.CVDeneyimler.AddAsync(deneyim);
        await _context.SaveChangesAsync();
        return deneyim;
    }

    public async Task<bool> CVDeneyimSil(string CVDeneyimId)
    {
        CVDeneyim deneyim = await _context.CVDeneyimler.FirstOrDefaultAsync(x => x.Id == CVDeneyimId);
        if (deneyim == null) return false;

        _context.Remove(deneyim);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<CVYetenekOutputDTO>> CVYetenekListesi(string cvId, int dilId)
    {
        string query = @"
                SELECT 
                    cvy.CVId,
                    cvy.Id AS CVYetenekId,
                    cvy.YetenekTipiKodu,
                    yt.Tip AS YetenekTipi,
                    cvy.YetenekKodu,
                    y.YetenekAdi AS Yetenek,
                    cvy.Derece,
                    cvyv.Video AS VideoUrl,
                    cvyv.Tags AS VideoTags,
                    cvyv.Id AS VideoId
                FROM 
                    CVYetenekleri cvy
                LEFT JOIN 
                    YetenekTipleri yt ON yt.YetenekTipiKodu = cvy.YetenekTipiKodu
                LEFT JOIN 
                    Yetenekler y ON y.YetenekKodu = cvy.YetenekKodu
                LEFT JOIN 
                    CVYetenekVideolari cvyv ON cvyv.CVYetenekId = cvy.Id
                WHERE 
                    cvy.CVId = @CVId 
                    AND yt.DilId = @DilId 
                    AND y.DilId = @DilId
                ORDER BY 
                    YetenekTipi";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var result = await connection.QueryAsync<CVYetenekOutputDTO>(query, new { CVId = cvId, DilId = dilId });

            return result.ToList();
        }
    }
    public async Task<CVYetenek> YeniCVYetenek(CVYetenek yetenek)
    {
        await _context.CVYetenekleri.AddAsync(yetenek);
        await _context.SaveChangesAsync();
        return yetenek;
    }
    public async Task<bool> CVYetenekSil(string cvYetenekId)
    {
        CVYetenek yetenek = await _context.CVYetenekleri.FirstOrDefaultAsync(x => x.Id == cvYetenekId);
        if (yetenek == null) return false;

        _context.Remove(yetenek);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<CVYetenekVideo> YeniCVYetenekVideosu(CVYetenekVideo cvYetenekVideo)
    {
        await _context.CVYetenekVideolari.AddAsync(cvYetenekVideo);
        await _context.SaveChangesAsync();
        return cvYetenekVideo;
    }
    public async Task<bool> CVYetenekVideosuSil(string cvYetenekVideosuId)
    {
        CVYetenekVideo video = await _context.CVYetenekVideolari.FirstOrDefaultAsync(x => x.Id == cvYetenekVideosuId);
        if (video == null) return false;

        _context.Remove(video);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> CheckCVYetenekVideosu(string cvYetenekId)
    {
        return await _context.CVYetenekVideolari.AnyAsync(x => x.CVYetenekId == cvYetenekId);
    }


    #endregion

    #region CV Egitim
    public async Task<CVEgitim> YeniCVEgitim(CVEgitim cv)
    {
        await _context.CVEgitimler.AddAsync(cv);
        await _context.SaveChangesAsync();
        return cv;
    }

    public async Task<bool> CVEgitimSil(string cvEgitimId)
    {
        CVEgitim egitim = await _context?.CVEgitimler.FirstOrDefaultAsync(x => x.Id == cvEgitimId);
        if (egitim == null) return false;

        _context.Remove(egitim);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<CVEgitimOutputDTO>> CVEgitimListesi(string cvId)
    {
        string query = @"Select cve.Id as CVEgitimId,cve.CVId, cve.EgitimTipiId,et.Tip as EgitimTipi, cve.OkulId, ok.OkulAdi , cve.BolumId, ob.Bolum, cve.Yil From CVEgitimler cve
                            left Join EgitimTipleri et on  et.Id=cve.EgitimTipiId
                            left Join Okullar ok on  ok.Id=cve.OkulId
                            left Join OkulBolumler ob on ob.Id=cve.BolumId where cve.CVId=@CVId";
        var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        var result = await connection.QueryAsync<CVEgitimOutputDTO>(query, new { CVId = cvId });
        return result.ToList();
    }


    #endregion

    #region Profil Videosu

    public async Task<ProfilVideo> YeniProfilVideosu(ProfilVideo video)
    {
        await _context.AddAsync(video);
        await _context.SaveChangesAsync();
        return video;
    }

    public async Task<ProfilVideo> ProfilVideosuGuncelle(ProfilVideo video)
    {
        _context.Update(video);
        await _context.SaveChangesAsync();
        return video;
    }

    public async Task<bool> ProfilVideosuSil(string profilVideoId)
    {
        ProfilVideo? video = await _context.ProfilVideolari.FirstOrDefaultAsync(x => x.Id == profilVideoId);
        if (video == null) return false;

        _context.Remove(video);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ProfilVideosuSil(ProfilVideo model)
    {
        _context.Remove(model);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<ProfilVideoOutputDTO>> ProfilVideolariListesi(string performerId)
    {
        string query = @"
                SELECT 
                    pv.Id AS ProfilVideoId,
                    pv.PerformerId,
                    pv.Dil,
                    pv.VideoTipiKodu,
                    pv.VideoURL,
                    pv.VideoTags 
                FROM 
                    ProfilVideolari pv
                WHERE 
                    pv.PerformerId = @PerformerId";

        var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        var result = await connection.QueryAsync<ProfilVideoOutputDTO>(query, new { PerformerId = performerId });
        return result.ToList();
    }

    public async Task<ProfilVideo> ProfilVideoGetir(string profilVideoId)
    {
        return await _context.ProfilVideolari.FirstOrDefaultAsync(x => x.Id == profilVideoId);
    }

    public async Task<List<ProfilVideoTipiOutputDTO>> VideoTipiListesi(int dilId)
    {
        string query = @"
                SELECT 
                    TipAdi AS VideoTipi, 
                    TipKodu AS VideoTipiKodu,
                    Sira,
                    NormalVideoLimit,
                    PremiumVideoLimit,
                    OnerilenEtiketler
                FROM 
                    VideoTipleri 
                WHERE 
                    DilId = @DilId 
                ORDER BY 
                    Sira";

        var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        var result = await connection.QueryAsync(query, new { DilId = dilId });

        var outputList = result.Select(row => new ProfilVideoTipiOutputDTO
        {
            VideoTipi = row.VideoTipi,
            VideoTipiKodu = row.VideoTipiKodu,
            Sira = row.Sira,
            NormalVideoLimit = row.NormalVideoLimit,
            PremiumVideoLimit = row.PremiumVideoLimit,
            OnerilenEtiketler = (row.OnerilenEtiketler as string)?.Split(',').Select(tag => tag.Trim()).ToList() ?? new List<string>()
        }).ToList();

        return outputList;
    }

    #endregion
}