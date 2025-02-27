using AutoMapper;
using OdiApp.BusinessLayer.Core;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.DataAccessLayer.IslemlerDataServices.CallbackIslemler;
using OdiApp.DTOs.IslemlerDTOs;
using OdiApp.DTOs.IslemlerDTOs.CallbackIslemler;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.IslemlerModels.CallbackIslemler;

namespace OdiApp.BusinessLayer.Services.IslemlerLogicServices.CallbackIslemler
{
    public class CallbackLogicService : ICallbackLogicService
    {
        private readonly IMapper _mapper;
        private readonly IAmazonS3Service _amazonS3Service;
        private readonly ICallbackDataService _callbackDataService;


        public CallbackLogicService(IMapper mapper, IAmazonS3Service amazonS3Service, ICallbackDataService callbackDataService)
        {
            _mapper = mapper;
            _amazonS3Service = amazonS3Service;
            _callbackDataService = callbackDataService;

        }

        public async Task<OdiResponse<List<CallbackTakvimSaatleriOutputDTO>>> YeniCallbackAyarlarıTakvimOlustur(CallbackAyarlarıTakvimCreateDTO input, OdiUser user)
        {
            bool control = await _callbackDataService.CheckCallbackAyarlari(input.ProjeId);
            if (control)
                return OdiResponse<List<CallbackTakvimSaatleriOutputDTO>>.Fail("Bu Proje için daha önce callback ayarları ve takvimi oluşturulmuş", "Bad Request", 400);

            CallbackAyarlari ayarlar = _mapper.Map<CallbackAyarlari>(input.CallbackAyarlari);
            List<CallbackNot> notlar = new List<CallbackNot>();
            if (input.CallbackNotlari != null) notlar = _mapper.Map<List<CallbackNot>>(input.CallbackNotlari);
            List<CallbackSenaryo> senaryolar = new List<CallbackSenaryo>();
            if (input.CallbackSenaryolari != null) senaryolar = _mapper.Map<List<CallbackSenaryo>>(input.CallbackSenaryolari);
            List<CallbackSaat> takvim = _mapper.Map<List<CallbackSaat>>(input.CallbackTakvimi);


            #region ekleyen guncelleyen
            ayarlar.Ekleyen = user.AdSoyad;
            ayarlar.EkleyenId = user.Id;
            ayarlar.EklenmeTarihi = DateTime.Now;

            ayarlar.GuncellenmeTarihi = DateTime.Now;
            ayarlar.Guncelleyen = user.AdSoyad;
            ayarlar.GuncelleyenId = user.Id;

            if (notlar != null)
            {
                foreach (CallbackNot not in notlar)
                {
                    not.Ekleyen = user.AdSoyad;
                    not.EkleyenId = user.Id;
                    not.EklenmeTarihi = DateTime.Now;

                    not.GuncellenmeTarihi = DateTime.Now;
                    not.Guncelleyen = user.AdSoyad;
                    not.GuncelleyenId = user.Id;
                }
            }
            if (senaryolar != null)
            {
                foreach (CallbackSenaryo senaryo in senaryolar)
                {
                    senaryo.Ekleyen = user.AdSoyad;
                    senaryo.EkleyenId = user.Id;
                    senaryo.EklenmeTarihi = DateTime.Now;

                    senaryo.GuncellenmeTarihi = DateTime.Now;
                    senaryo.Guncelleyen = user.AdSoyad;
                    senaryo.GuncelleyenId = user.Id;
                }
            }
            foreach (CallbackSaat saat in takvim)
            {
                saat.Ekleyen = user.AdSoyad;
                saat.EkleyenId = user.Id;
                saat.EklenmeTarihi = DateTime.Now;

                saat.GuncellenmeTarihi = DateTime.Now;
                saat.Guncelleyen = user.AdSoyad;
                saat.GuncelleyenId = user.Id;
            }
            #endregion

            await _callbackDataService.YeniCallbackAyarlari(ayarlar);
            if (notlar != null) await _callbackDataService.YeniCallbackNotlari(notlar);
            if (senaryolar != null) await _callbackDataService.YeniCallbackSenaryolari(senaryolar);
            await _callbackDataService.YeniCallbackTakvimi(takvim);

            await _callbackDataService.SaveChangesAsync();


            return OdiResponse<List<CallbackTakvimSaatleriOutputDTO>>.Success("Callback Ayarları ve takvimi oluşturuldu", await _callbackDataService.CallbackTakvimSaatGetir(input.ProjeId), 200);


        }

