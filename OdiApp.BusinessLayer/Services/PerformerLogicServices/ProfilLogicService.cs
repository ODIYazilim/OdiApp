using AutoMapper;
using Microsoft.Extensions.Configuration;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerCVLogicServices;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.PerformerDTOs;
using OdiApp.DTOs.PerformerDTOs.FotoAlbumDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices;

public class ProfilLogicService : IProfilLogicService
{
    private readonly IUseOtherService _useOtherService;
    private readonly IConfiguration _configuration;

    private readonly IPerformerCVLogicService _performerCVLogicService;
    private readonly IFotoAlbumLogicService _fotoAlbumLogicService;
    private readonly IVideoAlbumLogicService _videoAlbumLogicService;
    private readonly IMapper _mapper;
    public ProfilLogicService(IUseOtherService useOtherService, IConfiguration configuration, IPerformerCVLogicService performerCVLogicService, IFotoAlbumLogicService fotoAlbumLogicService, IVideoAlbumLogicService videoAlbumLogicService, IMapper mapper)
    {
        _useOtherService = useOtherService;
        _configuration = configuration;
        _performerCVLogicService = performerCVLogicService;
        _fotoAlbumLogicService = fotoAlbumLogicService;
        _videoAlbumLogicService = videoAlbumLogicService;
        _mapper = mapper;
    }

    public async Task<OdiResponse<ProfilDTO>> PerformerProfiliGetir(PerformerIdDTO id, string jwtToken)
    {
        ProfilDTO profil = new ProfilDTO();

        KullaniciIdDTO kllId = new KullaniciIdDTO { KullaniciId = id.PerformerId };
        dynamic dync = await _useOtherService.PostMethod(kllId, _configuration.GetSection("IdentityServerURL").Value + "/api/user/GetUserInfo", jwtToken);

        profil.PerformerBilgileri = KullaniciBilgileriDoldur(dync);


        //OdiResponse<PerformerCVDTO> cv = await _performerCVLogicService.PerformerCVGetir(new KullaniciIdDTO { KullaniciId = id.PerformerId });
        //profil.PerformerCV = cv.Data != null ? cv.Data : new PerformerCVDTO();

        OdiResponse<List<FotoAlbumDTO>> fotoAlbumList = await _fotoAlbumLogicService.FotoAlbumListesi(new KullaniciIdDTO { KullaniciId = id.PerformerId });
        profil.FotografAlbumleri = fotoAlbumList.Data != null ? fotoAlbumList.Data : new List<FotoAlbumDTO>();

        //OdiResponse<List<VideoAlbumDTO>> videoAlbumList = await _videoAlbumLogicService.VideoAlbumListesi(new KullaniciIdDTO { KullaniciId = id.PerformerId });
        //profil.VideoAlbumleri = videoAlbumList.Data != null ? videoAlbumList.Data : new List<VideoAlbumDTO>();

        return OdiResponse<ProfilDTO>.Success("Performer profil bilgileri getirildi.", profil, 200);
    }

    private KullaniciBilgileriDTO KullaniciBilgileriDoldur(dynamic dync)
    {
        KullaniciBilgileriDTO kll = new KullaniciBilgileriDTO();

        kll.Id = dync.id;
        kll.AdSoyad = dync.adSoyad;
        kll.ProfilFotografi = dync.profilFotografi;
        kll.TelefonNumarasi = dync.telefonNumarasi;
        kll.UlkeTelefonKodu = dync.ulkeTelefonKodu;
        kll.Email = dync.email;
        kll.SonGirisTarihi = dync.sonGirisTarihi;
        kll.KayitTarihi = dync.kayitTarihi;

        return kll;
    }
}