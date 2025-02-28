using AutoMapper;
using OdiApp.DTOs.UygulamaBilgileriDTOs;
using OdiApp.DTOs.UygulamaBilgileriDTOs.BankaDTOs;
using OdiApp.DTOs.UygulamaBilgileriDTOs.DilDtos;
using OdiApp.DTOs.UygulamaBilgileriDTOs.KayitGrubuDtos;
using OdiApp.DTOs.UygulamaBilgileriDTOs.KayitTuruDtos;
using OdiApp.DTOs.UygulamaBilgileriDTOs.SabitMetinDTOs;
using OdiApp.DTOs.UygulamaBilgileriDTOs.SosyalMedyaDTOs;
using OdiApp.DTOs.UygulamaBilgileriDTOs.SSSDTOs;
using OdiApp.DTOs.UygulamaBilgileriDTOs.TelefonKoduDtos;
using OdiApp.EntityLayer.UygulamaBilgileriModels;

namespace OdiApp.BusinessLayer.Mapping;
public class UygulamaBilgilerMapping : Profile
{
    public UygulamaBilgilerMapping()
    {
        CreateMap<Sehir, SehirDTo>().ForMember(dest => dest.SehirId, opt => opt.MapFrom(src => src.Id)).ReverseMap().ReverseMap();
        CreateMap<Ilce, IlceDTO>().ForMember(dest => dest.IlceId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<Dil, DilDTO>().ForMember(dest => dest.DilId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<TelefonKodu, TelefonKoduDTO>().ReverseMap();
        CreateMap<KayitTuru, KayitTuruDTO>().ForMember(dest => dest.KayitTuru, opt => opt.MapFrom(src => src.Tur)).ReverseMap();
        CreateMap<KayitGrubu, KayitGrubuDTO>().ReverseMap();
        CreateMap<SosyalMedya, SosyalMedyaOutputDTO>().ReverseMap();
        CreateMap<Banka, BankaOutputDTO>().ReverseMap();
        CreateMap<SabitMetin, SabitMetinOutputDTO>().ForMember(dest => dest.SabitMetinId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<SSS, SSSOutputDTO>().ReverseMap();
    }
}