        public async Task DeleteCallbackAyarlariTakvim()
        {
            await _callbackDataService.DeleteCallbackAyarlariTakvim();
        }

        public async Task<OdiResponse<List<CallbackTakvimSaatleriOutputDTO>>> CallbackTakvimiGetir(CallbackTakvimiGetirInput input)
        {
            List<CallbackTakvimSaatleriOutputDTO> saatlist = new List<CallbackTakvimSaatleriOutputDTO>();
            if (input.sadeceSaatBilgisi)
                saatlist = await _callbackDataService.CallbackTakvimSaatGetir(input.ProjeId);
            else
                saatlist = await _CallbackTakvimSaatleriGetir(input.ProjeId);
            return OdiResponse<List<CallbackTakvimSaatleriOutputDTO>>.Success("getirldi", saatlist, 200);
        }

        public async Task<OdiResponse<List<CallbackTakvimSaatleriOutputDTO>>> CallbackSaatleriKilitle(CallbackSaatKilitleInputDTO inputDTO)
        {
            await _callbackDataService.CallbackSaatleriKilitle(inputDTO.SaatIdListesi);

            return OdiResponse<List<CallbackTakvimSaatleriOutputDTO>>.Success("Saatler Kilitlendi", await _CallbackTakvimSaatleriGetir(inputDTO.ProjeId), 200);
        }
        public async Task<OdiResponse<List<CallbackTakvimSaatleriOutputDTO>>> CallbackSaatleriKilidiAc(CallbackSaatKilitleInputDTO inputDTO)
        {
            await _callbackDataService.CallbackSaatleriKilidiAc(inputDTO.SaatIdListesi);

            return OdiResponse<List<CallbackTakvimSaatleriOutputDTO>>.Success("Saatler Kilitleri Açıldı", await _CallbackTakvimSaatleriGetir(inputDTO.ProjeId), 200);
        }
        public async Task<OdiResponse<List<CallbackTakvimSaatleriOutputDTO>>> YeniCalllbackTarihSaatleriEkle(CallbackTarihSaatCreateDTO yeniCallbackTarihSaat, OdiUser user)
        {
            CallbackAyarlari ayarlar = await _callbackDataService.CallbackAyarlariGetir(yeniCallbackTarihSaat.ProjeId);
            ayarlar.CallbackTarihleri = ayarlar.CallbackTarihleri + "," + Fonksiyonlar.convertDateTimeListToString(yeniCallbackTarihSaat.YeniCallbackTarihleri);

            _callbackDataService.CallbackAyarlariGuncelle(ayarlar);
            List<CallbackSaat> yenisaatler = _mapper.Map<List<CallbackSaat>>(yeniCallbackTarihSaat.YeniCallbackSaatleri);
            foreach (var saat in yenisaatler)
            {
                saat.EklenmeTarihi = DateTime.Now;
                saat.Ekleyen = user.AdSoyad;
                saat.EkleyenId = user.Id;

                saat.GuncellenmeTarihi = DateTime.Now;
                saat.Guncelleyen = user.AdSoyad;
                saat.GuncelleyenId = user.Id;
            }
            await _callbackDataService.YeniCallbackTakvimi(yenisaatler);
            await _callbackDataService.SaveChangesAsync();

            return OdiResponse<List<CallbackTakvimSaatleriOutputDTO>>.Success("Yeni tarih ve saatler eklendi", await _CallbackTakvimSaatleriGetir(yeniCallbackTarihSaat.ProjeId), 200);
        }

