using AutoMapper;
using OdiApp.DataAccessLayer.PerformerDataServices.SeslendirmeDiliDataServices;
using OdiApp.DTOs.PerformerDTOs;
using OdiApp.DTOs.PerformerDTOs.SeslendirmeDiliDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.PerformerModels.SeslendirmeDiliModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminSeslendirmeDiliLogicServices;

public class AdminSeslendirmeDiliLogicService : IAdminSeslendirmeDiliLogicService
{
    private readonly IMapper _mapper;
    private readonly ISeslendirmeDiliDataService _seslendirmeDiliDataService;

    public AdminSeslendirmeDiliLogicService(IMapper mapper, ISeslendirmeDiliDataService seslendirmeDiliDataService)
    {
        _mapper = mapper;
        _seslendirmeDiliDataService = seslendirmeDiliDataService;
    }

    public async Task<OdiResponse<SeslendirmeDili>> SeslendirmeDiliOlustur(SeslendirmeDili model, OdiUser user)
    {
        DateTime date = DateTime.Now;

        model.EklenmeTarihi = date;
        model.Ekleyen = user.AdSoyad;
        model.EkleyenId = user.Id;

        model.GuncellenmeTarihi = date;
        model.Guncelleyen = user.AdSoyad;
        model.GuncelleyenId = user.Id;

        model = await _seslendirmeDiliDataService.YeniSeslendirmeDili(model);

        return OdiResponse<SeslendirmeDili>.Success("Seslendirme dili oluşturuldu.", model, 200);
    }

    public async Task<OdiResponse<SeslendirmeDili>> SeslendirmeDiliGuncelle(SeslendirmeDili model, OdiUser user)
    {
        SeslendirmeDili seslendirmeDili = await _seslendirmeDiliDataService.SeslendirmeDiliGetir(model.Id);

        if (seslendirmeDili == null) return OdiResponse<SeslendirmeDili>.Fail("Bu id ile kayıt bulunamadı.", "Not Found", 404);

        seslendirmeDili = _mapper.Map(model, seslendirmeDili);

        DateTime date = DateTime.Now;

        seslendirmeDili.GuncellenmeTarihi = date;
        seslendirmeDili.Guncelleyen = user.AdSoyad;
        seslendirmeDili.GuncelleyenId = user.Id;

        await _seslendirmeDiliDataService.SeslendirmeDiliGuncelle(seslendirmeDili);

        return OdiResponse<SeslendirmeDili>.Success("Seslendirme dili güncellendi.", seslendirmeDili, 200);
    }

    public async Task<OdiResponse<SeslendirmeDili>> SeslendirmeDiliDurumDegistir(SeslendirmeDiliIdDTO model, OdiUser user)
    {
        SeslendirmeDili seslendirmeDili = await _seslendirmeDiliDataService.SeslendirmeDiliGetir(model.SeslendirmeDiliId);

        if (seslendirmeDili == null) return OdiResponse<SeslendirmeDili>.Fail("Bu id ile kayıt bulunamadı.", "Not Found", 404);

        seslendirmeDili.Aktif = !seslendirmeDili.Aktif;

        DateTime date = DateTime.Now;

        seslendirmeDili.GuncellenmeTarihi = date;
        seslendirmeDili.Guncelleyen = user.AdSoyad;
        seslendirmeDili.GuncelleyenId = user.Id;

        await _seslendirmeDiliDataService.SeslendirmeDiliGuncelle(seslendirmeDili);

        return OdiResponse<SeslendirmeDili>.Success("Seslendirme dili aktif durumu güncellendi.", seslendirmeDili, 200);
    }

    public async Task<OdiResponse<List<SeslendirmeDili>>> SeslendirmeDiliListele(DilIdDTO model)
    {
        List<SeslendirmeDili> seslendirmeDiliList = await _seslendirmeDiliDataService.SeslendirmeDiliListesiGetir(model.DilId);

        return OdiResponse<List<SeslendirmeDili>>.Success("Seslendirme dili listesi getirildi.", seslendirmeDiliList, 200);
    }

    public async Task<OdiResponse<SeslendirmeDili>> SeslendirmeDiliGetir(SeslendirmeDiliIdDTO model)
    {
        SeslendirmeDili seslendirmeDili = await _seslendirmeDiliDataService.SeslendirmeDiliGetir(model.SeslendirmeDiliId);

        return OdiResponse<SeslendirmeDili>.Success("Seslendirme dili getirildi.", seslendirmeDili, 200);
    }
}