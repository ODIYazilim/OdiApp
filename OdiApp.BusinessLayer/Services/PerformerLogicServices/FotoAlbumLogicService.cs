using AutoMapper;
using Odi.Shared.Services.Interface;
using OdiApp.DataAccessLayer.PerformerDataServices.FotografAlbum;
using OdiApp.DataAccessLayer.PerformerDataServices.KullaniciBasicDataServices;
using OdiApp.DTOs.PerformerDTOs.FotoAlbumDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.EntityLayer.PerformerModels.FotografAlbum;
using OdiApp.EntityLayer.SharedModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices;

public class FotoAlbumLogicService : IFotoAlbumLogicService
{
    private readonly IFotoAlbumTipiDataService _albumTipiService;
    private readonly IFotoAlbumDataService _albumDataService;
    private readonly IMapper _mapper;
    private readonly IAmazonS3Service _amazonS3Service;
    private readonly IKullaniciBasicDataService _kullaniciBasicDataService;

    public FotoAlbumLogicService(IMapper mapper, IAmazonS3Service amazonS3Service, IFotoAlbumTipiDataService albumTipiService, IFotoAlbumDataService albumDataService, IKullaniciBasicDataService kullaniciBasicDataService)
    {
        _mapper = mapper;
        _albumTipiService = albumTipiService;
        _albumDataService = albumDataService;
        _amazonS3Service = amazonS3Service;
        _kullaniciBasicDataService = kullaniciBasicDataService;
    }

    public async Task<OdiResponse<List<FotoAlbumTipiDTO>>> FotoAlbumTipiListesi()
    {
        return OdiResponse<List<FotoAlbumTipiDTO>>.Success("", _mapper.Map<List<FotoAlbumTipiDTO>>(await _albumTipiService.FotoAlbumTipiListe()), 200);
    }

    #region FOTO ALBUM

    public async Task<OdiResponse<FotoAlbumDTO>> YeniFotoAlbum(FotoAlbumDTO album, OdiUser user)
    {
        FotoAlbum alb = _mapper.Map<FotoAlbum>(album);
        alb.EklenmeTarihi = DateTime.Now;
        alb.Ekleyen = user.AdSoyad;
        alb.EkleyenId = user.Id;

        alb.GuncellenmeTarihi = DateTime.Now;
        alb.Guncelleyen = user.AdSoyad;
        alb.GuncelleyenId = user.Id;

        try
        {
            alb = await _albumDataService.YeniFotoAlbum(alb);
        }
        catch (Exception ex)
        {

            string msg = ex.Message;
        }
        return OdiResponse<FotoAlbumDTO>.Success("Yeni Albüm oluşturuldu", _mapper.Map<FotoAlbumDTO>(alb), 200);
    }

    public async Task<OdiResponse<FotoAlbumDTO>> FotoAlbumGuncel(FotoAlbumDTO album, OdiUser user)
    {
        FotoAlbum alb = new FotoAlbum();
        alb = _mapper.Map(album, alb);
        alb.GuncellenmeTarihi = DateTime.Now;
        alb.Guncelleyen = user.AdSoyad;
        alb.GuncelleyenId = user.Id;

        alb = await _albumDataService.FotoAlbumGuncelle(alb);
        return OdiResponse<FotoAlbumDTO>.Success("Albüm güncellendi", _mapper.Map<FotoAlbumDTO>(alb), 200);
    }

    public async Task<OdiResponse<List<FotoAlbumDTO>>> FotoAlbumListesi(KullaniciIdDTO kullaniciId)
    {
        List<FotoAlbum> list = await _albumDataService.FotoAlbumListe(kullaniciId.KullaniciId);

        foreach (FotoAlbum item in list)
        {
            item.Fotograflar = await _albumDataService.AlbumFotograflari(item.Id);
        }

        foreach (FotoAlbum album in list)
        {
            foreach (var foto in album.Fotograflar)
            {
                foto.DosyaYolu = _amazonS3Service.GetPreSignedUrl(foto.DosyaYolu);
            }
        }

        return OdiResponse<List<FotoAlbumDTO>>.Success("Foto Albüm Listesi getirildi", _mapper.Map<List<FotoAlbumDTO>>(list), 200);
    }