        public async Task<OdiResponse<CallbackTarihEklemeAyarlariOutput>> CallbackTarihEklemeAyarlariGetir(ProjeIdDTO id)
        {
            CallbackTarihEklemeAyarlariOutput output = new CallbackTarihEklemeAyarlariOutput();

            CallbackAyarlari ayarlar = await _callbackDataService.CallbackAyarlariGetir(id.ToString());
            output.BitisSaati = ayarlar.BitisSaati;
            output.BaslangicSaati = ayarlar.BaslangicSaati;
            output.GorusmeAraligi = ayarlar.GorusmeAraligi;
            return OdiResponse<CallbackTarihEklemeAyarlariOutput>.Success("Tarh ekleme ayarları getirildi", output, 200);
        }

        public async Task<OdiResponse<CallbackSaatOutputDTO>> CallbackSaatiNotGuncelle(CallbackSaatNotuInputDTO input, OdiUser user)
        {
            CallbackSaat saat = await _callbackDataService.CallbackSaatiGetir(input.CallbackSaatiId);
            saat.Not = input.Not;

            saat.GuncellenmeTarihi = DateTime.Now;
            saat.Guncelleyen = user.AdSoyad;
            saat.GuncelleyenId = user.Id;

            saat = await _callbackDataService.CallbackSaatGuncelle(saat);
            _callbackDataService.SaveChangesAsync();

            return OdiResponse<CallbackSaatOutputDTO>.Success("Callback Saati Notu Eklendi", _mapper.Map<CallbackSaatOutputDTO>(saat), 200);
        }


        public async Task<OdiResponse<List<CallbackGonderilecekPerformerOutput>>> CallbackGonderilebilecekPerformerListesi(ProjeIdDTO projeId)
        {
            List<CallbackGonderilecekPerformerOutput> result = await _callbackDataService.CallbackGonderilecekPerformerListesi(projeId.ToString());
            foreach (var item in result)
            {
                item.PerformerProfilFotografi = _amazonS3Service.GetPreSignedUrl(item.PerformerProfilFotografi);
            }
            return OdiResponse<List<CallbackGonderilecekPerformerOutput>>.Success("Performer Listesi Getirildi", result, 200);
        }

        public async Task<OdiResponse<List<CallbackOutputDTO>>> CallbackGonder(List<CallbackCreateDTO> callbackList, OdiUser user)
        {
            List<Callback> cbList = _mapper.Map<List<Callback>>(callbackList);

            foreach (var item in cbList)
            {
                item.EklenmeTarihi = DateTime.Now;
                item.Ekleyen = user.AdSoyad;
                item.EkleyenId = user.Id;

                item.GuncellenmeTarihi = DateTime.Now;
                item.Guncelleyen = user.AdSoyad;
                item.GuncelleyenId = user.Id;
                CallbackSaat saat = await _callbackDataService.CallbackSaatiGetir(item.CallbackSaatId);
                saat.Dolu = true;
                await _callbackDataService.CallbackSaatGuncelle(saat);

            }

            await _callbackDataService.YeniCallback(cbList);
            await _callbackDataService.SaveChangesAsync();
            List<CallbackOutputDTO> outputList = await _callbackDataService.YapimCallbackListesiGetir(callbackList.FirstOrDefault().ProjeId);

            return OdiResponse<List<CallbackOutputDTO>>.Success("Callbackler Gönderildi", _CallbackOutputDTOHazirlik(outputList), 200);
        }



        public async Task<OdiResponse<List<CallbackOutputDTO>>> YapimCallbackListesi(ProjeIdDTO projeId)
        {
            List<CallbackOutputDTO> outputList = await _callbackDataService.YapimCallbackListesiGetir(projeId.ToString());

            return OdiResponse<List<CallbackOutputDTO>>.Success("Yapım Callback Listesi Getirildi", _CallbackOutputDTOHazirlik(outputList), 200);
        }

