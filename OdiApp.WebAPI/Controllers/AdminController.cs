using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.AuthAttribute;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.BankaDataServices;
using OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.DilDataServices;
using OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.KayitTuruDataServices;
using OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.SabitMetinDataServices;
using OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.SosyalMedyaDataServices;
using OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.TelefonKoduDataServices;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.UygulamaBilgileriDTOs.DilDtos;
using OdiApp.DTOs.UygulamaBilgileriDTOs.KayitGrubuDtos;
using OdiApp.DTOs.UygulamaBilgileriDTOs.TelefonKoduDtos;
using OdiApp.EntityLayer.UygulamaBilgileriModels;

namespace OdiApp.WebAPI.Controllers
{
    [Route("api/admin-uygulama-bilgileri")]
    [ApiController]
    [SuperAdminAuthorize]
    public class AdminController : ControllerBase
    {
        private readonly IGecerliDilService _gecerliDilService;

        private readonly IDilService _dilservice;
        private readonly IKayitTuruService _kayitturuservice;
        private readonly ISosyalMedyaService _sosyalMedyaService;
        private readonly IBankaService _bankaService;
        private readonly ISabitMetinService _sabitMetinService;
        private readonly ITelefonKoduService _telefonKoduService;
        private readonly IAmazonS3Service _amazonS3Service;

        public AdminController(IGecerliDilService gecerliDilService, IDilService dilservice, IKayitTuruService kayitTuruService, ISosyalMedyaService sosyalMedyaService, IBankaService bankaService, ISabitMetinService sabitMetinService, ITelefonKoduService telefonKoduService, IAmazonS3Service amazonS3Service)
        {
            _gecerliDilService = gecerliDilService;
            _dilservice = dilservice;
            _sosyalMedyaService = sosyalMedyaService;
            _kayitturuservice = kayitTuruService;
            _bankaService = bankaService;
            _sabitMetinService = sabitMetinService;
            _telefonKoduService = telefonKoduService;
            _amazonS3Service = amazonS3Service;
        }

        #region Sabit Metin

        [HttpPost("admin-sabit-metin-ekle")]
        public async Task<IActionResult> AdminSabitMetinEkle(SabitMetin model)
        {
            return Ok(OdiResponse<List<SabitMetin>>.Success("Sabit metin eklendi.", await _sabitMetinService.SabitMetinEkle(model), 200));
        }

        [HttpPost("admin-sabit-metin-guncelle")]
        public async Task<IActionResult> AdminSabitMetinGuncelle(SabitMetin model)
        {
            return Ok(OdiResponse<List<SabitMetin>>.Success("Sabit metin güncellendi.", await _sabitMetinService.SabitMetinGuncelle(model), 200));
        }

        [HttpPost("admin-sabit-metin-listesi")]
        public async Task<IActionResult> AdminSabitMetinListesi(DilIdDTO dilIdDTO)
        {
            return Ok(OdiResponse<List<SabitMetin>>.Success("Sabit metin listesi getirildi.", await _sabitMetinService.SabitMetinListesi(dilIdDTO.DilId), 200));
        }

        #endregion

        #region Sosyal Medya

        [HttpPost("admin-sosyal-medya-ekle")]
        public async Task<IActionResult> AdminSosyalMedyaEkle(SosyalMedya model)
        {
            return Ok(OdiResponse<List<SosyalMedya>>.Success("Sosyal medya eklendi.", await _sosyalMedyaService.SosyalMedyaEkle(model), 200));
        }

        [HttpPost("admin-sosyal-medya-guncelle")]
        public async Task<IActionResult> AdminSosyalMedyaGuncelle(SosyalMedya model)
        {
            return Ok(OdiResponse<List<SosyalMedya>>.Success("Sosyal medya güncellendi.", await _sosyalMedyaService.SosyalMedyaGuncelle(model), 200));
        }

        [HttpGet("admin-sosyal-medya-listesi")]
        public async Task<IActionResult> AdminSosyalMedyaListesi()
        {
            return Ok(OdiResponse<List<SosyalMedya>>.Success("Sosyal medya listesi getirildi.", await _sosyalMedyaService.SosyalMedyaListesi(), 200));
        }

        #endregion

        #region Banka

        [HttpPost("admin-banka-ekle")]
        public async Task<IActionResult> AdminBankaEkle(Banka model)
        {
            return Ok(OdiResponse<List<Banka>>.Success("Banka eklendi.", await _bankaService.BankaEkle(model), 200));
        }

        [HttpPost("admin-banka-guncelle")]
        public async Task<IActionResult> AdminBankaGuncelle(Banka model)
        {
            return Ok(OdiResponse<List<Banka>>.Success("Banka güncellendi.", await _bankaService.BankaGuncelle(model), 200));
        }

        [HttpGet("admin-banka-listesi")]
        public async Task<IActionResult> AdminBankaListesi()
        {
            return Ok(OdiResponse<List<Banka>>.Success("Banka listesi getirildi.", await _bankaService.BankaListesi(), 200));
        }

        #endregion

        #region Dil