    public async Task<OdiResponse<List<TopluFotoAlbumDTO>>> TopluFotoAlbumListesi(List<KullaniciIdDTO> kullaniciIdList)
    {
        List<KullaniciBasic> kullanicilar = await _kullaniciBasicDataService.KullaniciListesiGetir(kullaniciIdList.Select(s => s.KullaniciId).ToList());

        List<TopluFotoAlbumDTO> resultList = new List<TopluFotoAlbumDTO>();

        foreach (KullaniciBasic kullanici in kullanicilar)
        {
            List<FotoAlbum> list = await _albumDataService.FotoAlbumListe(kullanici.KullaniciId);

            foreach (FotoAlbum item in list)
            {
                item.Fotograflar = await _albumDataService.AlbumFotograflari(item.Id);
            }

            foreach (FotoAlbum album in list)
            {
                foreach (var foto in album.Fotograflar)
                {
                    foto.DosyaYolu = _amazonS3Service.GetPreSignedUrl(foto.DosyaYolu);
                }
            }

            TopluFotoAlbumDTO topluFotoAlbumDTO = new TopluFotoAlbumDTO();

            topluFotoAlbumDTO.KullaniciId = kullanici.KullaniciId;
            topluFotoAlbumDTO.KullaniciAdSoyad = kullanici.KullaniciAdSoyad;
            topluFotoAlbumDTO.KullaniciEmail = kullanici.KullaniciEmail;
            topluFotoAlbumDTO.KullaniciTelefon = kullanici.KullaniciTelefon;
            topluFotoAlbumDTO.Albumler = _mapper.Map<List<FotoAlbumDTO>>(list);

            resultList.Add(topluFotoAlbumDTO);
        }

        return OdiResponse<List<TopluFotoAlbumDTO>>.Success("Kullanıcılara ait foto albüm listesi getirildi.", resultList, 200);
    }

    public async Task<OdiResponse<NoContent>> FotoAlbumSil(FotoAlbumIdDTO fotoAlbumId)
    {
        bool result = await _albumDataService.FotoAlbumSil(fotoAlbumId.FotoAlbumId);
        if (result) return OdiResponse<NoContent>.Success("Fotoğraf albümü silindi", 200);
        else return OdiResponse<NoContent>.Fail("Bu id ile forotğraf albümü bulunamadı", "Not Found", 404);
    }

    #endregion

    #region FOTOĞRAFLAR
    public async Task<OdiResponse<FotoAlbumFotografDTO>> YeniAlbumFotografi(FotoAlbumFotografDTO foto, OdiUser user)
    {
        FotoAlbumFotograf fo = _mapper.Map<FotoAlbumFotograf>(foto);
        fo.EklenmeTarihi = DateTime.Now;
        fo.Ekleyen = user.AdSoyad;
        fo.EkleyenId = user.Id;

        fo.GuncellenmeTarihi = DateTime.Now;
        fo.Guncelleyen = user.AdSoyad;
        fo.GuncelleyenId = user.Id;

        fo = await _albumDataService.YeniAlbumFotografi(fo);
        fo.DosyaYolu = _amazonS3Service.GetPreSignedUrl(foto.DosyaYolu);
        return OdiResponse<FotoAlbumFotografDTO>.Success("Yeni Fotoğraf eklendi", _mapper.Map<FotoAlbumFotografDTO>(fo), 200);
    }

    public async Task<OdiResponse<FotoAlbumFotografDTO>> AlbumFotografiGuncelle(FotoAlbumFotografDTO foto, OdiUser user)
    {
        FotoAlbumFotograf fo = _mapper.Map<FotoAlbumFotograf>(foto);

        fo.GuncellenmeTarihi = DateTime.Now;
        fo.Guncelleyen = user.AdSoyad;
        fo.GuncelleyenId = user.Id;

        fo = await _albumDataService.AlbumFotografiGuncelle(fo);
        fo.DosyaYolu = _amazonS3Service.GetPreSignedUrl(foto.DosyaYolu);
        return OdiResponse<FotoAlbumFotografDTO>.Success(" Fotoğraf güncellendi", _mapper.Map<FotoAlbumFotografDTO>(fo), 200);
    }
    public async Task<OdiResponse<NoContent>> AlbumFotografiSil(FotoAlbumFotografIdDTO fotoId)
    {
        bool result = await _albumDataService.AlbumFotografiSil(fotoId.FotografId);
        if (result) return OdiResponse<NoContent>.Success("Fotoğraf albümü silindi", 200);
        else return OdiResponse<NoContent>.Fail("Bu id ile fotoğraf albümü bulunamadı", "Not Found", 404);
    }

    public async Task<OdiResponse<List<FotoAlbumFotografDTO>>> AlbumFotograflari(FotoAlbumIdDTO fotoAlbumId)
    {
        List<FotoAlbumFotograf> list = await _albumDataService.AlbumFotograflari(fotoAlbumId.FotoAlbumId);
        foreach (var foto in list)
        {
            foto.DosyaYolu = _amazonS3Service.GetPreSignedUrl(foto.DosyaYolu);
        }
        return OdiResponse<List<FotoAlbumFotografDTO>>.Success("Fotoğraf Listesi getirildi", _mapper.Map<List<FotoAlbumFotografDTO>>(list), 200);
    }



    #endregion
}
