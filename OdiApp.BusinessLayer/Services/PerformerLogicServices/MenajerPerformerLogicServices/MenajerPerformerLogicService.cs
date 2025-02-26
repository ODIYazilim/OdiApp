using AutoMapper;
using OdiApp.EntityLayer.Base;
using OdiApp.DataAccessLayer.PerformerDataServices.KullaniciBasicDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.MenajerPerformerNotDataServices;
using OdiApp.DTOs.PerformerDTOs.MenajerPerformerDTOs;
using OdiApp.EntityLayer.PerformerModels.MenajerPerformerNotModels;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.SharedModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.MenajerPerformerLogicServices;

public class MenajerPerformerLogicService : IMenajerPerformerLogicService
{
    private readonly IMapper _mapper;
    private readonly IMenajerPerformerNotDataService _menajerPerformerNotDataService;
    private readonly IKullaniciBasicDataService _kullaniciBasicDataService;

    public MenajerPerformerLogicService(IMapper mapper, IMenajerPerformerNotDataService menajerPerformerNotDataService, IKullaniciBasicDataService kullaniciBasicDataService)
    {
        _mapper = mapper;
        _menajerPerformerNotDataService = menajerPerformerNotDataService;
        _kullaniciBasicDataService = kullaniciBasicDataService;
    }

    #region Menajer Performer Not

    public async Task<OdiResponse<MenajerPerformerNotOutputDTO>> MenajerPerformerNotEkleGuncelle(MenajerPerformerNotCreateOrUpdateDTO model, OdiUser user)
    {
        bool kontrol = await _menajerPerformerNotDataService.MenajerPerformerNotKontrolEt(model.PerformerId, model.MenajerId);
        MenajerPerformerNot menajerPerformerNot = null;

        DateTime date = DateTime.Now;

        if (kontrol)
        {
            //Kayıt var

            menajerPerformerNot = await _menajerPerformerNotDataService.MenajerPerformerNotGetir(model.PerformerId, model.MenajerId);

            if (menajerPerformerNot == null) return OdiResponse<MenajerPerformerNotOutputDTO>.Fail("Bu id ile bir kayıt bulunamadı.", "Not Found", 404);

            menajerPerformerNot = _mapper.Map(model, menajerPerformerNot);

            menajerPerformerNot.NotKayitTarihi = date;

            menajerPerformerNot.Guncelleyen = user.AdSoyad;
            menajerPerformerNot.GuncelleyenId = user.Id;
            menajerPerformerNot.GuncellenmeTarihi = date;

            menajerPerformerNot = await _menajerPerformerNotDataService.MenajerPerformerNotGuncelle(menajerPerformerNot);
        }
        else
        {
            //Kayıt Yok

            menajerPerformerNot = _mapper.Map<MenajerPerformerNot>(model);

            menajerPerformerNot.NotKayitTarihi = date;

            menajerPerformerNot.Ekleyen = user.AdSoyad;
            menajerPerformerNot.EkleyenId = user.Id;
            menajerPerformerNot.EklenmeTarihi = date;

            menajerPerformerNot.Guncelleyen = user.AdSoyad;
            menajerPerformerNot.GuncelleyenId = user.Id;
            menajerPerformerNot.GuncellenmeTarihi = date;

            menajerPerformerNot = await _menajerPerformerNotDataService.YeniMenajerPerformerNot(menajerPerformerNot);
        }

        return OdiResponse<MenajerPerformerNotOutputDTO>.Success("Not kayıt işlemi başarılı", await MenajerPerformerNotGetir(menajerPerformerNot.PerformerId, menajerPerformerNot.MenajerId), 200);
    }

    public async Task<OdiResponse<MenajerPerformerNotOutputDTO>> MenajerPerformerNotGetir(MenajerPerformerNotGetirInputDTO model)
    {
        return OdiResponse<MenajerPerformerNotOutputDTO>.Success("Not getirildi", await MenajerPerformerNotGetir(model.PerformerId, model.MenajerId), 200);
    }

    private async Task<MenajerPerformerNotOutputDTO> MenajerPerformerNotGetir(string performerId, string menajerId)
    {
        MenajerPerformerNot menajerPerformerNot = await _menajerPerformerNotDataService.MenajerPerformerNotGetir(performerId, menajerId);
        if (menajerPerformerNot == null) return new MenajerPerformerNotOutputDTO();

        MenajerPerformerNotOutputDTO dto = _mapper.Map<MenajerPerformerNotOutputDTO>(menajerPerformerNot);

        KullaniciBasic menajer = await _kullaniciBasicDataService.KullaniciGetir(dto.MenajerId);

        if (menajer != null)
            dto.MenajerAdSoyad = menajer.KullaniciAdSoyad;

        KullaniciBasic performer = await _kullaniciBasicDataService.KullaniciGetir(dto.PerformerId);

        if (performer != null)
            dto.PerformerAdSoyad = performer.KullaniciAdSoyad;

        return dto;
    }

    #endregion
}