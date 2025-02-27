using AutoMapper;
using Microsoft.Extensions.Configuration;
using OdiApp.BusinessLayer.Core.Services;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.DataAccessLayer.PerformerDataServices.ProfilOnayDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.YetenekTemsilcisiDataServices;
using OdiApp.DTOs.PerformerDTOs.ProfilOnayDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.GlobalDTOs.KullaniciDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.EntityLayer.PerformerModels.ProfilOnayModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices;

public class ProfilOnayLogicService : IProfilOnayLogicService
{
    private readonly IConfiguration _configuration;
    private readonly IProfilOnayDataService _dataService;
    private readonly IMapper _mapper;
    private readonly IAmazonS3Service _amazonS3Service;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IYetenekTemsilcisiDataService _yetenekTemsilcisiDataService;

    public ProfilOnayLogicService(IProfilOnayDataService dataService, IMapper mapper, IConfiguration configuration, IAmazonS3Service amazonS3Service, IHttpClientFactory httpClientFactory, IYetenekTemsilcisiDataService yetenekTemsilcisiDataService)
    {
        _dataService = dataService;
        _mapper = mapper;
        _configuration = configuration;
        _amazonS3Service = amazonS3Service;
        _httpClientFactory = httpClientFactory;
        _yetenekTemsilcisiDataService = yetenekTemsilcisiDataService;
    }

    #region Admin

    public async Task<OdiResponse<NoContent>> ProfilOnayRedNedeniTanimiEkle(ProfilOnayRedNedeniTanimi model, OdiUser user)
    {
        model.EklenmeTarihi = DateTime.Now;
        model.EkleyenId = user.Id;
        model.Ekleyen = user.AdSoyad;

        model.GuncellenmeTarihi = DateTime.Now;
        model.GuncelleyenId = user.Id;
        model.Guncelleyen = user.AdSoyad;

        await _dataService.ProfilOnayRedNedeniTanimiEkle(model);

        return OdiResponse<NoContent>.Success("Profil onay red nedeni tanımı eklendi.", null, 200);
    }

    public async Task<OdiResponse<NoContent>> ProfilOnayRedNedeniTanimiGuncelle(ProfilOnayRedNedeniTanimi model, OdiUser user)
    {
        ProfilOnayRedNedeniTanimi eskiModel = await _dataService.ProfilOnayRedNedeniTanimiGetir(model.Id);

        eskiModel = _mapper.Map(model, eskiModel);

        eskiModel.GuncellenmeTarihi = DateTime.Now;
        eskiModel.GuncelleyenId = user.Id;
        eskiModel.Guncelleyen = user.AdSoyad;

        await _dataService.ProfilOnayRedNedeniTanimiGuncelle(eskiModel);

        return OdiResponse<NoContent>.Success("Profil onay red nedeni tanımı güncellendi.", null, 200);
    }

    public async Task<OdiResponse<List<ProfilOnayRedNedeniTanimiDTO>>> ProfilOnayRedNedeniTanimiListe()
    {
        return OdiResponse<List<ProfilOnayRedNedeniTanimiDTO>>.Success("Profil onay red nedeni tanımları listesi getirildi.", _mapper.Map<List<ProfilOnayRedNedeniTanimiDTO>>(await _dataService.ProfilOnayRedNedeniTanimiListe()), 200);
    }

    public async Task<OdiResponse<NoContent>> ProfilOnayRedNedeniTanimiSil(ProfilOnayRedNedeniTanimiIdDTO id)
    {
        ProfilOnayRedNedeniTanimi model = await _dataService.ProfilOnayRedNedeniTanimiGetir(id.ProfilOnayRedNedeniTanimiId);
        if (model == null) return OdiResponse<NoContent>.Fail("Bu id ile profil onay red nedeni tanımı bulunamadı.", "BadRequest", 400);

        await _dataService.ProfilOnayRedNedeniTanimiSil(model);

        return OdiResponse<NoContent>.Success("Profil onay red nedeni tanımı silindi", null, 200);
    }

