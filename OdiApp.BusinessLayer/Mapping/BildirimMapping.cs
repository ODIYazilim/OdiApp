using AutoMapper;
using OdiApp.DTOs.PerformerDTOs.FotoAlbumDTOs;
using OdiApp.DTOs.PerformerDTOs.MenajerPerformerDTOs;
using OdiApp.DTOs.PerformerDTOs.OnerilerDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerAbonelikUrunDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.CVFizikselOzelliklerDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.MenajerPerformerGuncellenenAlanlarDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs.ProfilVideo;
using OdiApp.DTOs.PerformerDTOs.PerformerEtiketleriDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerMenajerDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerPuanDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerTakvimler;
using OdiApp.DTOs.PerformerDTOs.PerformerYorumDTOs;
using OdiApp.DTOs.PerformerDTOs.ProfilOnayDTOs;
using OdiApp.DTOs.PerformerDTOs.SektorDTOs;
using OdiApp.DTOs.SharedDTOs.AbonelikUrunuDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.EgitimDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.YetenekTemsilcisiDTOs;
using OdiApp.EntityLayer.PerformerModels.Egitim;
using OdiApp.EntityLayer.PerformerModels.FotografAlbum;
using OdiApp.EntityLayer.PerformerModels.MenajerPerformerNotModels;
using OdiApp.EntityLayer.PerformerModels.OnerilerModels;
using OdiApp.EntityLayer.PerformerModels.PerformerAbonelikModels;
using OdiApp.EntityLayer.PerformerModels.PerformerAbonelikUrunModels;
using OdiApp.EntityLayer.PerformerModels.PerformerCVModels;
using OdiApp.EntityLayer.PerformerModels.PerformerEtiketleriModels;
using OdiApp.EntityLayer.PerformerModels.PerformerMenajerModels;
using OdiApp.EntityLayer.PerformerModels.PerformerPuanModels;
using OdiApp.EntityLayer.PerformerModels.PerformerTakvimModels;
using OdiApp.EntityLayer.PerformerModels.PerformerYorumModels;
using OdiApp.EntityLayer.PerformerModels.ProfilOnayModels;
using OdiApp.EntityLayer.PerformerModels.SektorModels;
using OdiApp.EntityLayer.PerformerModels.YetenekTemsilcisiModels;
using OdiApp.EntityLayer.SharedModels;

namespace OdiApp.BusinessLayer.Mapping;

public class BildirimMapping : Profile
{

