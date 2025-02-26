using AutoMapper;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerEtiketleriDataServices;
using OdiApp.DTOs.PerformerDTOs.PerformerEtiketleriDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.PerformerModels.PerformerEtiketleriModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerEtiketleriLogicServices;

public class PerformerEtiketleriLogicService : IPerformerEtiketleriLogicService
{
    private readonly IMapper _mapper;
    private readonly IPerformerEtiketleriDataService _performerEtiketleriDataService;

    public PerformerEtiketleriLogicService(IMapper mapper, IPerformerEtiketleriDataService performerEtiketleriDataService)
    {
        _mapper = mapper;
        _performerEtiketleriDataService = performerEtiketleriDataService;
    }

    #region Yetenek Temsilcisi Performer Etiket Tipi (Admin)

    public async Task<OdiResponse<YetenekTemsilcisiPerformerEtiketTipi>> YetenekTemsilcisiPerformerEtiketTipiEkle(YetenekTemsilcisiPerformerEtiketTipi model, OdiUser user)
    {
        DateTime date = DateTime.Now;

        model.EklenmeTarihi = date;
        model.Ekleyen = user.AdSoyad;
        model.EkleyenId = user.Id;

        model.GuncellenmeTarihi = date;
        model.Guncelleyen = user.AdSoyad;
        model.GuncelleyenId = user.Id;

        model = await _performerEtiketleriDataService.YeniYetenekTemsilcisiPerformerEtiketTipi(model);

        return OdiResponse<YetenekTemsilcisiPerformerEtiketTipi>.Success("Yeni etiket tipi oluşturuldu.", model, 200);
    }

    public async Task<OdiResponse<YetenekTemsilcisiPerformerEtiketTipi>> YetenekTemsilcisiPerformerEtiketTipiGuncelle(YetenekTemsilcisiPerformerEtiketTipi model, OdiUser user)
    {
        YetenekTemsilcisiPerformerEtiketTipi etiketTipi = await _performerEtiketleriDataService.YetenekTemsilcisiPerformerEtiketTipiGetirById(model.Id);

        if (etiketTipi == null) return OdiResponse<YetenekTemsilcisiPerformerEtiketTipi>.Fail("Bu id ile kayıt bulunamadı.", "Not Found", 404);

        etiketTipi = _mapper.Map(model, etiketTipi);

        DateTime date = DateTime.Now;

        etiketTipi.GuncellenmeTarihi = date;
        etiketTipi.Guncelleyen = user.AdSoyad;
        etiketTipi.GuncelleyenId = user.Id;

        await _performerEtiketleriDataService.YetenekTemsilcisiPerformerEtiketTipiGuncelle(etiketTipi);

        return OdiResponse<YetenekTemsilcisiPerformerEtiketTipi>.Success("Etiket tipi güncellendi.", etiketTipi, 200);
    }

    public async Task<OdiResponse<NoContent>> YetenekTemsilcisiPerformerEtiketTipiSil(YetenekTemsilcisiPerformerEtiketTipiIdDTO model)
    {
        YetenekTemsilcisiPerformerEtiketTipi etiketTipi = await _performerEtiketleriDataService.YetenekTemsilcisiPerformerEtiketTipiGetirById(model.YetenekTemsilcisiPerformerEtiketTipiId);

        if (etiketTipi == null) return OdiResponse<NoContent>.Fail("Bu id ile kayıt bulunamadı.", "Not Found", 404);

        await _performerEtiketleriDataService.YetenekTemsilcisiPerformerEtiketTipiSil(etiketTipi);

        return OdiResponse<NoContent>.Success("Etiket tipi silindi.", null, 200);
    }

    public async Task<OdiResponse<List<YetenekTemsilcisiPerformerEtiketTipi>>> YetenekTemsilcisiPerformerEtiketTipiListesiGetir(int dilId)
    {
        List<YetenekTemsilcisiPerformerEtiketTipi> list = await _performerEtiketleriDataService.YetenekTemsilcisiPerformerEtiketTipiListesiGetir(dilId);

        return OdiResponse<List<YetenekTemsilcisiPerformerEtiketTipi>>.Success("Etiket tipi listesi getirildi.", list, 200);
    }

    #endregion

    #region Performer Etiket (Admin)

    public async Task<OdiResponse<PerformerEtiket>> PerformerEtiketEkle(PerformerEtiket model, OdiUser user)
    {
        DateTime date = DateTime.Now;

        model.EklenmeTarihi = date;
        model.Ekleyen = user.AdSoyad;
        model.EkleyenId = user.Id;

        model.GuncellenmeTarihi = date;
        model.Guncelleyen = user.AdSoyad;
        model.GuncelleyenId = user.Id;

        model = await _performerEtiketleriDataService.YeniPerformerEtiket(model);

        return OdiResponse<PerformerEtiket>.Success("Yeni etiket oluşturuldu.", model, 200);
    }

    public async Task<OdiResponse<PerformerEtiket>> PerformerEtiketGuncelle(PerformerEtiket model, OdiUser user)
    {
        PerformerEtiket etiket = await _performerEtiketleriDataService.PerformerEtiketGetirById(model.Id);

        if (etiket == null) return OdiResponse<PerformerEtiket>.Fail("Bu id ile kayıt bulunamadı.", "Not Found", 404);

        etiket = _mapper.Map(model, etiket);

        DateTime date = DateTime.Now;

        etiket.GuncellenmeTarihi = date;
        etiket.Guncelleyen = user.AdSoyad;
        etiket.GuncelleyenId = user.Id;

        await _performerEtiketleriDataService.PerformerEtiketGuncelle(etiket);

        return OdiResponse<PerformerEtiket>.Success("Etiket güncellendi.", etiket, 200);
    }

