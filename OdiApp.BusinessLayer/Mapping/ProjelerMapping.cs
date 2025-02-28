using AutoMapper;
using OdiApp.DTOs.Enums;
using OdiApp.DTOs.ProjelerDTOs.OdiFotograf;
using OdiApp.DTOs.ProjelerDTOs.OdiSes;
using OdiApp.DTOs.ProjelerDTOs.OdiSoru;
using OdiApp.DTOs.ProjelerDTOs.OdiVideo;
using OdiApp.DTOs.ProjelerDTOs.PerformerProje;
using OdiApp.DTOs.ProjelerDTOs.ProjeBilgileriDTOs;
using OdiApp.DTOs.ProjelerDTOs.ProjeRolBilgileri;
using OdiApp.DTOs.ProjelerDTOs.ProjeRolBilgileri.ProjeRolAnketSorusuDTO;
using OdiApp.DTOs.ProjelerDTOs.ProjeRolBilgileri.ProjeRolDTO;
using OdiApp.DTOs.ProjelerDTOs.ProjeRolBilgileri.ProjeRolOzellikDTOs;
using OdiApp.DTOs.ProjelerDTOs.ProjeRolOdiBilgisi;
using OdiApp.DTOs.ProjelerDTOs.ProjeTurleri;
using OdiApp.DTOs.ProjelerDTOs.ProjeYetkilileri;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeBilgileriDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeYetkilileriDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeRolPerformerDTOs;
using OdiApp.EntityLayer.Base;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiFotograf;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiSes;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiSoru;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiVideo;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliRolBilgisi;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliRolOdi;
using OdiApp.EntityLayer.ProjelerModels.OdiFotograf;
using OdiApp.EntityLayer.ProjelerModels.OdiSes;
using OdiApp.EntityLayer.ProjelerModels.OdiSoru;
using OdiApp.EntityLayer.ProjelerModels.OdiVideo;
using OdiApp.EntityLayer.ProjelerModels.ProjeBilgileri;
using OdiApp.EntityLayer.ProjelerModels.ProjeRolBilgisi;
using OdiApp.EntityLayer.ProjelerModels.ProjeRolOdi;
using OdiApp.EntityLayer.SharedModels;