        [HttpGet("dil-listesi")]
        public async Task<IActionResult> DilListesiGetir()
        {
            return Ok(OdiResponse<List<Dil>>.Success("Dil listesi getirildi.", await _dilservice.DilListesi(), 200));
        }

        [HttpPost("dil-getir")]
        public async Task<IActionResult> DilGetir(DilIdDTO dil)
        {
            return Ok(OdiResponse<Dil>.Success("Dil getirildi.", await _dilservice.DilGetir(dil.DilId), 200));
        }

        [HttpPost("yeni-dil")]
        public async Task<IActionResult> YeniDil(Dil dil)
        {


            await _dilservice.YeniDil(dil);

            return Ok(OdiResponse<List<Dil>>.Success("Dil kaydı oluşturuldu.", await _dilservice.DilListesi(), 200));
        }
        [HttpPost("dil-sil")]
        public async Task<IActionResult> DilSil(DilIdDTO id)
        {
            Dil entity = await _dilservice.DilGetir(id.DilId);

            if (entity == null)
                return Ok(OdiResponse<bool>.Fail("Dil bulunamadı. Silme işlemi başarısız.", "Not Found", 404));
            else
            {
                bool result = await _dilservice.DilSil(entity);
                return Ok(OdiResponse<List<Dil>>.Success("Silme işlemi başarılı.", await _dilservice.DilListesi(), 200));
            }
        }

        [HttpPost("dil-durum-degistir")]
        public async Task<IActionResult> DilDurumDegistir(DilIdDTO id)
        {
            Dil dil = await _dilservice.DilGetir(id.DilId);
            dil.Aktif = dil.Aktif == true ? false : true;
            await _dilservice.DilGuncelle(dil);
            return Ok(OdiResponse<Dil>.Success("Dil durumu güncellendi.", dil, 200));
        }
        #endregion 

        #region Kayıt Grubu

        [HttpGet("kayit-grubu-listesi")]
        public async Task<IActionResult> KayitGrubuListesi(DilIdDTO id)
        {
            int dil = await _gecerliDilService.GecerliDil();
            return Ok(OdiResponse<List<KayitGrubu>>.Success("Kayıt grubu listesi getirildi.", await _kayitturuservice.KayitGrubuListesi(id.DilId), 200));
        }

        [HttpPost("kayit-grubu-getir")]
        public async Task<IActionResult> KayitGrubuGetir(KayitGrubuIdDTO kgr)
        {
            return Ok(OdiResponse<KayitGrubu>.Success("Kayıt grubu getirildi.", await _kayitturuservice.KayitGrubuGetir(kgr.KayitGrubuId), 200));
        }

        [HttpPost("yeni-kayit-grubu")]
        public async Task<IActionResult> YeniKayitGrubu(KayitGrubu kgr)
        {
            await _kayitturuservice.YeniKayitGrubu(kgr);
            return Ok(OdiResponse<List<KayitGrubu>>.Success("Kayıt grubu kaydı oluşturuldu.", await _kayitturuservice.KayitGrubuListesi(kgr.DilId), 200));
        }

        [HttpPost("kayit-grubu-guncelle")]
        public async Task<IActionResult> KayitGrubuGuncelle(KayitGrubu kgr)
        {
            await _kayitturuservice.KayitGrubuGuncelle(kgr);
            return Ok(OdiResponse<List<KayitGrubu>>.Success("Kayıt grubu güncellendi.", await _kayitturuservice.KayitGrubuListesi(kgr.DilId), 200));
        }

        [HttpPost("kayit-grubu-sil")]
        public async Task<IActionResult> KayitGrubuSil(KayitGrubuIdDTO id)
        {
            bool sonuc = await _kayitturuservice.KayitGrubuSil(id.KayitGrubuId);
            int dilId = await _gecerliDilService.GecerliDil();

            if (!sonuc)
                return Ok(OdiResponse<bool>.Fail("Kayıt grubu bulunamadı. Silme işlemi başarısız.", "Not Found", 404));
            else
                return Ok(OdiResponse<List<KayitGrubu>>.Success("Silme işlemi başarılı.", await _kayitturuservice.KayitGrubuListesi(dilId), 200));
        }

        [HttpPost("kayit-grubu-durum-degistir")]
        public async Task<IActionResult> KayitGrubuDurumDegistir(KayitGrubuIdDTO id)
        {
            KayitGrubu kgr = await _kayitturuservice.KayitGrubuGetir(id.KayitGrubuId);
            kgr.Aktif = kgr.Aktif == true ? false : true;
            await _kayitturuservice.KayitGrubuGuncelle(kgr);
            return Ok(OdiResponse<KayitGrubu>.Success("Kayıt Grubu durumu güncellendi.", kgr, 200));
        }
        #endregion

        //#region Kayıt Türü

        //[HttpGet("kayit-turu-listesi")]
        //public async Task<IActionResult> KayitTuruListesi()
        //{
        //    return Ok(OdiResponse<List<KayitTuru>>.Success("Kayıt türü listesi getirildi.", await _adminService.KayitTuruListesi(), 200));
        //}