    public async Task<OdiResponse<NoContent>> ProfilOnayRedNedeniTanimiDurumDegistir(ProfilOnayRedNedeniTanimiIdDTO id, OdiUser user)
    {
        ProfilOnayRedNedeniTanimi model = await _dataService.ProfilOnayRedNedeniTanimiGetir(id.ProfilOnayRedNedeniTanimiId);

        model.Aktif = !model.Aktif;

        model.GuncellenmeTarihi = DateTime.Now;
        model.Guncelleyen = user.AdSoyad;
        model.GuncelleyenId = user.Id;

        await _dataService.ProfilOnayRedNedeniTanimiGuncelle(model);

        return OdiResponse<NoContent>.Success("Profil onay red nedeni tanımı aktiflik durumu güncellendi.", null, 200);
    }

    #endregion

    #region PERFORMER

    public async Task<OdiResponse<ProfilOnayGonderDTO>> ProfilOnayaGonder(ProfilOnayGonderDTO onayDTO, OdiUser user, string jwt)
    {
        if (string.IsNullOrEmpty(onayDTO.PerformerId.Trim()) || string.IsNullOrEmpty(onayDTO.YetenekTemsilcisiId.Trim())) return OdiResponse<ProfilOnayGonderDTO>.Fail("Profil onaya gönderilemedi.", "Performer veya yetenek temsilcisi id'si geçersiz.", 400);
        if (await _dataService.AcikOnaySureciVarmi(onayDTO.PerformerId)) return OdiResponse<ProfilOnayGonderDTO>.Fail("Kullanıcıya ait onay süreci devam etmektedir.", "Bad Request", 400);
        ProfilOnay onay = _mapper.Map<ProfilOnay>(onayDTO);

        onay.Id = Guid.NewGuid().ToString();
        onay.OnayGonderimTarihi = DateTime.Now;
        onay.EklenmeTarihi = DateTime.Now;
        onay.EkleyenId = user.Id;
        onay.Ekleyen = user.AdSoyad;

        onay.GuncellenmeTarihi = DateTime.Now;
        onay.GuncelleyenId = user.Id;
        onay.Guncelleyen = user.AdSoyad;

        onay.Aktif = true;

        await _dataService.ProfilOnayaGonder(onay);
        onayDTO = _mapper.Map<ProfilOnayGonderDTO>(onay);

        WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
        string url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/kullanici-onay-durum-degistir";

        KullaniciOnayDurumDegistirInputDTO requestModel = new KullaniciOnayDurumDegistirInputDTO();

        requestModel.KullaniciId = onayDTO.PerformerId;
        requestModel.OnaylayanId = user.Id;
        requestModel.OnayliKullanici = false;
        requestModel.Reddedildi = false;
        requestModel.OnayBekliyor = true;

        OdiResponse<NoContent> apiResult = await webApiRequest.Post<NoContent, KullaniciOnayDurumDegistirInputDTO>(url, jwt, requestModel);

        if (!apiResult.IsSuccessful)
        {
            return OdiResponse<ProfilOnayGonderDTO>.Fail("Profil onay işlemi başarısız.", apiResult.Errors, 400);
        }

        return OdiResponse<ProfilOnayGonderDTO>.Success("Profiliniz onaya gönderildi", onayDTO, 200);
    }

    public async Task<OdiResponse<List<ProfilOnayOutputDTO>>> ProfilOnaySureci(PerformerIdDTO id)
    {
        List<ProfilOnayOutputDTO> result = _mapper.Map<List<ProfilOnayOutputDTO>>(await _dataService.ProfilOnaySureci(id.PerformerId));

        result = await ProfilOnayOutputDTORedNedeniDoldur(result);

        return OdiResponse<List<ProfilOnayOutputDTO>>.Success("Profil onay süreci getirildi", result, 200);
    }