namespace OdiApp.BusinessLayer.Mapping;
public class ProjelerMapping : Profile
{
    public ProjelerMapping()
    {
        CreateMap<ProjeKatilimBolgesi, ProjeKatilimBolgesiOutputDTO>().ReverseMap();
        CreateMap<Proje, ProjeCreateDTO>().ReverseMap();
        CreateMap<Proje, ProjeUpdateDTO>().ForMember(dest => dest.ProjeId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<Proje, ProjeOutputDTO>().ForMember(dest => dest.ProjeId, opt => opt.MapFrom(src => src.Id))
                                          .ForMember(dest => dest.ProjeTuru, opt => opt.MapFrom(src => src.ProjeTuru.Tur)).ReverseMap();

        CreateMap<ProjeYetkili, ProjeYetkiliCreateDTO>().ReverseMap();
        CreateMap<ProjeYetkili, ProjeYetkiliUpdateDTO>().ForMember(dest => dest.ProjeYetkiliId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<ProjeYetkili, ProjeYetkiliOutputDTO>().ForMember(dest => dest.ProjeYetkiliId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<ProjeTuru, ProjeTuruOutputDTO>().ForMember(dest => dest.ProjeTuruId, opt => opt.MapFrom(src => src.Id))
                                                    .ForMember(dest => dest.ProjeTuru, opt => opt.MapFrom(src => src.Tur)).ReverseMap();

        CreateMap<Proje, ProjeListItemDTO>().ForMember(dest => dest.ProjeId, opt => opt.MapFrom(src => src.Id))
                                            .ForMember(dest => dest.ProjeTuru, opt => opt.MapFrom(src => src.ProjeTuru.Tur))
                                            .ForMember(dest => dest.Yonetmen, opt => opt.MapFrom(src => src.Yetkililer.FirstOrDefault(x => x.YetkiliTipi == (int)ProjeYetkiliTipleri.Yonetmen).YetkiliAdi))
                                            .ForMember(dest => dest.CekimTarihi, opt => opt.MapFrom(src => src.CekimBaslangicTarihi))
                                            .ForMember(dest => dest.CekimYeri, opt => opt.MapFrom(src => src.Sehirler))
                                            .ForMember(dest => dest.ProjeSorumlusu, opt => opt.MapFrom(src => src.Yetkililer.FirstOrDefault(x => x.YetkiliTipi == (int)ProjeYetkiliTipleri.ProjeSorumlusu).YetkiliAdi));

        CreateMap<ProjeDefaultLogo, ProjeDefaultLogoOutputDTO>().ReverseMap();

        CreateMap<RolAgirlikTipi, RolAgirlikTipiDTO>().ReverseMap();
        CreateMap<ProjeRol, ProjeRolCreateDTO>().ReverseMap();
        CreateMap<ProjeRol, ProjeRolOutputDTO>().ForMember(dest => dest.ProjeRolId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<ProjeRol, ProjeRolOpsiyonDetayOutputDTO>().ForMember(dest => dest.ProjeRolId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<ProjeRol, ProjeRolUpdateDTO>().ForMember(dest => dest.ProjeRolId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<ProjeRolPerformer, ProjeRolPerformerCreateDTO>().ReverseMap();

        CreateMap<ProjeRolOzellik, ProjeRolOzellikCreateDTO>().ReverseMap();
        CreateMap<ProjeRolOzellik, ProjeRolOzellikOutputDTO>().ForMember(dest => dest.ProjeRolOzellikId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<ProjeRolOzellik, ProjeRolOzellikUpdateDTO>().ForMember(dest => dest.ProjeRolOzellikId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<RolOzellikFiziksel, RolOzellikFizikselDTO>().ForMember(dest => dest.RolOzellikFizikselId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<RolOzellikDeneyim, RolOzellikDeneyimDTO>().ForMember(dest => dest.RolOzellikDeneyimId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<RolOzellikEgitim, RolOzellikEgitimDTO>().ForMember(dest => dest.RolOzellikEgitimId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<RolOzellikYetenek, RolOzellikYetenekDTO>().ForMember(dest => dest.RolOzellikYetenekId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<RolOzellikPerformerEtiket, RolOzellikPerformerEtiketDTO>().ForMember(dest => dest.RolOzellikPerformerEtiketId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<ProjeRolAnketSorusu, ProjeRolAnketSorusuCreateDTO>().ReverseMap();
        CreateMap<ProjeRolAnketSorusu, ProjeRolAnketSorusuOutputDTO>().ForMember(dest => dest.ProjeRolAnketSorusuId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<ProjeRolAnketSorusu, ProjeOpsiyonRolAnketSorusuOutputDTO>().ReverseMap();
        CreateMap<ProjeRolAnketSorusu, ProjeRolAnketSorusuUpdateDTO>().ForMember(dest => dest.ProjeRolAnketSorusuId, opt => opt.MapFrom(src => src.Id)).ReverseMap();


        CreateMap<ProjeRolOdi, ProjeRolOdiCreateDTO>().ForMember(dest => dest.SonOdilemeSaati, opt => opt.MapFrom(src => src.SonOdilemeTarihi.ToString("HH:mm")))
                                                       .ForMember(dest => dest.SonOdilemeTarihi, opt => opt.MapFrom(src => src.SonOdilemeTarihi.ToString("dd MM yyyy "))).ReverseMap();
        CreateMap<ProjeRolOdi, ProjeRolOdiOutputDTO>().ForMember(dest => dest.ProjeRolOdiId, opt => opt.MapFrom(src => src.Id))
                                                      .ForMember(dest => dest.SonOdilemeSaati, opt => opt.MapFrom(src => src.SonOdilemeTarihi.ToString("HH:mm")))
                                                       .ForMember(dest => dest.SonOdilemeTarihi, opt => opt.MapFrom(src => src.SonOdilemeTarihi.ToString("dd MM yyyy ")))
                                                       .ForMember(dest => dest.SonOdilemeTarihi2, opt => opt.MapFrom(src => src.SonOdilemeTarihi)).ReverseMap();

        CreateMap<RolOdiFotoPoz, RolOdiFotoPozOutputDTO>().ForMember(dest => dest.RolOdiFotoPozId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<RolOdiFotoPoz, RolOdiFotoPozUpdateDTO>().ForMember(dest => dest.RolOdiFotoPozId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<RolOdiFotoPoz, RolOdiFotoPozCreateDTO>().ReverseMap();

        CreateMap<RolOdiFotoOrnekFotograf, RolOdiFotoOrnekFotografCreateDTO>().ReverseMap();
        CreateMap<RolOdiFotoOrnekFotograf, RolOdiFotoOrnekFotografOutputDTO>().ForMember(dest => dest.RolOdiFotoOrnekId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<RolOdiFotoOrnekFotograf, RolOdiFotoOrnekFotografUpdateDTO>().ForMember(dest => dest.RolOdiFotoOrnekId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<RolOdiFotoYonetmenNotu, RolOdiFotoYonetmenNotuOutputDTO>().ForMember(dest => dest.RolOdiFotoYonetmenNotuId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<RolOdiFotoYonetmenNotu, RolOdiFotoYonetmenNotuUpdateDTO>().ForMember(dest => dest.RolOdiFotoYonetmenNotuId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<RolOdiSes, RolOdiSesCreateDTO>().ReverseMap();
        CreateMap<RolOdiSes, RolOdiSesOutputDTO>().ForMember(dest => dest.RolOdiSesId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<RolOdiSes, RolOdiSesUpdateDTO>().ForMember(dest => dest.RolOdiSesId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<RolOdiSesSenaryo, RolOdiSesSenaryoCreateDTO>().ReverseMap();
        CreateMap<RolOdiSesSenaryo, RolOdiSesSenaryoOutputDTO>().ForMember(dest => dest.RolOdiSesSenaryoId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<RolOdiSesSenaryo, RolOdiSesSenaryoUpdateDTO>().ForMember(dest => dest.RolOdiSesSenaryoId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<RolOdiSesYonetmenNotu, RolOdiSesYonetmenNotuCreateDTO>().ReverseMap();
        CreateMap<RolOdiSesYonetmenNotu, RolOdiSesYonetmenNotuOutputDTO>().ForMember(dest => dest.RolOdiSesYonetmenNotuId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<RolOdiSesYonetmenNotu, RolOdiSesYonetmenNotuUpdateDTO>().ForMember(dest => dest.RolOdiSesYonetmenNotuId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<RolOdiVideo, RolOdiVideoCreateDTO>().ReverseMap();
        CreateMap<RolOdiVideo, RolOdiVideoOutputDTO>().ForMember(dest => dest.RolOdiVideoId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<RolOdiVideo, RolOdiVideoUpdateDTO>().ForMember(dest => dest.RolOdiVideoId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<RolOdiVideoDetay, RolOdiVideoDetayCreateDTO>().ReverseMap();
        CreateMap<RolOdiVideoDetay, RolOdiVideoDetayOutputDTO>().ForMember(dest => dest.RolOdiVideoDetayId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<RolOdiVideoDetay, RolOdiVideoDetayUpdateDTO>().ForMember(dest => dest.RolOdiVideoDetayId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<RolOdiVideoOrnekOyun, RolOdiVideoOrnekOyunCreateDTO>().ReverseMap();
        CreateMap<RolOdiVideoOrnekOyun, RolOdiVideoOrnekOyunOutputDTO>().ForMember(dest => dest.RolOdiVideoOrnekOyunId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<RolOdiVideoOrnekOyun, RolOdiVideoOrnekOyunUpdateDTO>().ForMember(dest => dest.RolOdiVideoOrnekOyunId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<RolOdiVideoSenaryo, RolOdiVideoSenaryoCreateDTO>().ReverseMap();
        CreateMap<RolOdiVideoSenaryo, RolOdiVideoSenaryoOutputDTO>().ForMember(dest => dest.RolOdiVideoSenaryoId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<RolOdiVideoSenaryo, RolOdiVideoSenaryoUpdateDTO>().ForMember(dest => dest.RolOdiVideoSenaryoId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<RolOdiVideoYonetmenNotu, RolOdiVideoYonetmenNotuCreateDTO>().ReverseMap();
        CreateMap<RolOdiVideoYonetmenNotu, RolOdiVideoYonetmenNotuOutputDTO>().ForMember(dest => dest.RolOdiVideoYonetmenNotuId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<RolOdiVideoYonetmenNotu, RolOdiVideoYonetmenNotuUpdateDTO>().ForMember(dest => dest.RolOdiVideoYonetmenNotuId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<RolOdiSoru, RolOdiSoruCreateDTO>().ReverseMap();
        CreateMap<RolOdiSoru, RolOdiSoruUpdateDTO>().ForMember(dest => dest.RolOdiSoruId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<RolOdiSoru, RolOdiSoruOutputDTO>().ForMember(dest => dest.RolOdiSoruId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<RolOdiSoruCevapSecenek, RolOdiSoruCevapSecenekCreateDTO>().ReverseMap();
        CreateMap<RolOdiSoruCevapSecenek, RolOdiSoruCevapSecenekUpdateDTO>().ForMember(dest => dest.RolOdiSoruCevapSecenekId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<RolOdiSoruCevapSecenek, RolOdiSoruCevapSecenekOutputDTO>().ForMember(dest => dest.RolOdiSoruCevapSecenekId, opt => opt.MapFrom(src => src.Id)).ReverseMap();


        CreateMap<RolOdiSoruAciklama, RolOdiSoruAciklamaCreateDTO>().ReverseMap();
        CreateMap<RolOdiSoruAciklama, RolOdiSoruAciklamaUpdateDTO>().ForMember(dest => dest.RolOdiSoruAciklamaId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        CreateMap<RolOdiSoruAciklama, RolOdiSoruAciklamaOutputDTO>().ForMember(dest => dest.RolOdiSoruAciklamaId, opt => opt.MapFrom(src => src.Id)).ReverseMap();


        //////////

        CreateMap<ProjeOutputDTO, PerformerProjeDTO>().ForMember(dest => dest.ProjeKapakFotografi, opt => opt.MapFrom(src => src.Fotograf))
                                                     .ForMember(dest => dest.ProjeAdi, opt => opt.MapFrom(src => src.Adi))
                                                      .ForMember(dest => dest.KalanGun, opt => opt.MapFrom(src => (DateTime.Now - Convert.ToDateTime(src.SonOdilemeTarihi.Value)).Days));

        //Kayıtlı Roller için yapılan mapping işlemleri
        CreateMap<KayitliRol, ProjeRol>()
        .ForMember(dest => dest.ProjeId, opt => opt.Ignore())
        .ForMember(dest => dest.RolOzellik, opt => opt.MapFrom(src => src.RolOzellik))
        .ForMember(dest => dest.AnketSorulari, opt => opt.MapFrom(src => src.AnketSorulari))
        .ReverseMap();

        CreateMap<KayitliRolOzellik, ProjeRolOzellik>().ForMember(dest => dest.ProjeRolId, opt => opt.Ignore()).ReverseMap();

        CreateMap<KayitliRolAnketSorusu, ProjeRolAnketSorusu>().ForMember(dest => dest.ProjeRolId, opt => opt.Ignore()).ReverseMap();

        CreateMap<KayitliRolOdiFotoOrnekFotograf, RolOdiFotoOrnekFotograf>().ForMember(dest => dest.ProjeRolOdiId, opt => opt.Ignore()).ReverseMap();

        CreateMap<KayitliRolOdiFotoPoz, RolOdiFotoPoz>().ForMember(dest => dest.ProjeRolOdiId, opt => opt.Ignore()).ReverseMap();

        CreateMap<KayitliRolOdiFotoYonetmenNotu, RolOdiFotoYonetmenNotu>().ForMember(dest => dest.ProjeRolOdiId, opt => opt.Ignore()).ReverseMap();

        CreateMap<KayitliRolOdiSes, RolOdiSes>().ForMember(dest => dest.ProjeRolOdiId, opt => opt.Ignore()).ReverseMap();

        CreateMap<KayitliRolOdiSesSenaryo, RolOdiSesSenaryo>().ForMember(dest => dest.ProjeRolOdiId, opt => opt.Ignore()).ReverseMap();

        CreateMap<KayitliRolOdiSesYonetmenNotu, RolOdiSesYonetmenNotu>().ForMember(dest => dest.ProjeRolOdiId, opt => opt.Ignore()).ReverseMap();

        CreateMap<KayitliRolOdiSoru, RolOdiSoru>()
       .ForMember(dest => dest.ProjeRolOdiId, opt => opt.Ignore())
       .ForMember(dest => dest.CevapSecenekleri, opt => opt.MapFrom(src => src.CevapSecenekleri))
       .ReverseMap();

        CreateMap<KayitliRolOdiSoruCevapSecenek, RolOdiSoruCevapSecenek>().ForMember(dest => dest.RolOdiSoruId, opt => opt.Ignore()).ReverseMap();

        CreateMap<KayitliRolOdiSoruAciklama, RolOdiSoruAciklama>().ForMember(dest => dest.ProjeRolOdiId, opt => opt.Ignore()).ReverseMap();

        CreateMap<KayitliRolOdiVideo, RolOdiVideo>()
        .ForMember(dest => dest.ProjeRolOdiId, opt => opt.Ignore())
        .ForMember(dest => dest.DetayList, opt => opt.MapFrom(src => src.DetayList))
        .ReverseMap();

        CreateMap<KayitliRolOdiVideoDetay, RolOdiVideoDetay>().ForMember(dest => dest.RolOdiVideoId, opt => opt.Ignore()).ReverseMap();

        CreateMap<KayitliRolOdiVideoOrnekOyun, RolOdiVideoOrnekOyun>().ForMember(dest => dest.ProjeRolOdiId, opt => opt.Ignore()).ReverseMap();

        CreateMap<KayitliRolOdiVideoSenaryo, RolOdiVideoSenaryo>().ForMember(dest => dest.ProjeRolOdiId, opt => opt.Ignore()).ReverseMap();

        CreateMap<KayitliRolOdiVideoYonetmenNotu, RolOdiVideoYonetmenNotu>().ForMember(dest => dest.ProjeRolOdiId, opt => opt.Ignore()).ReverseMap();

        //KayitliRoller için base model genel mapping ayarları
        CreateMap<StringBaseModel, KayitliRol>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        CreateMap<StringBaseModel, KayitliRolOzellik>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        CreateMap<StringBaseModel, KayitliRolAnketSorusu>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        CreateMap<StringBaseModel, KayitliRolOdi>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        CreateMap<StringBaseModel, KayitliRolOdiFotoOrnekFotograf>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        CreateMap<StringBaseModel, KayitliRolOdiFotoPoz>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        CreateMap<StringBaseModel, KayitliRolOdiFotoYonetmenNotu>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        CreateMap<StringBaseModel, KayitliRolOdiSes>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        CreateMap<StringBaseModel, KayitliRolOdiSesSenaryo>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        CreateMap<StringBaseModel, KayitliRolOdiSesYonetmenNotu>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        CreateMap<StringBaseModel, KayitliRolOdiSoru>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        CreateMap<StringBaseModel, KayitliRolOdiSoruCevapSecenek>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        CreateMap<StringBaseModel, KayitliRolOdiSoruAciklama>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        CreateMap<StringBaseModel, KayitliRolOdiVideo>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        CreateMap<StringBaseModel, KayitliRolOdiVideoDetay>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        CreateMap<StringBaseModel, KayitliRolOdiVideoOrnekOyun>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        CreateMap<StringBaseModel, KayitliRolOdiVideoSenaryo>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        CreateMap<StringBaseModel, KayitliRolOdiVideoYonetmenNotu>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();

        #region KullaniciBasic

        CreateMap<KullaniciBasic, KullaniciBasicEkleDTO>().ReverseMap();

        #endregion
    }
}