    public BildirimMapping()
    {
        CreateMap<Sektor, SektorOutputDTO>().ForMember(dest => dest.SektorId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        #region Performer Yorum

        CreateMap<PerformerYorum, PerformerYorumOutputDTO>().ForMember(dest => dest.PerformerYorumId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<PerformerYorum, PerformerYorumCreateDTO>().ReverseMap();

        #endregion

        #region Performer Etiketleri

        CreateMap<YetenekTemsilcisiPerformerEtiketi, YetenekTemsilcisiPerformerEtiketiUpdateItemDTO>().ReverseMap();

        CreateMap<PerformerEtiket, PerformerEtiketOutputDTO>().ReverseMap();

        CreateMap<YetenekTemsilcisiPerformerEtiketTipi, YetenekTemsilcisiPerformerEtiketTipiOutputDTO>().ForMember(dest => dest.YetenekTemsilcisiPerformerEtiketTipiId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<YetenekTemsilcisiPerformerEtiketTipi, YetenekTemsilcisiPerformerEtiketiListesiOutputDTO>().ForMember(dest => dest.YetenekTemsilcisiPerformerEtiketTipiId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        #endregion

        #region Menajer Performer 

        CreateMap<MenajerPerformerNot, MenajerPerformerNotCreateOrUpdateDTO>().ReverseMap();
        CreateMap<MenajerPerformerNot, MenajerPerformerNotOutputDTO>().ForMember(dest => dest.MenajerPerformerNotId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        #endregion

        #region Performer Abonelik

        CreateMap<PerformerAbonelikUrunu, PerformerAbonelikUrunuCreateDTO>().ReverseMap();
        CreateMap<PerformerAbonelikUrunu, PerformerAbonelikUrunuUpdateDTO>().ForMember(dest => dest.PerformerAbonelikUrunuId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<PerformerAbonelik, PerformerAbonelikCreateDTO>().ReverseMap();
        CreateMap<PerformerAbonelikUrunu, PerformerAbonelikUrunuOutputDTO>().ForMember(dest => dest.PerformerAbonelikUrunuId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        #endregion

        #region Performer Menajer

        CreateMap<PerformerMenajerSozlesme, PerformerMenajerSozlesmeCreateDTO>().ReverseMap();
        CreateMap<PerformerMenajerSozlesme, PerformerMenajerSozlesmeUpdateDTO>().ForMember(dest => dest.PerformerMenajerSozlesmeId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<PerformerMenajerSozlesme, PerformerMenajerSozlesmeOutputDTO>().ForMember(dest => dest.PerformerMenajerSozlesmeId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        #endregion

        #region Performer Puan

        CreateMap<PerformerPuan, PerformerIcinPuanVerInputDTO>().ReverseMap();
        CreateMap<PerformerPuan, PerformerPuanListOutputDTO>().ForMember(dest => dest.PerformerPuanId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        #endregion

        // 

        CreateMap<EgitimTipi, EgitimTipiDTO>().ForMember(dest => dest.EgitimTipiId, opt => opt.MapFrom(src => src.Id))
                                              .ForMember(dest => dest.EgitimTipi, opt => opt.MapFrom(src => src.Tip))
                                              .ReverseMap();

        CreateMap<Okul, OkulDTO>().ForMember(dest => dest.OkulId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<OkulBolum, OkulBolumDTO>().ForMember(dest => dest.OkulBolumId, opt => opt.MapFrom(src => src.Id)).ReverseMap();



        //


        //CreateMap<CVEgitimBilgisi, CVEgitimDTO>().ForMember(dest => dest.CVEgitimBilgisiId, opt => opt.MapFrom(src => src.Id))
        //                                         .ForMember(dest => dest.PerformerCVId, opt => opt.MapFrom(src => src.CVId)).ReverseMap();





        CreateMap<CVFizikselOzellik, CVFizikselOzellikCreateDTO>().ReverseMap();


        CreateMap<PerformerCV, PerformerCVCreateDTO>().ReverseMap();
        CreateMap<PerformerCV, PerformerCVOutputDTO>().ForMember(dest => dest.PerformerCVId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<PerformerCV, PerformerCVUpdateDTO>().ForMember(dest => dest.PerformerCVId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<CV, CVCreateDTO>().ReverseMap();
        CreateMap<CVData, CVDataInputDTO>().ReverseMap();
        CreateMap<MenajerPerformerGuncellenenAlani, MenajerPerformerGuncellenenAlaniOutputDTO>().ForMember(dest => dest.MenajerPerformerGuncellenenAlaniId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        //

        CreateMap<FotoAlbumTipiLabel, FotoAlbumTipiDTO>().ForMember(dest => dest.FotoAlbumTipAdi, opt => opt.MapFrom(src => src.Label)).ReverseMap();
        CreateMap<FotoAlbumTipi, FotoAlbumTipiDTO>()
            .ForMember(dest => dest.FotoAlbumTipAdi, opt => opt.MapFrom(src => src.AlbumTipi))
            .ForMember(dest => dest.AlbumTipId, opt => opt.MapFrom(src => src.Id))
            .ReverseMap();

        CreateMap<FotoAlbum, FotoAlbumDTO>().ForMember(dest => dest.FotoAlbumId, opt => opt.MapFrom(src => src.Id))
                                            .ForMember(dest => dest.FotoAlbumTipi, opt => opt.MapFrom(src => src.AlbumTipi.AlbumTipi))
                                            .ForMember(dest => dest.Premium, opt => opt.MapFrom(src => src.AlbumTipi.Premium));

        CreateMap<FotoAlbumDTO, FotoAlbum>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FotoAlbumId));

        CreateMap<FotoAlbumFotograf, FotoAlbumFotografDTO>().ForMember(dest => dest.FotografId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<ProfilVideo, ProfilVideoCreateDTO>().ReverseMap();
        //

        //CreateMap<VideoAlbumTipiLabel, VideoAlbumTipiDTO>().ForMember(dest => dest.VideoAlbumTipAdi, opt => opt.MapFrom(src => src.Label)).ReverseMap();
        //CreateMap<VideoAlbum, VideoAlbumDTO>().ForMember(dest => dest.VideoAlbumId, opt => opt.MapFrom(src => src.Id))
        //                                    .ForMember(dest => dest.VideoAlbumTipi, opt => opt.MapFrom(src => src.AlbumTipi.AlbumTipi));
        //CreateMap<VideoAlbumDTO, VideoAlbum>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.VideoAlbumId));

        //CreateMap<VideoAlbumVideo, VideoAlbumVideoDTO>().ForMember(dest => dest.VideoId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        //
        CreateMap<ProfilOnay, ProfilOnayGonderDTO>().ForMember(dest => dest.ProfilOnayId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<ProfilOnay, ProfilOnayOutputDTO>().ForMember(dest => dest.ProfilOnayId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<ProfilOnay, ProfilOnayRedInputDTO>().ForMember(dest => dest.ProfilOnayId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<ProfilOnay, ProfilOnayTalepDTO>().ForMember(dest => dest.ProfilOnayId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<ProfilOnayRedNedeniTanimi, ProfilOnayRedNedeniTanimiDTO>().ForMember(dest => dest.ProfilOnayRedNedeniId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<PerformerTakvim, PerformerTakvimCreateDTO>().ReverseMap();
        CreateMap<PerformerTakvim, PerformerTakvimOutputDTO>().ForMember(dest => dest.PerformerTakvimId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<PerformerTakvim, PerformerTakvimUpdateDTO>().ForMember(dest => dest.PerformerTakvimId, opt => opt.MapFrom(src => src.Id)).ReverseMap();


        //Oneriler
        CreateMap<OneriTalepleri, OneriTalepEtDTO>().ForMember(dest => dest.OneriIsteyenId, opt => opt.MapFrom(src => src.TalepGonderenId)).ReverseMap();

        //YetenekTemsilcisi
        CreateMap<PerformerYetenekTemsilcisi, PerformerYetenekTemsilcisiAtamaDTO>().ReverseMap();

        #region KullaniciBasic

        CreateMap<KullaniciBasic, KullaniciBasicEkleDTO>().ReverseMap();

        #endregion
    }
}