    public async Task<OdiResponse<NoContent>> ProfilOnayDurumSorgula(ProfilOnayIdDTO id)
    {
        ProfilOnay onay = await _dataService.ProfilOnayGetir(id.ProfilOnayId);

        string message = "Profil onayı beklemede"; //default message

        if (onay.Aktif && !onay.Incelemede) message = "Profil onayı beklemede.";
        if (onay.Aktif && onay.Incelemede) message = "Profil inceleniyor.";
        if (!onay.Aktif && onay.Red) message = "Profil reddedildi.";
        if (!onay.Aktif && onay.Onay) message = "Profil onaylandı.";

        return OdiResponse<NoContent>.Success(message, null, 200);
    }

    public async Task<OdiResponse<ProfilOnayOutputDTO>> ProfilOnaySonDurumGetir(PerformerIdDTO id)
    {
        return OdiResponse<ProfilOnayOutputDTO>.Success("Profil onay süreci getirildi", _mapper.Map<ProfilOnayOutputDTO>(await _dataService.ProfilOnaySonDurumGetir(id.PerformerId)), 200);
    }

    #endregion

    #region YETENEK TEMSİLCİSİ

    public async Task<OdiResponse<NoContent>> ProfilOnayOnayla(ProfilOnayIdDTO onayId, OdiUser user, string jwt)
    {
        ProfilOnay onay = await _dataService.ProfilOnayGetir(onayId.ProfilOnayId);

        if (onay == null) return OdiResponse<NoContent>.Fail("Kullanıcıya ait onay süreci bulunmamaktadır", "Not Found", 404);

        onay.Onay = true;
        onay.OnaylanmaTarihi = DateTime.Now;
        onay.OnaylayanId = user.Id;

        onay.GuncellenmeTarihi = DateTime.Now;
        onay.Guncelleyen = user.AdSoyad;
        onay.GuncelleyenId = user.Id;

        onay.Aktif = false;

        await _dataService.ProfilOnayGuncelle(onay);

        WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
        string url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/kullanici-onay-durum-degistir";

        KullaniciOnayDurumDegistirInputDTO requestModel = new KullaniciOnayDurumDegistirInputDTO();

        requestModel.KullaniciId = onay.PerformerId;
        requestModel.OnaylayanId = user.Id;
        requestModel.OnayliKullanici = true;
        requestModel.Reddedildi = false;
        requestModel.OnayBekliyor = false;

        OdiResponse<NoContent> apiResult = await webApiRequest.Post<NoContent, KullaniciOnayDurumDegistirInputDTO>(url, jwt, requestModel);

        if (apiResult.IsSuccessful)
        {
            return OdiResponse<NoContent>.Success("Profil onaylandı.", null, 200);
        }
        else
        {
            return OdiResponse<NoContent>.Fail("Profil onaylama işlemi başarısız.", apiResult.Errors, 400);
        }
    }

    public async Task<OdiResponse<NoContent>> ProfilOnayGeriAl(ProfilOnayIdDTO onayId, OdiUser user, string jwt)
    {
        ProfilOnay onay = await _dataService.ProfilOnayGetir(onayId.ProfilOnayId);

        if (onay == null) return OdiResponse<NoContent>.Fail("Kullanıcıya ait onay süreci bulunmamaktadır", "Not Found", 404);

        onay.Onay = false;
        onay.OnaylanmaTarihi = DateTime.Now;
        onay.OnaylayanId = null;
        onay.Incelemede = true;

        onay.GuncellenmeTarihi = DateTime.Now;
        onay.Guncelleyen = user.AdSoyad;
        onay.GuncelleyenId = user.Id;

        onay.Aktif = true;

        await _dataService.ProfilOnayGuncelle(onay);

        WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
        string url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/kullanici-onay-durum-degistir";

        KullaniciOnayDurumDegistirInputDTO requestModel = new KullaniciOnayDurumDegistirInputDTO();

        requestModel.KullaniciId = onay.PerformerId;
        requestModel.OnaylayanId = string.Empty;
        requestModel.OnayliKullanici = false;
        requestModel.Reddedildi = false;
        requestModel.OnayBekliyor = false;

        OdiResponse<NoContent> apiResult = await webApiRequest.Post<NoContent, KullaniciOnayDurumDegistirInputDTO>(url, jwt, requestModel);

        if (apiResult.IsSuccessful)
        {
            return OdiResponse<NoContent>.Success("Profil onay geri alındı.", null, 200);
        }
        else
        {
            return OdiResponse<NoContent>.Fail("Profil onay geri alma işlemi başarısız.", apiResult.Errors, 400);
        }
    }

