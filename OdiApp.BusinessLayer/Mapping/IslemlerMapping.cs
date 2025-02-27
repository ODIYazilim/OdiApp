using AutoMapper;
using OdiApp.BusinessLayer.Core;
using OdiApp.DTOs.IslemlerDTOs.CallbackIslemler;
using OdiApp.DTOs.IslemlerDTOs.OdiIslemler.OdiTalepDTOs;
using OdiApp.DTOs.IslemlerDTOs.OdiIslemler.PerformerOdiDTO;
using OdiApp.DTOs.IslemlerDTOs.OdiListeler;
using OdiApp.DTOs.IslemlerDTOs.OpsiyonIslemler;
using OdiApp.DTOs.IslemlerDTOs.PerformerListeler;
using OdiApp.DTOs.IslemlerDTOs.ProjePerformer;
using OdiApp.DTOs.IslemlerDTOs.RolSendeDTOs;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.EntityLayer.IslemlerModels.CallbackIslemler;
using OdiApp.EntityLayer.IslemlerModels.OdiIslemler;
using OdiApp.EntityLayer.IslemlerModels.OdiListeler;
using OdiApp.EntityLayer.IslemlerModels.OpsiyonIslemler;
using OdiApp.EntityLayer.IslemlerModels.PerformerListeler;
using OdiApp.EntityLayer.IslemlerModels.RolSendeModels;