    public async Task<OdiResponse<NoContent>> PerformerEtiketSil(PerformerEtiketIdDTO model)
    {
        PerformerEtiket etiket = await _performerEtiketleriDataService.PerformerEtiketGetirById(model.PerformerEtiketId);

        if (etiket == null) return OdiResponse<NoContent>.Fail("Bu id ile kayıt bulunamadı.", "Not Found", 404);

        await _performerEtiketleriDataService.PerformerEtiketSil(etiket);

        return OdiResponse<NoContent>.Success("Etiket silindi.", null, 200);
    }

    public async Task<OdiResponse<List<PerformerEtiket>>> PerformerEtiketListesiGetir(int dilId)
    {
        List<PerformerEtiket> list = await _performerEtiketleriDataService.PerformerEtiketListesiGetir(dilId);

        return OdiResponse<List<PerformerEtiket>>.Success("Etiket listesi getirildi.", list, 200);
    }

    #endregion

    #region Yetenek Temsilcisi Performer Etiketi (Yetenek Temsilcisi)

    public async Task<OdiResponse<NoContent>> YetenekTemsilcisiPerformerEtiketiGuncelle(YetenekTemsilcisiPerformerEtiketiUpdateDTO model, OdiUser user)
    {
        List<YetenekTemsilcisiPerformerEtiketi> yeniPerformerEtiketListesi = _mapper.Map<List<YetenekTemsilcisiPerformerEtiketi>>(model.Etiketler);

        foreach (var item in yeniPerformerEtiketListesi)
        {
            item.YetenekTemsilcisiId = model.YetenekTemsilcisiId;
            item.PerformerId = model.PerformerId;
        }

        List<YetenekTemsilcisiPerformerEtiketi> varolanPerformerEtiketListesi = await _performerEtiketleriDataService.YetenekTemsilcisiPerformerEtiketiListesiGetir(model.YetenekTemsilcisiId, model.PerformerId);

        if (yeniPerformerEtiketListesi?.Any() == false)
        {
            await _performerEtiketleriDataService.YetenekTemsilcisiPerformerEtiketiTopluSil(varolanPerformerEtiketListesi);

            return OdiResponse<NoContent>.Success("Performer etiket güncellemeleri tamamlandı.", null, 200);
        }
        else
        {
            //Yeni listede gönderilmeyen alanlar kaldırılmış demektir. Bunlar silinecek.
            List<YetenekTemsilcisiPerformerEtiketi> removeList = varolanPerformerEtiketListesi.Where(x => !yeniPerformerEtiketListesi.Select(x => x.EtiketKodu).Contains(x.EtiketKodu)).ToList();

            await _performerEtiketleriDataService.YetenekTemsilcisiPerformerEtiketiTopluSil(removeList);

            List<YetenekTemsilcisiPerformerEtiketi> addList = yeniPerformerEtiketListesi.Where(x => !varolanPerformerEtiketListesi.Select(x => x.EtiketKodu).Contains(x.EtiketKodu)).ToList();

            foreach (var item in addList)
            {
                DateTime date = DateTime.Now;

                item.EklenmeTarihi = date;
                item.Ekleyen = user.AdSoyad;
                item.EkleyenId = user.Id;

                item.GuncellenmeTarihi = date;
                item.Guncelleyen = user.AdSoyad;
                item.GuncelleyenId = user.Id;
            }

            await _performerEtiketleriDataService.YetenekTemsilcisiPerformerEtiketiTopluEkle(addList);

            return OdiResponse<NoContent>.Success("Performer etiket güncellemeleri tamamlandı.", null, 200);
        }
    }

    public async Task<OdiResponse<List<YetenekTemsilcisiPerformerEtiketiListesiOutputDTO>>> YetenekTemsilcisiPerformerEtiketiListesiGetir(YetenekTemsilcisiPerformerEtiketiListesiDTO model, int dilId)
    {
        List<YetenekTemsilcisiPerformerEtiketiListesiOutputDTO> resultList = new List<YetenekTemsilcisiPerformerEtiketiListesiOutputDTO>();

        List<YetenekTemsilcisiPerformerEtiketTipi> etiketTipiList = await _performerEtiketleriDataService.YetenekTemsilcisiPerformerEtiketTipiListesiGetir(dilId, true);

        if (etiketTipiList != null)
        {
            List<PerformerEtiket> performerEtiketListesi = await _performerEtiketleriDataService.PerformerEtiketListesiGetir(dilId);

            if (performerEtiketListesi?.Any() == true)
            {
                resultList = _mapper.Map<List<YetenekTemsilcisiPerformerEtiketiListesiOutputDTO>>(etiketTipiList);

                List<YetenekTemsilcisiPerformerEtiketi> performerEtiketleri = await _performerEtiketleriDataService.YetenekTemsilcisiPerformerEtiketiListesiGetir(model.YetenekTemsilcisiId, model.PerformerId);

                foreach (var item in resultList)
                {
                    item.PerformerEtiketleri = _mapper.Map<List<PerformerEtiketOutputDTO>>(performerEtiketListesi.Where(x => x.EtiketTipKodu == item.EtiketTipKodu).ToList());

                    foreach (var etiket in item.PerformerEtiketleri)
                    {
                        if (performerEtiketleri.Any(x => x.EtiketKodu == etiket.EtiketKodu))
                        {
                            etiket.Secili = true;
                        }
                    }
                }
            }
        }

        return OdiResponse<List<YetenekTemsilcisiPerformerEtiketiListesiOutputDTO>>.Success("Performer etiket listesi getirildi.", resultList, 200);
    }

    #endregion
}