    public async Task<OdiResponse<NoContent>> ProfilOnayRed(ProfilOnayRedInputDTO red, OdiUser user, string jwt)
    {
        ProfilOnay onay = await _dataService.ProfilOnayGetir(red.ProfilOnayId);
        if (onay == null) return OdiResponse<NoContent>.Fail("Kullanıcıya ait onay süreci bulunmamaktadır", "Not Found", 404);

        onay = _mapper.Map(red, onay);

        onay.ReddedenId = user.Id;
        onay.RedTarihi = DateTime.Now;
        onay.Red = true;

        onay.GuncellenmeTarihi = DateTime.Now;
        onay.GuncelleyenId = user.Id;
        onay.Guncelleyen = user.AdSoyad;

        onay.Aktif = false; //onay süreci sonlandı.

        await _dataService.ProfilOnayGuncelle(onay);

        if (red.OnTanimliRedSebepleriIdListesi?.Any() == true)
        {
            List<ProfilOnayRedNedeni> redNedeniList = new List<ProfilOnayRedNedeni>();

            foreach (int tanimliRedSebebiId in red.OnTanimliRedSebepleriIdListesi)
            {
                ProfilOnayRedNedeniTanimi redNedeni = await _dataService.ProfilOnayRedNedeniTanimiGetir(tanimliRedSebebiId);

                if (redNedeni != null)
                {
                    ProfilOnayRedNedeni onayRedNedeni = new ProfilOnayRedNedeni
                    {
                        ProfilOnayId = onay.Id,
                        ProfilOnayRedNedeniTanimiId = redNedeni.Id,
                        EklenmeTarihi = DateTime.Now,
                        EkleyenId = user.Id,
                        Ekleyen = user.AdSoyad
                    };

                    redNedeniList.Add(onayRedNedeni);
                }
            }

            if (redNedeniList.Any())
                await _dataService.ProfilOnayRedNedeniTopluEkle(redNedeniList);
        }

        WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
        string url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/kullanici-onay-durum-degistir";

        KullaniciOnayDurumDegistirInputDTO requestModel = new KullaniciOnayDurumDegistirInputDTO();

        requestModel.KullaniciId = onay.PerformerId;
        requestModel.OnaylayanId = user.Id;
        requestModel.OnayliKullanici = false;
        requestModel.Reddedildi = true;
        requestModel.OnayBekliyor = false;

        OdiResponse<NoContent> apiResult = await webApiRequest.Post<NoContent, KullaniciOnayDurumDegistirInputDTO>(url, jwt, requestModel);

        if (apiResult.IsSuccessful)
        {
            return OdiResponse<NoContent>.Success("Profil reddedildi.", null, 200);
        }
        else
        {
            return OdiResponse<NoContent>.Fail("Profil red işlemi başarısız.", apiResult.Errors, 400);
        }

    }

