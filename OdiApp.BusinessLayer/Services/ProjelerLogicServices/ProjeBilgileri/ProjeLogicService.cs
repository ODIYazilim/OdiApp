using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OdiApp.BusinessLayer.Core.Services;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.ProjelerLogicServices.ProjeRolBilgileri;
using OdiApp.DataAccessLayer.ProjelerDataServices.ProjeBilgileri;
using OdiApp.DataAccessLayer.ProjelerDataServices.ProjeRolBilgileri;
using OdiApp.DTOs.Enums;
using OdiApp.DTOs.ProjelerDTOs.PerformerProje;
using OdiApp.DTOs.ProjelerDTOs.ProjeBilgileriDTOs;
using OdiApp.DTOs.ProjelerDTOs.ProjeRolBilgileri.ProjeRolDTO;
using OdiApp.DTOs.ProjelerDTOs.ProjeTurleri;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.IdentityDTOs.FirmaDTOs;
using OdiApp.DTOs.SharedDTOs.IdentityDTOs.UserDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeBilgileriDTOs;
using OdiApp.EntityLayer.ProjelerModels.ProjeBilgileri;

namespace OdiApp.BusinessLayer.Services.ProjelerLogicServices.ProjeBilgileri
{
    public class ProjeLogicService : IProjeLogicService
    {
        private readonly IProjeDataService _projeDataService;
        private readonly IProjeRolLogicService _projeRolLogicService;
        private readonly IProjeRolDataService _projeRolDataService;
        private readonly IUseOtherService _useOtherService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IAmazonS3Service _amazonS3Service;
        private readonly IHttpClientFactory _httpClientFactory;