        public async Task<OdiResponse<List<CallbackOutputDTO>>> MenajerCallbackListesi(KullaniciIdDTO menajerId)
        {
            List<CallbackOutputDTO> outputList = await _callbackDataService.MenajerCallbackListesiGetir(menajerId.ToString());

            return OdiResponse<List<CallbackOutputDTO>>.Success("Menajer Callback Listesi Getirildi", _CallbackOutputDTOHazirlik(outputList), 200);
        }

        private List<CallbackOutputDTO> _CallbackOutputDTOHazirlik(List<CallbackOutputDTO> list)
        {
            foreach (var item in list)
            {
                item.CallbackSaati = Convert.ToDateTime(item.CallbackSaati).ToString("HH:mm");
                item.CallbackTarihi = Convert.ToDateTime(item.CallbackTarihi).ToString("dd MMMM yyyy - dddd");
                item.PerformerProfilFotografi = _amazonS3Service.GetPreSignedUrl(item.PerformerProfilFotografi);
            }
            return list;
        }
        private CallbackOutputDTO _CallbackOutputDTOHazirlik(CallbackOutputDTO callback)
        {
            callback.CallbackSaati = Convert.ToDateTime(callback.CallbackSaati).ToString("HH:mm");
            callback.CallbackTarihi = Convert.ToDateTime(callback.CallbackTarihi).ToString("dd MMMM yyyy - dddd");
            callback.PerformerProfilFotografi = _amazonS3Service.GetPreSignedUrl(callback.PerformerProfilFotografi);
            return callback;
        }
        private async Task<List<CallbackTakvimSaatleriOutputDTO>> _CallbackTakvimSaatleriGetir(string projeId)
        {
            List<CallbackTakvimSaatleriOutputDTO> saatlist = await _callbackDataService.CallbackTakvimSaatGetir(projeId);
            List<CallbackOutputDTO> callbackList = await _callbackDataService.YapimCallbackListesiGetir(projeId);
            List<CallbackOutputDTO> hazircallbackList = _CallbackOutputDTOHazirlik(callbackList);
            foreach (var output in saatlist)
            {
                foreach (var saat in output.CallbackSaatleri)
                {
                    saat.Callback = hazircallbackList.FirstOrDefault(x => x.CallbackSaatId == saat.CallbackSaatId);
                }
            }

            return saatlist;
        }

        public async Task<OdiResponse<CallbackOutputDTO>> CallbackOnayla(CallbackOnaylaDTO onay, OdiUser user)
        {
            Callback callback = await _callbackDataService.CallbackGetir(onay.CallbackId);
            callback.Onaylandi = true;
            callback.PerformerOnayladi = onay.PerformerOnayladi;
            callback.MenajerOnayladi = onay.MenajerOnayladi;
            callback.Kapandi = true;
            callback.CallbackKapanmaTarihi = DateTime.Now;
            callback.GuncellenmeTarihi = DateTime.Now;
            callback.Guncelleyen = user.AdSoyad;
            callback.GuncelleyenId = user.Id;
            if (onay.CallbackSaatiDegistirildi)
            {
                CallbackSaat saat = await _callbackDataService.CallbackSaatiGetir(callback.CallbackSaatId);
                saat.Dolu = false;
                await _callbackDataService.CallbackSaatGuncelle(saat);

                CallbackSaat yenisaat = await _callbackDataService.CallbackSaatiGetir(onay.YeniSaatId);
                saat.Dolu = true;
                await _callbackDataService.CallbackSaatGuncelle(saat);
                callback.CallbackSaatId = onay.YeniSaatId;
            }


            await _callbackDataService.CallbackGuncelle(callback);


            CallbackOutputDTO callbackOutput = await _callbackDataService.CallbackOutputGetir(onay.CallbackId);

            return OdiResponse<CallbackOutputDTO>.Success("Callback Onaylandı", _CallbackOutputDTOHazirlik(callbackOutput), 200);

        }