    public async Task<OdiResponse<NoContent>> ProfilOnayIncele(ProfilOnayIdDTO onayId, OdiUser user, string jwt)
    {
        ProfilOnay onay = await _dataService.ProfilOnayGetir(onayId.ProfilOnayId);
        if (onay == null) return OdiResponse<NoContent>.Fail("Kullanıcıya ait onay süreci bulunmamaktadır", "Not Found", 404);

        onay.Incelemede = true;
        onay.IncelemeTarihi = DateTime.Now;
        onay.InceleyenId = user.Id;
        onay.GuncellenmeTarihi = DateTime.Now;
        onay.Guncelleyen = user.AdSoyad;
        onay.GuncelleyenId = user.Id;

        onay = await _dataService.ProfilOnayGuncelle(onay);

        WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
        string url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/kullanici-onay-durum-degistir";

        KullaniciOnayDurumDegistirInputDTO requestModel = new KullaniciOnayDurumDegistirInputDTO();

        requestModel.KullaniciId = onay.PerformerId;
        requestModel.OnaylayanId = user.Id;
        requestModel.OnayliKullanici = false;
        requestModel.Reddedildi = false;
        requestModel.OnayBekliyor = true;

        OdiResponse<NoContent> apiResult = await webApiRequest.Post<NoContent, KullaniciOnayDurumDegistirInputDTO>(url, jwt, requestModel);

        if (!apiResult.IsSuccessful)
        {
            return OdiResponse<NoContent>.Fail("Profil onay işlemi başarısız.", apiResult.Errors, 400);
        }

        return OdiResponse<NoContent>.Success("Profil inceleniyor.", null, 200);
    }

    //public async Task<OdiResponse<List<ProfilOnayOutputDTO>>> TalepListesi(TalepListesiInputDTO dto, string jwt)
    //{
    //    List<ProfilOnay> list = new List<ProfilOnay>();

    //    // Tüm listeleri alıyoruz
    //    List<ProfilOnay> acikTalepList = await _dataService.AcikTalepListesi(dto.MenajerId);
    //    List<ProfilOnay> redListesi = await _dataService.RedProfilOnayListesi(dto.MenajerId);
    //    List<ProfilOnay> onayListesi = await _dataService.OnayliProfilOnayListesi(dto.MenajerId);

    //    list.AddRange(acikTalepList);
    //    list.AddRange(redListesi);
    //    list.AddRange(onayListesi);

    //    // Yeni removeList oluşturuluyor
    //    List<ProfilOnay> removeList = new List<ProfilOnay>();

    //    // PerformerId'ye göre gruplama
    //    var performerGruplari = list.GroupBy(x => x.PerformerId);

    //    // Her bir PerformerId için işlemler
    //    foreach (var grup in performerGruplari)
    //    {
    //        // İlgili performer'ın kayıtlarını GuncellenmeTarihi'ne göre tersten sırala
    //        var sortedRecords = grup.OrderByDescending(x => x.GuncellenmeTarihi).ToList();

    //        // Sonuncu kaydı al
    //        var sonKayit = sortedRecords.FirstOrDefault();

    //        if (sonKayit != null)
    //        {
    //            // Eğer son kayıt Red = true ise
    //            if (sonKayit.Red)
    //            {
    //                removeList.AddRange(grup.Where(x => x.Onay == true));
    //            }

    //            // Eğer son kayıt Onay = true ise
    //            else if (sonKayit.Onay)
    //            {
    //                removeList.AddRange(grup.Where(x => x.Red == true));
    //            }
    //        }

    //        if (!dto.ReddedilenleriGetir)
    //        {
    //            removeList.AddRange(grup.Where(x => x.Red == true));
    //        }

    //        if (!dto.OnaylananlariGetir && !dto.OnayliPasifleriGetir)
    //        {
    //            removeList.AddRange(grup.Where(x => x.Onay == true));
    //        }
    //    }

    //    if (!dto.AcikTalepleriGetir)
    //    {
    //        removeList.AddRange(list.Where(x => x.Incelemede == true && x.Onay == false && x.Red == false));
    //    }

