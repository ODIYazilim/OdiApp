using AutoMapper;
using OdiApp.DTOs.OdemeDTOs.AbonelikUrunuDTOs;
using OdiApp.DTOs.OdemeDTOs.OdicikİslemleriDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels;
using OdiApp.EntityLayer.OdemeModels.OdicikModels;
using OdiApp.EntityLayer.SharedModels;
using AbonelikUrunu = OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels.AbonelikUrunu;

namespace OdiApp.BusinessLayer.Mapping;
public class OdemeMapping : Profile
{
    public OdemeMapping()
    {
        #region Odicik İslemleri

        CreateMap<OdicikIslemleri, OdicikEklemeDTO>().ReverseMap();
        CreateMap<OdicikIslemleri, OdicikHarcamaDTO>().ReverseMap();
        CreateMap<OdicikIslemleri, OdicikIslemleriOutputDTO>().ForMember(dest => dest.OdicikIslemleriId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        #endregion

        #region Abonelik Urunleri

        CreateMap<AbonelikUrunu, OdemeYontemiPerformerAbonelikUrunuCreateDTO>().ReverseMap();
        CreateMap<AbonelikUrunuOdemePlani, AbonelikUrunuOdemePlaniOutputDTO>().ForMember(dest => dest.AbonelikUrunuOdemePlaniId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<AbonelikYukseltmeTalep, AbonelikYukseltmeTalepCreateDTO>().ReverseMap();

        #endregion

        #region KullaniciBasic

        CreateMap<KullaniciBasic, KullaniciBasicEkleDTO>().ReverseMap();

        #endregion
    }
}