        public async Task<OdiResponse<CallbackOutputDTO>> CallbackRed(CallbackRedDTO red, OdiUser user)
        {
            Callback callback = await _callbackDataService.CallbackGetir(red.CallbackId);
            callback.Reddedildi = true;
            callback.PerformerReddetti = red.PerformerReddetti;
            callback.MenajerReddetti = red.MenajerReddetti;
            callback.RedSebebi = red.RedSebebi;
            callback.Kapandi = true;
            callback.CallbackKapanmaTarihi = DateTime.Now;
            callback.GuncellenmeTarihi = DateTime.Now;
            callback.Guncelleyen = user.AdSoyad;
            callback.GuncelleyenId = user.Id;

            await _callbackDataService.CallbackGuncelle(callback);

            CallbackOutputDTO callbackOutput = await _callbackDataService.CallbackOutputGetir(red.CallbackId);

            return OdiResponse<CallbackOutputDTO>.Success("Callback Reddedildi", _CallbackOutputDTOHazirlik(callbackOutput), 200);

        }

        public async Task<OdiResponse<CallbackOutputDTO>> CallbackMenajerGordu(CallbackIdDTO callbackId)
        {

            Callback callback = await _callbackDataService.CallbackGetir(callbackId.ToString());
            callback.MenajerGordu = true;

            await _callbackDataService.CallbackGuncelle(callback);

            CallbackOutputDTO callbackOutput = await _callbackDataService.CallbackOutputGetir(callbackId.ToString());

            return OdiResponse<CallbackOutputDTO>.Success("Callback Menajer tarafından görüldü", _CallbackOutputDTOHazirlik(callbackOutput), 200);

        }

        public async Task<OdiResponse<CallbackOutputDTO>> CallbackPerformerGordu(CallbackIdDTO callbackId)
        {
            Callback callback = await _callbackDataService.CallbackGetir(callbackId.ToString());
            callback.PerformerGordu = true;

            await _callbackDataService.CallbackGuncelle(callback);

            CallbackOutputDTO callbackOutput = await _callbackDataService.CallbackOutputGetir(callbackId.ToString());

            return OdiResponse<CallbackOutputDTO>.Success("Callback performer tarafından görüldü", _CallbackOutputDTOHazirlik(callbackOutput), 200);
        }

        public async Task<OdiResponse<CallbackPerformerDetaylariOutputDTO>> CallbackPerformerDetaylari(CallbackPerformerDetaylariInputDTO input)
        {
            CallbackOutputDTO callback = await _callbackDataService.CallbackOutputGetir(input.ProjeId, input.PerformerId);
            CallbackSaat saat = await _callbackDataService.CallbackSaatiGetir(callback.CallbackSaatId);
            CallbackAyarlari ayarlar = await _callbackDataService.CallbackAyarlariGetir(input.ProjeId);
            CallbackNot not = await _callbackDataService.CallbackNotGetir(input.ProjeId, callback.ProjeRolId);
            CallbackSenaryo senaryo = await _callbackDataService.CallbackSenaryoGetir(input.ProjeId, callback.ProjeRolId);
            CallbackPerformerDetaylariOutputDTO output = new CallbackPerformerDetaylariOutputDTO();
            output.callback = _CallbackOutputDTOHazirlik(callback);
            output.GorusmeYeri = ayarlar.GorusmeYeri;
            output.GorusmeAdresi = ayarlar.GorusmeAdresi;
            if (not != null)
                output.CallbackNotu = not.Not;
            if (senaryo != null)
                output.Senaryo = _amazonS3Service.GetPreSignedUrl(senaryo.Senaryo);

            return OdiResponse<CallbackPerformerDetaylariOutputDTO>.Success("Performer callback detayları getirildi", output, 200);
        }
    }
}