namespace OdiApp.BusinessLayer.Mapping;
public class IslemlerMapping : Profile
{
    public IslemlerMapping()
    {
        #region Odi Talep
        CreateMap<OdiTalep, OdiTalepCreateDTO>().ReverseMap();
        CreateMap<OdiTalep, OdiTalepOutputDTO>().ForMember(dest => dest.OdiTalepId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        #endregion

        #region Odi Talep To Performer


        //performer odileri

        CreateMap<PerformerOdi, PerformerOdiCreateDTO>().ReverseMap();
        CreateMap<PerformerOdi, PerformerOdiUpdateDTO>().ForMember(dest => dest.PerformerOdiId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<PerformerOdi, PerformerOdiOutputDTO>().ForMember(dest => dest.PerformerOdiId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<PerformerOdiSoru, PerformerOdiSoruCreateDTO>().ReverseMap();
        CreateMap<PerformerOdiSoru, PerformerOdiSoruOutputDTO>().ForMember(dest => dest.PerformerOdiSoruId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<PerformerOdiSoru, PerformerOdiSoruUpdateDTO>().ForMember(dest => dest.PerformerOdiSoruId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<PerformerOdiFotograf, PerformerOdiFotografCreateDTO>().ReverseMap();
        CreateMap<PerformerOdiFotograf, PerformerOdiFotografOutputDTO>().ForMember(dest => dest.PerformerOdiFotografId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<PerformerOdiFotograf, PerformerOdiFotografUpdateDTO>().ForMember(dest => dest.PerformerOdiFotografId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<PerformerOdiSes, PerformerOdiSesCreateDTO>().ReverseMap();
        CreateMap<PerformerOdiSes, PerformerOdiSesOutputDTO>().ForMember(dest => dest.PerformerOdiSesId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<PerformerOdiSes, PerformerOdiSesUpdateDTO>().ForMember(dest => dest.PerformerOdiSesId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<PerformerOdiVideo, PerformerOdiVideoCreateDTO>().ReverseMap();
        CreateMap<PerformerOdiVideo, PerformerOdiVideoOutputDTO>().ForMember(dest => dest.PerformerOdiVideoId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<PerformerOdiVideo, PerformerOdiVideoUpdateDTO>().ForMember(dest => dest.PerformerOdiVideoId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<PerformerOdiTekrarCekOneri, PerformerOdiTekrarCekOneriOutput>().ForMember(dest => dest.PerformerOdiTekrarCekId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        //
        #endregion

        #region Listeler

        #region Odi Liste
        CreateMap<OdiListe, OdiListeCreateDTO>().ReverseMap();
        CreateMap<OdiListe, OdiListeOutputDTO>().ForMember(dest => dest.OdiListeId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<OdiListeDetay, OdiListeDetayCreateDTO>().ReverseMap();


        #endregion

        #region Performer Liste
        CreateMap<PerformerListe, PerformerListeDTO>().ForMember(dest => dest.PerformerListeId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<PerformerListe, PerformerListeCreateDTO>().ReverseMap();
        CreateMap<PerformerListe, PerformerListeOutputDTO>().ForMember(dest => dest.PerformerListeId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<PerformerListe, PerformerListeUpdateDTO>().ForMember(dest => dest.PerformerListeId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<PerformerListeDetay, PerformerIdDTO>().ForMember(dest => dest.PerformerId, opt => opt.MapFrom(src => src.PerformerId)).ReverseMap();

        CreateMap<PerformerListeDetay, PerformerListeDetayCreateDTO>().ReverseMap();
        CreateMap<PerformerListeDetayDisplayInfoDTO, PerformerDisplayInfoDTO>().ReverseMap();
        CreateMap<PerformerListeDetay, PerformerListeDetayOutputDTO>().ForMember(dest => dest.PerformerListeDetayId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<PerformerListeDetayDisplayInfoDTO, KullaniciBilgileriDTO>().ReverseMap();
        #endregion

        #endregion

        #region Opsiyon

        CreateMap<OpsiyonListesi, OpsiyonListesiCreateDTO>().ReverseMap();
        CreateMap<OpsiyonListesi, OpsiyonListesiOutputDTO>().ForMember(dest => dest.OpsiyonListesiId, opt => opt.MapFrom(src => src.Id));

        CreateMap<Opsiyon, OpsiyonCreateDTO>().ReverseMap();

        //CreateMap<Opsiyon, OpsiyonYanitlaDTO>()
        //.ForMember(dest => dest.MusaitOlmadigimGunler, opt => opt.Ignore())
        //.ReverseMap();

        CreateMap<Opsiyon, OpsiyonYanitlaDetayDTO>()
            .ForMember(dest => dest.OpsiyonId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MusaitOlmadigimGunler, opt => opt.Ignore()).ReverseMap();
        //.ForMember(dest => dest.MusaitOlmadigimGunler, opt => opt.MapFrom(src => ConvertToStringList(src.MusaitOlmadigimGunler)))
        //.ReverseMap();

        CreateMap<Opsiyon, OpsiyonOutputDTO>()
        .ForMember(dest => dest.OpsiyonId, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.MusaitOlmadigimGunler, opt => opt.MapFrom(src => ConvertToDateTimeList(src.MusaitOlmadigimGunler)))
        .ReverseMap();
        CreateMap<OpsiyonViewDTO, OpsiyonOutputDTO>().ForMember(dest => dest.MusaitOlmadigimGunler, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.MusaitOlmadigimGunler) ? null : ConvertToDateTimeList(src.MusaitOlmadigimGunler)));
        CreateMap<OpsiyonListesiViewDTO, OpsiyonListesiOutputDTO>().ReverseMap();
        CreateMap<OpsiyonAnketSorulari, OpsiyonAnketSorulariCreateDTO>().ReverseMap();
        CreateMap<OpsiyonAnketSorulari, OpsiyonAnketSorulariOutputDTO>().ForMember(dest => dest.OpsiyonAnketSorulariId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<OpsiyonAnketSorulari, OpsiyonAnketSorulariUpdateDTO>().ForMember(dest => dest.OpsiyonAnketSorulariId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        #endregion

        #region Callback

        CreateMap<CallbackAyarlari, CallbackAyarlariCreateDTO>().ForMember(dest => dest.CallbackTarihleri, opt => opt.MapFrom(src => Fonksiyonlar.convertStringToDateTimeList(src.CallbackTarihleri)));
        CreateMap<CallbackAyarlariCreateDTO, CallbackAyarlari>().ForMember(dest => dest.CallbackTarihleri, opt => opt.MapFrom(src => Fonksiyonlar.convertDateTimeListToString(src.CallbackTarihleri)));
        CreateMap<CallbackNot, CallbackNotCreateDTO>().ReverseMap();
        CreateMap<CallbackSenaryo, CallbackSenaryoCreateDTO>().ReverseMap();
        CreateMap<CallbackSaat, CallbackSaatCreateDTO>().ReverseMap();
        CreateMap<CallbackSaat, CallbackSaatOutputDTO>().ForMember(dest => dest.CallbackSaatId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<Callback, CallbackCreateDTO>().ReverseMap();
        CreateMap<Callback, CallbackOutputDTO>().ForMember(dest => dest.CallbackId, opt => opt.MapFrom(src => src.Id)).ReverseMap();


        //CreateMap<Callback, CallbackOutputDTO>().ForMember(dest => dest.CallbackId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        #endregion

        #region RolSende

        CreateMap<RolSende, RolSendeCreateDTO>().ReverseMap();

        #endregion
    }

    private string ConvertToStringList(List<DateTime> dates)
    {
        return string.Join(',', dates.Select(date => date.ToString()));
    }

    private List<DateTime>? ConvertToDateTimeList(string? musaitOlmadigimGunler)
    {
        if (string.IsNullOrEmpty(musaitOlmadigimGunler))
        {
            return null;
        }

        try
        {
            var dateStrings = musaitOlmadigimGunler.Split(',');
            var dateList = dateStrings.Select(dateString => DateTime.Parse(dateString.Trim())).ToList();

            return dateList;
        }
        catch (FormatException)
        {
            return null;
        }
    }
}