        public ProjeLogicService(IProjeDataService projeDataService, IProjeRolDataService rolDataService, IProjeRolLogicService projeRolLogicService, IMapper mapper, IAmazonS3Service amazonS3Service, IUseOtherService useOtherService, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _projeDataService = projeDataService;
            _projeRolLogicService = projeRolLogicService;
            _projeRolDataService = rolDataService;
            _mapper = mapper;
            _amazonS3Service = amazonS3Service;
            _useOtherService = useOtherService;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<OdiResponse<ProjeAyarlariDTO>> ProjeAyarlariGetir(int dilId, string jwtToken)
        {
            ProjeAyarlariDTO ayarlar = new ProjeAyarlariDTO();
            List<ProjeTuru> turler = await _projeDataService.ProjeTuruListe(dilId);
            if (turler != null) ayarlar.ProjeTurleri = _mapper.Map<List<ProjeTuruOutputDTO>>(turler);

            List<ProjeDefaultLogo> defaultLogolar = await _projeDataService.ProjeDefaultLogoListe();
            if (defaultLogolar != null) ayarlar.LogoListesi = _mapper.Map<List<ProjeDefaultLogoOutputDTO>>(defaultLogolar);

            List<ProjeKatilimBolgesi> projeKatilimBolgeleri = await _projeDataService.ProjeKatilimBolgesiListe();
            if (projeKatilimBolgeleri != null) ayarlar.ProjeKatilimBolgeleri = _mapper.Map<List<ProjeKatilimBolgesiOutputDTO>>(projeKatilimBolgeleri);

            WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
            string url = _configuration.GetSection("IdentityServerUrl").Value + "/api/firma/birlestirilmis-yapim-firma-listesi";

            OdiResponse<List<BirlestirilmisFirmaListeOutputDTO>> firmaListesiApiResult = await webApiRequest.Get<List<BirlestirilmisFirmaListeOutputDTO>>(url, jwtToken);

            if (!firmaListesiApiResult.IsSuccessful)
                return OdiResponse<ProjeAyarlariDTO>.Fail("Bir sorun oluştu.", "Firma listeleri getirilirken sorun oluştu. Hatalar: " + string.Join(",", firmaListesiApiResult.Errors), 500);

            if (firmaListesiApiResult.Data != null)
                ayarlar.YapimciFirmaListesi = firmaListesiApiResult.Data;

            url = _configuration.GetSection("IdentityServerUrl").Value + "/api/user/projeyapimyetkililistesi";

            OdiResponse<List<YapimListItemDTO>> yetkiliListesiApiResult = await webApiRequest.Get<List<YapimListItemDTO>>(url, jwtToken);

            if (!yetkiliListesiApiResult.IsSuccessful)
                return OdiResponse<ProjeAyarlariDTO>.Fail("Bir sorun oluştu.", "Yetkili listeleri getirilirken sorun oluştu. Hatalar: " + string.Join(",", yetkiliListesiApiResult.Errors), 500);

            if (yetkiliListesiApiResult.Data != null)
                ayarlar.YetkiliListesi = yetkiliListesiApiResult.Data;

            return OdiResponse<ProjeAyarlariDTO>.Success("Proje Ayarları Getirildi", ayarlar, 200);
        }

        public async Task<OdiResponse<ProjeOutputDTO>> ProjeGetir(ProjeIdDTO id)
        {
            ProjeOutputDTO proje = await this.ProjeGetir(id.ProjeId);
            if (proje.ProjeId == null) return OdiResponse<ProjeOutputDTO>.Fail("Bu Id ile proje bulumamaktadır", "Bad Request", 400);
            return OdiResponse<ProjeOutputDTO>.Success("Proje Getirildi", proje, 200);
        }

        private async Task<ProjeOutputDTO> ProjeGetir(string projeId)
        {
            Proje proje = await _projeDataService.ProjeGetir(projeId);
            if (proje == null) return new ProjeOutputDTO();

            if (!string.IsNullOrEmpty(proje.Fotograf)) proje.Fotograf = _amazonS3Service.GetPreSignedUrl(proje.Fotograf);
            //List<ProjeRolOutputDTO> roller = await _projeRolLogicService.ProjeRolleriGetir(projeId);
            ProjeOutputDTO projeDTO = _mapper.Map<ProjeOutputDTO>(proje);
            //if (roller != null) projeDTO.Roller = roller;
            return projeDTO;
        }

        public async Task<OdiResponse<ProjeOutputDTO>> YeniProje(ProjeCreateDTO proje, OdiUser user, int dil, string jwtToken)
        {
            Proje prj = _mapper.Map<Proje>(proje);
            prj.DilId = dil;

            prj.Ekleyen = user.AdSoyad;
            prj.EkleyenId = user.Id;
            prj.EklenmeTarihi = DateTime.Now;

            prj.Guncelleyen = user.AdSoyad;
            prj.GuncelleyenId = user.Id;
            prj.GuncellenmeTarihi = DateTime.Now;

            if (prj.Yetkililer != null)
            {
                foreach (var yet in prj.Yetkililer)
                {
                    yet.Ekleyen = user.AdSoyad;
                    yet.EkleyenId = user.Id;
                    yet.EklenmeTarihi = DateTime.Now;
                    yet.Guncelleyen = user.AdSoyad;
                    yet.GuncelleyenId = user.Id;
                    yet.GuncellenmeTarihi = DateTime.Now;
                }
            }

            prj = await _projeDataService.YeniProje(prj);

            if (string.IsNullOrEmpty(proje.YapimciFirmaKodu))
            {
                WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
                string url = _configuration.GetSection("IdentityServerUrl").Value + "/api/firma/onerilen-firma-ekle";

                OnerilenFirmaCreateDTO requestModel = new OnerilenFirmaCreateDTO();

                requestModel.FirmaAdi = proje.YapimciFirmaAdi;
                requestModel.OneriYeri = OnerilenFirmaOneriYeri.ProjeBilgileri;

                OdiResponse<bool> onerilenFirmaEkleApiResult = await webApiRequest.Post<bool, OnerilenFirmaCreateDTO>(url, jwtToken, requestModel);

                if (!onerilenFirmaEkleApiResult.IsSuccessful)
                    return OdiResponse<ProjeOutputDTO>.Fail("Bir sorun oluştu.", "Önerilen firma kayıt ediliemedi. Hatalar:" + string.Join(";", onerilenFirmaEkleApiResult.Errors), 500);
            }

            return OdiResponse<ProjeOutputDTO>.Success("Yeni Proje Oluşturuldu", await this.ProjeGetir(prj.Id), 200);
        }

        public async Task<OdiResponse<ProjeOutputDTO>> ProjeGuncelle(ProjeUpdateDTO proje, OdiUser user, string jwtToken)
        {
            Proje prj = await _projeDataService.ProjeGetir(proje.ProjeId);
            List<ProjeYetkili> eskiYetkiliListesi = prj.Yetkililer;
            List<ProjeYetkili> guncellenecekYetkiliListesi = new List<ProjeYetkili>();
            List<ProjeYetkili> yeniYetkiliListesi = new List<ProjeYetkili>();

            if (prj == null) return OdiResponse<ProjeOutputDTO>.Fail("Bu ait e sahip bir proje bulunamaktadır", "Bad Request", 400);

            prj = _mapper.Map(proje, prj);

            prj.Guncelleyen = user.AdSoyad;
            prj.GuncelleyenId = user.Id;
            prj.GuncellenmeTarihi = DateTime.Now;

            prj = await _projeDataService.ProjeGuncelle(prj);

            guncellenecekYetkiliListesi = _mapper.Map<List<ProjeYetkili>>(proje.Yetkililer);

            List<ProjeYetkili> silinenYetkililer = new List<ProjeYetkili>();

            foreach (var item in eskiYetkiliListesi)
            {
                if (!guncellenecekYetkiliListesi.Any(x => x.Id == item.Id)) silinenYetkililer.Add(item);
            }
            if (silinenYetkililer != null)
            {
                await _projeDataService.ProjeYetkiliListesiSil(silinenYetkililer);
            }

            if (proje.YeniYetkililer != null)
            {
                yeniYetkiliListesi = _mapper.Map<List<ProjeYetkili>>(proje.YeniYetkililer);

                foreach (var yet in yeniYetkiliListesi)
                {
                    yet.Ekleyen = user.AdSoyad;
                    yet.EkleyenId = user.Id;
                    yet.EklenmeTarihi = DateTime.Now;
                    yet.Guncelleyen = user.AdSoyad;
                    yet.GuncelleyenId = user.Id;
                    yet.GuncellenmeTarihi = DateTime.Now;
                }

                yeniYetkiliListesi = await _projeDataService.ProjeYeniYetkiliListesi(yeniYetkiliListesi);
            }

            if (string.IsNullOrEmpty(proje.YapimciFirmaKodu))
            {
                WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
                string url = _configuration.GetSection("IdentityServerUrl").Value + "/api/firma/onerilen-firma-ekle";

                OnerilenFirmaCreateDTO requestModel = new OnerilenFirmaCreateDTO();

                requestModel.FirmaAdi = proje.YapimciFirmaAdi;
                requestModel.OneriYeri = OnerilenFirmaOneriYeri.ProjeBilgileri;

                OdiResponse<bool> onerilenFirmaEkleApiResult = await webApiRequest.Post<bool, OnerilenFirmaCreateDTO>(url, jwtToken, requestModel);

                if (!onerilenFirmaEkleApiResult.IsSuccessful)
                    return OdiResponse<ProjeOutputDTO>.Fail("Bir sorun oluştu.", "Önerilen firma kayıt ediliemedi. Hatalar:" + string.Join(";", onerilenFirmaEkleApiResult.Errors), 500);
            }

            return OdiResponse<ProjeOutputDTO>.Success("Proje Güncellendi", await this.ProjeGetir(prj.Id), 200);
        }

        public async Task<OdiResponse<string>> ProjeFotografiGuncelle(ProjeFotografiUpdateDTO foto)
        {
            Proje proje = await _projeDataService.ProjeGetir(foto.ProjeId);
            proje.Fotograf = foto.DosyaYolu;
            proje.FotografAdi = foto.FotografAdi;
            await _projeDataService.ProjeGuncelle(proje);

            string dosyalink = _amazonS3Service.GetPreSignedUrl(proje.Fotograf);
            return OdiResponse<string>.Success("Proje Fotoğrafı güncellendi", dosyalink, 200);
        }

        public async Task<OdiResponse<List<string>>> ProjeFotografArama(ProjeFotografAramaDto fotoAdi)
        {
            List<string> list = await _projeDataService.ProjeFotografiArama(fotoAdi.FotografAdi);
            if (list == null) return OdiResponse<List<string>>.Fail("Fotoğraf Bulunamadı", "Not Found", 404);
            List<string> urlList = new List<string>();

            foreach (string item in list)
            {
                urlList.Add(_amazonS3Service.GetPreSignedUrl(item));
            }

            return OdiResponse<List<string>>.Success("Bulunan Fotoğraflar getirildi", urlList, 200);
        }

        public Task<OdiResponse<bool>> ProjeSil(ProjeIdDTO id)
        {
            throw new NotImplementedException();
        }

        public async Task<OdiResponse<bool>> ProjeKayitAyarlari(ProjeKayitAyarlariDTO model, OdiUser user)
        {
            Proje proje = await _projeDataService.ProjeGetir(model.ProjeId);
            if (proje == null) return OdiResponse<bool>.Fail("Bu Id ile proje bulumamaktadır", "Bad Request", 400);

            proje.Guncelleyen = user.AdSoyad;
            proje.GuncelleyenId = user.Id;
            proje.GuncellenmeTarihi = DateTime.Now;

            proje.Gizli = model.Gizli;
            proje.Acil = model.Acil;

            await _projeDataService.ProjeGuncelle(proje);

            return OdiResponse<bool>.Success("Proje kayıt ayarları güncellendi.", true, 200);
        }

        public async Task<OdiResponse<bool>> ProjeYayinaAl(ProjeIdDTO model, OdiUser user)
        {
            Proje proje = await _projeDataService.ProjeGetir(model.ProjeId);
            if (proje == null) return OdiResponse<bool>.Fail("Bu Id ile proje bulumamaktadır", "Bad Request", 400);

            proje.Guncelleyen = user.AdSoyad;
            proje.GuncelleyenId = user.Id;
            proje.GuncellenmeTarihi = DateTime.Now;

            proje.Durum = true;

            await _projeDataService.ProjeGuncelle(proje);

            return OdiResponse<bool>.Success("Proje yayın ayarları güncellendi.", true, 200);
        }

        public async Task<OdiResponse<bool>> ProjeYayiniDurdurma(ProjeIdDTO model, OdiUser user)
        {
            Proje proje = await _projeDataService.ProjeGetir(model.ProjeId);
            if (proje == null) return OdiResponse<bool>.Fail("Bu Id ile proje bulumamaktadır", "Bad Request", 400);

            proje.Guncelleyen = user.AdSoyad;
            proje.GuncelleyenId = user.Id;
            proje.GuncellenmeTarihi = DateTime.Now;

            proje.Durum = false;

            await _projeDataService.ProjeGuncelle(proje);

            return OdiResponse<bool>.Success("Proje yayın ayarları güncellendi.", true, 200);
        }

        #region Proje Listeleri

        public async Task<OdiResponse<List<ProjeOutputDTO>>> TumAcikProjeler(int dilId)
        {
            List<Proje> projeListesi = await _projeDataService.TumProjeler(dilId);
            List<ProjeOutputDTO> projeDtoList = _mapper.Map<List<ProjeOutputDTO>>(projeListesi);

            foreach (ProjeOutputDTO item in projeDtoList)
            {
                if (!string.IsNullOrEmpty(item.Fotograf)) item.Fotograf = _amazonS3Service.GetPreSignedUrl(item.Fotograf);
            }

            return OdiResponse<List<ProjeOutputDTO>>.Success("Tüm Projeler Listelendi", projeDtoList, 200);
        }

        public async Task<OdiResponse<List<PerformerProjeDTO>>> PerformerProjeListesi(PerformerIdDTO id, string jwtToken, int dilId)
        {
            var dynamicData = await _useOtherService.PostMethod(id, _configuration.GetSection("IslemlerServerUrl").Value + "/performer-islem-listesi", jwtToken);

            dynamicData = dynamicData.ToString();
            List<PerformerIslemDTO> performerIslemleri = JsonConvert.DeserializeObject<List<PerformerIslemDTO>>(dynamicData);

            if (performerIslemleri == null) return OdiResponse<List<PerformerProjeDTO>>.Success("Performera ait proje bulunmamaktadır", null, 200);

            List<string> listProjeId = performerIslemleri.Select(x => x.ProjeId).ToList();

            //proje listesi 
            List<Proje> projeList = await _projeDataService.PerformerProjeListesi(listProjeId, dilId);

            //projelistesinin performerprojelistesine ye dönüştürme
            List<PerformerProjeDTO> performerProjeList = new List<PerformerProjeDTO>();

            foreach (var proje in projeList)
            {
                if (!string.IsNullOrEmpty(proje.Fotograf)) proje.Fotograf = _amazonS3Service.GetPreSignedUrl(proje.Fotograf);
                List<ProjeRolOutputDTO> roller = await _projeRolLogicService.ProjeRolleriGetir(proje.Id);
                ProjeOutputDTO projeDTO = _mapper.Map<ProjeOutputDTO>(proje);
                PerformerIslemDTO islem = performerIslemleri.Where(x => x.ProjeId == proje.Id).FirstOrDefault();

                //ROL BİLGİSİ VE ODİ BİLGİSİ BİRLEŞTİRME
                List<string> rolidlist = islem.RolOdiBilgileri.Select(x => x.ProjeRolId).ToList();
                List<ProjeRolOutputDTO> odiTalepRoller = new List<ProjeRolOutputDTO>();//Odi Talepi Yapılmış Rol listesi
                if (roller != null) odiTalepRoller = roller.Where(x => rolidlist.Contains(x.ProjeRolId)).ToList();//Odi Talepi Yapılmış Rol listesi

                //odi talepi yapılmış rollere odi bilgisi eklenmesi
                List<ProjeRolWithOdiBilgisiOutput> rollerWithOdiTalep = new List<ProjeRolWithOdiBilgisiOutput>();
                foreach (var item in odiTalepRoller)
                {
                    ProjeRolWithOdiBilgisiOutput rolwithodi = new ProjeRolWithOdiBilgisiOutput();
                    rolwithodi.Rol = item;
                    rolwithodi.RolOdiBilgisi = islem.RolOdiBilgileri.Where(x => x.ProjeRolId == item.ProjeRolId).FirstOrDefault();

                    //opsiyon bilgisinini işlenmesi
                    if (islem.RolOpsiyonBilgileri.Count > 0)
                        rolwithodi.OpsiyonId = islem.RolOpsiyonBilgileri.FirstOrDefault(x => x.ProjeRolId == item.ProjeRolId).OpsiyonId;

                    rollerWithOdiTalep.Add(rolwithodi);

                }

                PerformerProjeDTO performerProje = _mapper.Map<PerformerProjeDTO>(projeDTO);
                performerProje.OdiTalep = true;
                performerProje.OpsiyonTalep = islem.Opsiyon;
                performerProje.CallbackTalep = islem.Callback;
                performerProje.Roller = rollerWithOdiTalep;
                performerProjeList.Add(performerProje);
            }

            return OdiResponse<List<PerformerProjeDTO>>.Success("Performer Proje Listesi Getirildi", performerProjeList, 200);
        }

        #endregion

        #region

        public async Task<OdiResponse<PerformerProjeDTO>> MenajerProjeOdiDetayGetir(MenajerIslemInputDTO input, string token, int dilId)
        {
            var dynamicData = await _useOtherService.PostMethod(input, _configuration.GetSection("IslemlerServerUrl").Value + "/menajer-proje-islem", token);

            string jsonString = JsonConvert.SerializeObject(dynamicData);
            List<PerformerIslemDTO> performerIslemleri = JsonConvert.DeserializeObject<List<PerformerIslemDTO>>(jsonString);
            if (performerIslemleri == null) return OdiResponse<PerformerProjeDTO>.Success(" veri bulunamadı", null, 200);
            Proje proje = await _projeDataService.ProjeGetir(performerIslemleri.FirstOrDefault().ProjeId);
            PerformerProjeDTO performerProje = new PerformerProjeDTO();
            if (proje != null)
            {
                if (!string.IsNullOrEmpty(proje.Fotograf)) proje.Fotograf = _amazonS3Service.GetPreSignedUrl(proje.Fotograf);
                List<ProjeRolOutputDTO> roller = await _projeRolLogicService.ProjeRolleriGetir(proje.Id);
                ProjeOutputDTO projeDTO = _mapper.Map<ProjeOutputDTO>(proje);
                PerformerIslemDTO islem = performerIslemleri.Where(x => x.ProjeId == proje.Id).FirstOrDefault();

                //ROL BİLGİSİ VE ODİ BİLGSİ BİRLEŞTİRME
                List<string> rolidlist = islem.RolOdiBilgileri.Select(x => x.ProjeRolId).ToList();
                List<ProjeRolOutputDTO> odiTalepRoller = new List<ProjeRolOutputDTO>();//Odi Talepi Yapılmış Rol listesi
                if (roller != null) odiTalepRoller = roller.Where(x => rolidlist.Contains(x.ProjeRolId)).ToList();//Odi Talepi Yapılmış Rol listesi

                //odi talepi yapılmış rollere odi bilgisi eklenmesi
                List<ProjeRolWithOdiBilgisiOutput> rollerWithOdiTalep = new List<ProjeRolWithOdiBilgisiOutput>();
                foreach (var item in odiTalepRoller)
                {
                    ProjeRolWithOdiBilgisiOutput rolwithodi = new ProjeRolWithOdiBilgisiOutput();
                    rolwithodi.Rol = item;
                    rolwithodi.RolOdiBilgisi = islem.RolOdiBilgileri.Where(x => x.ProjeRolId == item.ProjeRolId).FirstOrDefault();
                    rollerWithOdiTalep.Add(rolwithodi);
                }

                performerProje = _mapper.Map<PerformerProjeDTO>(projeDTO);
                performerProje.OdiTalep = true;
                performerProje.Roller = rollerWithOdiTalep;
            }
            return OdiResponse<PerformerProjeDTO>.Success("Menajer için proje odi bilgileri getirldi", performerProje, 200);
        }

        #endregion
    }
}