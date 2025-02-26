using AutoMapper;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerProfilAlanlariDataServices;
using OdiApp.DTOs.PerformerDTOs.PerformerProfilAlanlariDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.PerformerModels.PerformerProfilModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminPerformerProfilAlanlariLogicServices;

public class AdminPerformerProfilAlanlariLogicService : IAdminPerformerProfilAlanlariLogicService
{
    private readonly IMapper _mapper;
    private readonly IPerformerProfilAlanlariDataService _performerProfilAlanlariDataService;

    public AdminPerformerProfilAlanlariLogicService(IMapper mapper, IPerformerProfilAlanlariDataService performerProfilAlanlariDataService)
    {
        _mapper = mapper;
        _performerProfilAlanlariDataService = performerProfilAlanlariDataService;
    }

    public async Task<OdiResponse<PerformerProfilAlanlari>> PerformerProfilAlanlariOlustur(PerformerProfilAlanlari model, OdiUser user)
    {
        DateTime date = DateTime.Now;

        model.EklenmeTarihi = date;
        model.Ekleyen = user.AdSoyad;
        model.EkleyenId = user.Id;

        model.GuncellenmeTarihi = date;
        model.Guncelleyen = user.AdSoyad;
        model.GuncelleyenId = user.Id;

        model = await _performerProfilAlanlariDataService.YeniPerformerProfilAlanlari(model);

        return OdiResponse<PerformerProfilAlanlari>.Success("Performer profil alanı oluşturuldu.", model, 200);
    }

    public async Task<OdiResponse<PerformerProfilAlanlari>> PerformerProfilAlanlariGuncelle(PerformerProfilAlanlari model, OdiUser user)
    {
        PerformerProfilAlanlari performerProfilAlanlari = await _performerProfilAlanlariDataService.PerformerProfilAlanlariGetir(model.Id);

        if (performerProfilAlanlari == null) return OdiResponse<PerformerProfilAlanlari>.Fail("Bu id ile kayıt bulunamadı.", "Not Found", 404);

        performerProfilAlanlari = _mapper.Map(model, performerProfilAlanlari);

        DateTime date = DateTime.Now;

        performerProfilAlanlari.GuncellenmeTarihi = date;
        performerProfilAlanlari.Guncelleyen = user.AdSoyad;
        performerProfilAlanlari.GuncelleyenId = user.Id;

        await _performerProfilAlanlariDataService.PerformerProfilAlanlariGuncelle(performerProfilAlanlari);

        return OdiResponse<PerformerProfilAlanlari>.Success("Performer profil alanı güncellendi.", performerProfilAlanlari, 200);
    }

    public async Task<OdiResponse<PerformerProfilAlanlari>> PerformerProfilAlanlariDurumDegistir(PerformerProfilAlanlariIdDTO model, OdiUser user)
    {
        PerformerProfilAlanlari performerProfilAlanlari = await _performerProfilAlanlariDataService.PerformerProfilAlanlariGetir(model.PerformerProfilAlanlariId);

        if (performerProfilAlanlari == null) return OdiResponse<PerformerProfilAlanlari>.Fail("Bu id ile kayıt bulunamadı.", "Not Found", 404);

        performerProfilAlanlari.Aktif = !performerProfilAlanlari.Aktif;

        DateTime date = DateTime.Now;

        performerProfilAlanlari.GuncellenmeTarihi = date;
        performerProfilAlanlari.Guncelleyen = user.AdSoyad;
        performerProfilAlanlari.GuncelleyenId = user.Id;

        await _performerProfilAlanlariDataService.PerformerProfilAlanlariGuncelle(performerProfilAlanlari);

        return OdiResponse<PerformerProfilAlanlari>.Success("Performer profil alanı aktif durumu güncellendi.", performerProfilAlanlari, 200);
    }

    public async Task<OdiResponse<List<PerformerProfilAlanlari>>> PerformerProfilAlanlariListele(PerformerProfilAlanlariListeleInputDTO model)
    {
        List<PerformerProfilAlanlari> performerProfilAlanlariList = await _performerProfilAlanlariDataService.PerformerProfilAlanlariListesiGetir(model.KayitTuru);

        return OdiResponse<List<PerformerProfilAlanlari>>.Success("Performer profil alanları listesi getirildi.", performerProfilAlanlariList, 200);
    }
}