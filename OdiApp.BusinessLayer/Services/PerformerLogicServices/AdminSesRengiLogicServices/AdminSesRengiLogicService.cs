using AutoMapper;
using OdiApp.DataAccessLayer.PerformerDataServices.SesRengiDataServices;
using OdiApp.DTOs.PerformerDTOs;
using OdiApp.DTOs.PerformerDTOs.SesRengiDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.PerformerModels.SesRengiModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminSesRengiLogicServices;

public class AdminSesRengiLogicService : IAdminSesRengiLogicService
{
    private readonly IMapper _mapper;
    private readonly ISesRengiDataService _sesRengiDataService;

    public AdminSesRengiLogicService(IMapper mapper, ISesRengiDataService sesRengiDataService)
    {
        _mapper = mapper;
        _sesRengiDataService = sesRengiDataService;
    }

    public async Task<OdiResponse<SesRengi>> SesRengiOlustur(SesRengi model, OdiUser user)
    {
        DateTime date = DateTime.Now;

        model.EklenmeTarihi = date;
        model.Ekleyen = user.AdSoyad;
        model.EkleyenId = user.Id;

        model.GuncellenmeTarihi = date;
        model.Guncelleyen = user.AdSoyad;
        model.GuncelleyenId = user.Id;

        model = await _sesRengiDataService.YeniSesRengi(model);

        return OdiResponse<SesRengi>.Success("Ses rengi oluşturuldu.", model, 200);
    }

    public async Task<OdiResponse<SesRengi>> SesRengiGuncelle(SesRengi model, OdiUser user)
    {
        SesRengi sesRengi = await _sesRengiDataService.SesRengiGetir(model.Id);

        if (sesRengi == null) return OdiResponse<SesRengi>.Fail("Bu id ile kayıt bulunamadı.", "Not Found", 404);

        sesRengi = _mapper.Map(model, sesRengi);

        DateTime date = DateTime.Now;

        sesRengi.GuncellenmeTarihi = date;
        sesRengi.Guncelleyen = user.AdSoyad;
        sesRengi.GuncelleyenId = user.Id;

        await _sesRengiDataService.SesRengiGuncelle(sesRengi);

        return OdiResponse<SesRengi>.Success("Ses rengi güncellendi.", sesRengi, 200);
    }

    public async Task<OdiResponse<SesRengi>> SesRengiDurumDegistir(SesRengiIdDTO model, OdiUser user)
    {
        SesRengi sesRengi = await _sesRengiDataService.SesRengiGetir(model.SesRengiId);

        if (sesRengi == null) return OdiResponse<SesRengi>.Fail("Bu id ile kayıt bulunamadı.", "Not Found", 404);

        sesRengi.Aktif = !sesRengi.Aktif;

        DateTime date = DateTime.Now;

        sesRengi.GuncellenmeTarihi = date;
        sesRengi.Guncelleyen = user.AdSoyad;
        sesRengi.GuncelleyenId = user.Id;

        await _sesRengiDataService.SesRengiGuncelle(sesRengi);

        return OdiResponse<SesRengi>.Success("Ses rengi aktif durumu güncellendi.", sesRengi, 200);
    }

    public async Task<OdiResponse<List<SesRengi>>> SesRengiListele(DilIdDTO model)
    {
        List<SesRengi> sesRengiList = await _sesRengiDataService.SesRengiListesiGetir(model.DilId);

        return OdiResponse<List<SesRengi>>.Success("Ses rengi listesi getirildi.", sesRengiList, 200);
    }

    public async Task<OdiResponse<SesRengi>> SesRengiGetir(SesRengiIdDTO model)
    {
        SesRengi sesRengi = await _sesRengiDataService.SesRengiGetir(model.SesRengiId);

        return OdiResponse<SesRengi>.Success("Ses rengi getirildi.", sesRengi, 200);
    }
}