using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OdiApp.DataAccessLayer.Extensions;
using OdiApp.DTOs.PerformerDTOs.YetenekTemsilcisiDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.YetenekTemsilcisiDTOs;
using OdiApp.EntityLayer.PerformerModels.YetenekTemsilcisiModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.YetenekTemsilcisiDataServices;

public class YetenekTemsilcisiDataService : IYetenekTemsilcisiDataService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public YetenekTemsilcisiDataService(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<PagedData<string>> PerformerListesi(PerformerListesiInputDTO model, List<string>? aktifKullaniciIdList)
    {
        IQueryable<PerformerYetenekTemsilcisi> query = _dbContext.PerformerYetenekTemsilcisi.AsNoTracking().Where(q => q.MenajerId == model.YetenekTemsilcisiId).AsQueryable();

        switch (model.PerformerListelemeTipi)
        {
            case PerformerListelemeTipi.Hepsi:
                //Burada ayrıca bir filtrelemeye gerek yok. Doğrudan menajerin kullanıcıları gelecek.
                break;
            case PerformerListelemeTipi.Onaylanan:

                query = query.Join(
                    _dbContext.ProfilOnaylari,
                   performerYetenekTemsilcisi => performerYetenekTemsilcisi.PerformerId,
                   profilOnay => profilOnay.PerformerId,
                   (performerYetenekTemsilcisi, profilOnay) => new { PerformerYetenekTemsilcisi = performerYetenekTemsilcisi, ProfilOnay = profilOnay }
                   ).Where(
                    x => x.ProfilOnay != null
                         && x.ProfilOnay.YetenekTemsilcisiId == model.YetenekTemsilcisiId
                         && x.ProfilOnay.Aktif == false
                         && x.ProfilOnay.Onay == true
                   ).Select(q => q.PerformerYetenekTemsilcisi);

                break;
            case PerformerListelemeTipi.OnaylananPasif:

                //Onaylanan ile neredeyse aynı, tek fark aktif performer id list ilavesi
                query = query.Join(
                    _dbContext.ProfilOnaylari,
                   performerYetenekTemsilcisi => performerYetenekTemsilcisi.PerformerId,
                   profilOnay => profilOnay.PerformerId,
                   (performerYetenekTemsilcisi, profilOnay) => new { PerformerYetenekTemsilcisi = performerYetenekTemsilcisi, ProfilOnay = profilOnay }
                   ).Where(
                    x => x.ProfilOnay != null
                         && x.ProfilOnay.YetenekTemsilcisiId == model.YetenekTemsilcisiId
                         && x.ProfilOnay.Aktif == false
                         && x.ProfilOnay.Onay == true
                         && (aktifKullaniciIdList == null ? true : !aktifKullaniciIdList.Contains(x.PerformerYetenekTemsilcisi.PerformerId))
                   ).Select(q => q.PerformerYetenekTemsilcisi);

                break;
            case PerformerListelemeTipi.DegisiklikYapan:

                query = query.Join(
                    _dbContext.MenajerPerformerGuncellenenAlanlari,
                    performerYetenekTemsilcisi => performerYetenekTemsilcisi.PerformerId,
                    menajerPerformerGuncellenenAlanlari => menajerPerformerGuncellenenAlanlari.PerformerId,
                    (performerYetenekTemsilcisi, menajerPerformerGuncellenenAlanlari) => new { PerformerYetenekTemsilcisi = performerYetenekTemsilcisi, MenajerPerformerGuncellenenAlanlari = menajerPerformerGuncellenenAlanlari }
                    ).Where(x => x.MenajerPerformerGuncellenenAlanlari != null
                            && x.MenajerPerformerGuncellenenAlanlari.MenajerId == model.YetenekTemsilcisiId
                            && x.MenajerPerformerGuncellenenAlanlari.MenajerGordu == false
                    ).Select(q => q.PerformerYetenekTemsilcisi);

                break;
            case PerformerListelemeTipi.OnayBekleyen:

                query = query.Join(
                  _dbContext.ProfilOnaylari,
                 performerYetenekTemsilcisi => performerYetenekTemsilcisi.PerformerId,
                 profilOnay => profilOnay.PerformerId,
                 (performerYetenekTemsilcisi, profilOnay) => new { PerformerYetenekTemsilcisi = performerYetenekTemsilcisi, ProfilOnay = profilOnay }
                 ).Where(
                  x => x.ProfilOnay != null
                       && x.ProfilOnay.YetenekTemsilcisiId == model.YetenekTemsilcisiId
                       && x.ProfilOnay.Aktif == true
                 ).Select(q => q.PerformerYetenekTemsilcisi);

                break;
            case PerformerListelemeTipi.Reddedilen:

                query = query.Join(
                   _dbContext.ProfilOnaylari,
                  performerYetenekTemsilcisi => performerYetenekTemsilcisi.PerformerId,
                  profilOnay => profilOnay.PerformerId,
                  (performerYetenekTemsilcisi, profilOnay) => new { PerformerYetenekTemsilcisi = performerYetenekTemsilcisi, ProfilOnay = profilOnay }
                  ).Where(
                   x => x.ProfilOnay != null
                        && x.ProfilOnay.YetenekTemsilcisiId == model.YetenekTemsilcisiId
                        && x.ProfilOnay.Aktif == false
                        && x.ProfilOnay.Red == true
                        && x.ProfilOnay.EklenmeTarihi == _dbContext.ProfilOnaylari
                           .Where(p => p.PerformerId == x.PerformerYetenekTemsilcisi.PerformerId
                                    && p.Red == true
                                    && p.Aktif == false)
                           .Max(p => p.EklenmeTarihi)
                        && !_dbContext.ProfilOnaylari.Any(p =>
                             p.PerformerId == x.PerformerYetenekTemsilcisi.PerformerId &&
                             p.EklenmeTarihi > x.ProfilOnay.EklenmeTarihi &&
                             p.Aktif == false)
                  ).Select(q => q.PerformerYetenekTemsilcisi);

                break;
            case PerformerListelemeTipi.EksikProfil:

                query = query.Where(q =>
                                    !_dbContext.ProfilOnaylari.AsNoTracking()
                                        .Select(po => po.PerformerId)
                                        .Contains(q.PerformerId));

                break;
            case PerformerListelemeTipi.Dondurulan:

                query = query.Where(q =>
                                   _dbContext.PerformerDondurulanlar.AsNoTracking()
                                       .Select(x => x.DondurulanPerformerId)
                                       .Contains(q.PerformerId));

                break;
            case PerformerListelemeTipi.Engellenen:

                query = query.Where(q =>
                                  _dbContext.PerformerEngellenenler.AsNoTracking()
                                      .Select(x => x.EngellenenPerformerId)
                                      .Contains(q.PerformerId));

                break;
            case PerformerListelemeTipi.Sozlesmeliler:

                query = query.Where(q =>
                                  _dbContext.PerformerMenajerSozlesmeleri.AsNoTracking()
                                      .Where(x => x.MenajerId == model.YetenekTemsilcisiId)
                                      .Select(x => x.PerformerId)
                                      .Contains(q.PerformerId));

                break;
            default:
                return new(); //TODO: Buna gerek olmayabilir.
        }

        //Not: Sıralamalar verilmediği için varsayılan olarak ortalama puan üzerinden sıralama yapılır.
        query = query.Select(performer => new
        {
            Performer = performer,
            OrtalamaPuan = (double?)_dbContext.PerformerPuanlari
            .Where(puan => puan.PerformerId == performer.PerformerId)
            .Sum(puan => puan.İlgiCekicilikPuani + puan.YetenekPuani + puan.BasariPuani) /
            (3 * _dbContext.PerformerPuanlari.Count(puan => puan.PerformerId == performer.PerformerId))
            ?? 0
        })
        .Join(
            _dbContext.KullaniciBasic,
            performerYetenekTemsilcisi => performerYetenekTemsilcisi.Performer.PerformerId,
            kullaniciBasic => kullaniciBasic.KullaniciId,
            (performerYetenekTemsilcisi, kullaniciBasic) => new
            {
                performerYetenekTemsilcisi.Performer,
                performerYetenekTemsilcisi.OrtalamaPuan,
                Kullanici = kullaniciBasic
            }
        )
        .OrderByDescending(x => x.OrtalamaPuan)
        .ThenByDescending(x => x.Kullanici.EklenmeTarihi)
        .Select(x => x.Performer);

        var performerIdsQuery = query.Select(performerYetenekTemsilcisi => performerYetenekTemsilcisi.PerformerId).Distinct();

        var pagedResult = await performerIdsQuery.PaginateAsync(model.Page, model.Limit);

        return pagedResult;
    }

    public async Task<PerformerListesiSayilariOutputDTO> PerformerListesiSayilari(string menajerId, List<string>? aktifKullaniciIdList)
    {
        aktifKullaniciIdList ??= new List<string>();

        var performerData = await _dbContext.PerformerYetenekTemsilcisi
            .AsNoTracking()
            .Where(pyt => pyt.MenajerId == menajerId) // Yetenek temsilcisinin performerları
            .Select(pyt => new
            {
                PerformerYetenekTemsilcisi = pyt,
                ProfilOnaylari = _dbContext.ProfilOnaylari
                    .AsNoTracking()
                    .Where(po => po.PerformerId == pyt.PerformerId)
                    .OrderByDescending(po => po.EklenmeTarihi) // Son güncel kayıt
                    .FirstOrDefault(),
                DegisiklikVarMi = _dbContext.MenajerPerformerGuncellenenAlanlari
                    .AsNoTracking()
                    .Any(mpg => mpg.PerformerId == pyt.PerformerId && (!mpg.MenajerGordu ?? false)),
                DondurulmusMu = _dbContext.PerformerDondurulanlar
                    .AsNoTracking()
                    .Any(pd => pd.DondurulanPerformerId == pyt.PerformerId),
                EngellenmisMi = _dbContext.PerformerEngellenenler
                    .AsNoTracking()
                    .Any(pe => pe.EngellenenPerformerId == pyt.PerformerId),
                SozlesmesiVarMi = _dbContext.PerformerMenajerSozlesmeleri
                    .AsNoTracking()
                    .Any(s => s.PerformerId == pyt.PerformerId && s.MenajerId == menajerId)
            })
            .ToListAsync(); // Veriyi al ve bellekte işlemleri uygula.

        var result = new PerformerListesiSayilariOutputDTO
        {
            TumPerformerSayisi = performerData.Count,
            OnaylananPerformerSayisi = performerData.Count(x => x.ProfilOnaylari != null && x.ProfilOnaylari.Aktif == false && x.ProfilOnaylari.Onay == true),
            OnaylananPasifPerformerSayisi = performerData.Count(x => x.ProfilOnaylari != null && x.ProfilOnaylari.Aktif == false && x.ProfilOnaylari.Onay == true &&
                                                                     !aktifKullaniciIdList.Contains(x.PerformerYetenekTemsilcisi.PerformerId)),
            DegisiklikYapanPerformerSayisi = performerData.Count(x => x.DegisiklikVarMi),
            OnayBekleyenPerformerSayisi = performerData.Count(x => x.ProfilOnaylari != null && x.ProfilOnaylari.Aktif == true),
            ReddedilenPerformerSayisi = performerData.Count(x => x.ProfilOnaylari != null && x.ProfilOnaylari.Aktif == false && x.ProfilOnaylari.Red == true),
            EksikProfilPerformerSayisi = performerData.Count(x => x.ProfilOnaylari == null),
            DondurulanPerformerSayisi = performerData.Count(x => x.DondurulmusMu),
            EngellenenPerformerSayisi = performerData.Count(x => x.EngellenmisMi),
            SozlesmeliPerformerSayisi = performerData.Count(x => x.SozlesmesiVarMi)
        };

        return result;
    }

    public async Task<PerformerYetenekTemsilcisi> YeniPerformerYetenekTemsilcisi(PerformerYetenekTemsilcisi model)
    {
        await _dbContext.PerformerYetenekTemsilcisi.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<PerformerMenajerListItemOutputDTO> PerformerMenajerGetir(string performerId)
    {
        var odiKullaniciId = "ODI";

        var query = @"
                SELECT
                  p.PerformerId,
                  p.MenajerId,
                  CASE WHEN p.MenajerId = @OdiKullaniciId THEN 'ODI'
                       ELSE (SELECT kb.KullaniciAdSoyad
                             FROM KullaniciBasic kb
                             WHERE kb.KullaniciId = p.MenajerId)
                  END AS MenajerAdSoyad
                FROM PerformerYetenekTemsilcisi p
                WHERE p.PerformerId = @PerformerId AND p.Aktif = 1;
            ";

        var parameters = new DynamicParameters();
        parameters.Add("@PerformerId", performerId);
        parameters.Add("@OdiKullaniciId", odiKullaniciId);

        var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);

        var result = await connection.QueryAsync<PerformerMenajerListItemOutputDTO>(query, parameters);

        return result.FirstOrDefault();
    }

    public async Task<List<PerformerMenajerListItemOutputDTO>> PerformerMenajerListesiGetir(List<string> performerIdList)
    {
        var odiKullaniciId = "ODI";

        var query = @"
                SELECT p.PerformerId,
                       p.MenajerId,
                       CASE WHEN p.MenajerId = @OdiKullaniciId THEN 'ODI'
                            ELSE (SELECT kb.KullaniciAdSoyad
                                  FROM KullaniciBasic kb
                                  WHERE kb.KullaniciId = p.MenajerId)
                       END AS MenajerAdSoyad
                FROM PerformerYetenekTemsilcisi p
                WHERE p.PerformerId IN @PerformerIdList AND p.Aktif = 1;
            ";

        var parameters = new DynamicParameters();
        parameters.Add("@OdiKullaniciId", odiKullaniciId);
        parameters.Add("@PerformerIdList", performerIdList);

        var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);

        var results = await connection.QueryAsync<PerformerMenajerListItemOutputDTO>(query, parameters);

        return results.ToList();
    }

    //Menajerlere ait performer id listesini getirir.
    public async Task<List<MenajerPerformerListItemOutputDTO>> MenajerPerformerListesiGetir(List<string> manajerIdList)
    {
        var query = @"
                SELECT p.PerformerId,
                       p.MenajerId
                FROM PerformerYetenekTemsilcisi p
                WHERE p.MenajerId IN @MenajerIdList AND p.Aktif = 1;
            ";

        var parameters = new DynamicParameters();
        parameters.Add("@MenajerIdList", manajerIdList);

        var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);

        var results = await connection.QueryAsync<MenajerPerformerListItemOutputDTO>(query, parameters);

        return results.ToList();
    }
}