        //[HttpPost("kayit-turu-getir")]
        //public async Task<IActionResult> KayitTuruGetir(KayitTuruIdDTO model)
        //{
        //    return Ok(OdiResponse<KayitTuru>.Success("Kayıt türü getirildi.", await _adminService.KayitTuruGetir(model.KayitTuruId), 200));
        //}

        //[HttpPost("yeni-kayit-turu")]
        //public async Task<IActionResult> YeniKayitTuru(KayitTuru model)
        //{
        //    return Ok(OdiResponse<KayitTuru>.Success("Kayıt türü kaydı oluşturuldu.", await _adminService.YeniKayitTuru(model), 200));
        //}

        //[HttpPost("kayit-turu-guncelle")]
        //public async Task<IActionResult> KayitTuruGuncelle(KayitTuru model)
        //{
        //    return Ok(OdiResponse<KayitTuru>.Success("Kayıt türü güncellendi.", await _adminService.KayitTuruGuncelle(model), 200));
        //}

        //[HttpPost("kayit-turu-sil")]
        //public async Task<IActionResult> KayitTuruSil(KayitTuruIdDTO model)
        //{
        //    KayitTuru entity = await _adminService.KayitTuruGetir(model.KayitTuruId);

        //    if (entity == null)
        //        return Ok(OdiResponse<bool>.Fail("Kayıt türü bulunamadı. Silme işlemi başarısız.", "Not Found", 404));
        //    else
        //    {
        //        bool result = await _adminService.KayitTuruSil(entity);
        //        return Ok(OdiResponse<bool>.Success("Silme işlemi başarılı.", result, 200));
        //    }
        //}

        //#endregion

        #region Telefon Kodu

        [HttpGet("telefon-kodu-listesi")]
        public async Task<IActionResult> TelefonKoduListesi()
        {
            List<TelefonKodu> list = await _telefonKoduService.TelefonKoduListesi();


            return Ok(OdiResponse<List<TelefonKodu>>.Success("Telefon kodu listesi getirildi.", list, 200));
        }

        [HttpPost("telefon-kodu-getir")]
        public async Task<IActionResult> TelefonKoduGetir(TelefonKoduIdDTO telKod)
        {
            return Ok(OdiResponse<TelefonKodu>.Success("Telefon kodu getirildi.", await _telefonKoduService.TelefonKoduGetir(telKod.TelefonKoduId), 200));
        }

        [HttpPost("yeni-telefon-kodu")]
        public async Task<IActionResult> YeniTelefonKodu(TelefonKodu telKod)
        {
            return Ok(OdiResponse<TelefonKodu>.Success("Telefon kodu kaydı oluşturuldu.", await _telefonKoduService.YeniTelefonKodu(telKod), 200));
        }

        [HttpPost("telefon-kodu-guncelle")]
        public async Task<IActionResult> TelefonKoduGuncelle(TelefonKodu telKod)
        {
            return Ok(OdiResponse<TelefonKodu>.Success("Telefon kodu güncellendi.", await _telefonKoduService.TelefonKoduGuncelle(telKod), 200));
        }

        [HttpPost("telefon-kodu-sil")]
        public async Task<IActionResult> TelefonKoduSil(TelefonKoduIdDTO id)
        {
            TelefonKodu entity = await _telefonKoduService.TelefonKoduGetir(id.TelefonKoduId);

            if (entity == null)
                return Ok(OdiResponse<bool>.Fail("Telefon kodu bulunamadı. Silme işlemi başarısız.", "Not Found", 404));
            else
            {
                bool result = await _telefonKoduService.TelefonKoduSil(entity);
                return Ok(OdiResponse<bool>.Success("Silme işlemi başarılı.", result, 200));
            }
        }

        #endregion
        //[HttpPost("admin-sosyal-medya-ekle")]
        //public async Task<IActionResult> AdminSosyalMedyaEkle(SosyalMedya model)
        //{
        //    return Ok(OdiResponse<List<Dil>>.Success("Dil listesi getirildi.", await _dilservice.DilListesi(), 200));
        //}
        //[HttpPost("admin-sosyal-medya-ekle")]
        //public async Task<IActionResult> AdminSosyalMedyaEkle(SosyalMedya model)
        //{
        //    return Ok(OdiResponse<List<SosyalMedya>>.Success("Sosyal medya eklendi.", await _sosyalMedyaService.SosyalMedyaEkle(model), 200));
        //}

        //[HttpPost("admin-sosyal-medya-guncelle")]
        //public async Task<IActionResult> AdminSosyalMedyaGuncelle(SosyalMedya model)
        //{
        //    return Ok(OdiResponse<List<SosyalMedya>>.Success("Sosyal medya güncellendi.", await _sosyalMedyaService.SosyalMedyaGuncelle(model), 200));
        //}

        //[HttpGet("admin-sosyal-medya-listesi")]
        //public async Task<IActionResult> AdminSosyalMedyaListesi()
        //{
        //    return Ok(OdiResponse<List<SosyalMedya>>.Success("Sosyal medya listesi getirildi.", await _sosyalMedyaService.SosyalMedyaListesi(), 200));
        //}

    }
}