    //    // removeList boş değilse, list içerisinden bu kayıtları çıkar
    //    if (removeList.Any())
    //    {
    //        list = list.Except(removeList).ToList();
    //    }

    //    List<ProfilOnayOutputDTO> result = new List<ProfilOnayOutputDTO>();

    //    if (list != null)
    //    {
    //        result.AddRange(_mapper.Map<List<ProfilOnayOutputDTO>>(list));

    //        //performer işlemleri
    //        List<string> performerIdList = list.Select(x => x.PerformerId).ToList();

    //        WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
    //        string url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/performer-bilgileri-liste";
    //        OdiResponse<List<KullaniciBilgileriDTO>> apiResult = await webApiRequest.Post<List<KullaniciBilgileriDTO>, List<string>>(url, jwt, performerIdList);

    //        //Sonuç listesinden kaldırılacak kayıtlar.
    //        //Bu kayıtları ayıklamak için ilk önce performer'ın son giriş tarihinin alınıp kontrol edilmesi gerekiyor.
    //        //Bu sebeple listeye alınan kayıtlar kontrollerden sonra ayıklanıp listeden çıkartılması gerektiği için remove list kullanımı yapıldı.
    //        List<ProfilOnayOutputDTO> resultRemoveList = new List<ProfilOnayOutputDTO>();

    //        if (apiResult.IsSuccessful)
    //        {
    //            foreach (ProfilOnayOutputDTO talep in result)
    //            {
    //                talep.Performer = apiResult.Data.Where(x => x.Id == talep.PerformerId).FirstOrDefault();

    //                if (talep.Performer != null)
    //                {
    //                    talep.OnayliPasif = talep.Performer.SonGirisTarihi < DateTime.Now.AddMonths(-3);

    //                    if (
    //                        dto.OnayliPasifleriGetir &&
    //                        (!talep.OnayliPasif && !dto.OnaylananlariGetir)
    //                    )
    //                    {
    //                        //Performer varsa, son giriş tarihine bakılır.
    //                        //Onayli pasifler isteniyorsa, pasif olmayan kullanıcı alınır kaldırılacaklar listesine eklenir.
    //                        //Eğer onaylılar ve pasifler birlikte istenirse pasif olan olmayan hepsi getirilir. Çünkü hepsi aslında onaylı kullanıcıdır.

    //                        resultRemoveList.Add(talep);

    //                        continue; //zaten kaldırılacak, o yüzden bir sonraki işleme geç.
    //                    }

    //                    PerformerMenajerListItemOutputDTO menajer = await _yetenekTemsilcisiDataService.PerformerMenajerGetir(talep.PerformerId);

    //                    if (menajer != null)
    //                    {
    //                        talep.Performer.MenajerId = menajer.MenajerId;
    //                        talep.Performer.MenajerAdSoyad = menajer.MenajerAdSoyad;
    //                    }
    //                }
    //            }

    //            //red nedenleri
    //            result = await ProfilOnayOutputDTORedNedeniDoldur(result.Except(resultRemoveList).ToList());
    //        }
    //        else
    //        {
    //            return OdiResponse<List<ProfilOnayOutputDTO>>.Fail("Kullanıcı bilgileri alınamadı.", apiResult.Errors, 400);
    //        }
    //    }

    //    return OdiResponse<List<ProfilOnayOutputDTO>>.Success("Profil talepleri listelendi.", result, 200);
    //}

    //public async Task<OdiResponse<TalepSayisiOutputDTO>> TalepSayisi(MenajerIdDTO dto, string jwt)
    //{
    //    TalepSayisiOutputDTO result = new TalepSayisiOutputDTO();

    //    result.AcikTalepSayisi = await _dataService.AcikTalepSayisi(dto.MenajerId);
    //    result.RedTalepSayisi = await _dataService.RedTalepSayisi(dto.MenajerId);
    //    result.OnayliProfilSayisi = await _dataService.OnayliProfilSayisi(dto.MenajerId);

    //    List<ProfilOnay> onayListesi = await _dataService.OnayliProfilOnayListesi(dto.MenajerId);

    //    if (onayListesi?.Any() == true)
    //    {
    //        List<ProfilOnayOutputDTO> onayListeOutput = _mapper.Map<List<ProfilOnayOutputDTO>>(onayListesi);

    //        //performer işlemleri
    //        List<string> performerIdList = onayListesi.Select(x => x.PerformerId).ToList();

    //        WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
    //        string url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/performer-bilgileri-liste";
    //        OdiResponse<List<KullaniciBilgileriDTO>> apiResult = await webApiRequest.Post<List<KullaniciBilgileriDTO>, List<string>>(url, jwt, performerIdList);

    //        List<ProfilOnayOutputDTO> resultRemoveList = new List<ProfilOnayOutputDTO>();

    //        if (apiResult.IsSuccessful)
    //        {
    //            foreach (ProfilOnayOutputDTO talep in onayListeOutput)
    //            {
    //                talep.Performer = apiResult.Data.Where(x => x.Id == talep.PerformerId).FirstOrDefault();

    //                if (talep.Performer != null)
    //                {
    //                    talep.OnayliPasif = talep.Performer.SonGirisTarihi < DateTime.Now.AddMonths(-3);

    //                    if (!talep.OnayliPasif)
    //                    {
    //                        resultRemoveList.Add(talep);
    //                    }
    //                }
    //            }

    //            onayListeOutput = onayListeOutput.Except(resultRemoveList).ToList();

    //            result.OnayliPasifSayisi = onayListeOutput.Count();
    //        }
    //        else
    //        {
    //            return OdiResponse<TalepSayisiOutputDTO>.Fail("Kullanıcı bilgileri alınamadı.", apiResult.Errors, 400);
    //        }
    //    }

    //    return OdiResponse<TalepSayisiOutputDTO>.Success("Talep sayıları getirildi.", result, 200);
    //}

    #endregion

    #region Private methods

    private async Task<List<ProfilOnayOutputDTO>> ProfilOnayOutputDTORedNedeniDoldur(List<ProfilOnayOutputDTO> dtoList)
    {
        List<ProfilOnayRedNedeni> profilOnayRedNedeniListesi = await _dataService.ProfilOnayRedNedeniListe(dtoList.Select(x => x.ProfilOnayId).ToList());
        List<ProfilOnayRedNedeniTanimi> profilOnayRedNedeniTanimListesi = await _dataService.ProfilOnayRedNedeniTanimiListe(profilOnayRedNedeniListesi.Select(x => x.ProfilOnayRedNedeniTanimiId).ToList());

        foreach (ProfilOnayOutputDTO talep in dtoList)
        {
            List<ProfilOnayRedNedeni> profilOnayRedNedeniListe = profilOnayRedNedeniListesi.Where(x => x.ProfilOnayId == talep.ProfilOnayId).ToList();

            if (profilOnayRedNedeniListe?.Any() == true)
            {
                talep.RedNedeniListesi = new List<ProfilOnayRedNedeniOutputDTO>();

                foreach (ProfilOnayRedNedeni item in profilOnayRedNedeniListe)
                {
                    ProfilOnayRedNedeniOutputDTO profilOnayRedNedeniOutputDTO = new ProfilOnayRedNedeniOutputDTO();

                    profilOnayRedNedeniOutputDTO.ProfilOnayRedNedeniTanimiId = item.ProfilOnayRedNedeniTanimiId;
                    profilOnayRedNedeniOutputDTO.RedMetni = profilOnayRedNedeniTanimListesi.FirstOrDefault(x => x.Id == item.ProfilOnayRedNedeniTanimiId)?.RedMetni ?? string.Empty;

                    talep.RedNedeniListesi.Add(profilOnayRedNedeniOutputDTO);
                }
            }
        }

        return dtoList;
    }

    #endregion
}