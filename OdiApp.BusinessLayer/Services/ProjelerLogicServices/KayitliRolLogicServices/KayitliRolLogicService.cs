using AutoMapper;
using OdiApp.DataAccessLayer.ProjelerDataServices.KayitliRoller;
using OdiApp.DataAccessLayer.ProjelerDataServices.ProjeRolBilgileri;
using OdiApp.DataAccessLayer.ProjelerDataServices.ProjeRolOdiBilgileri;
using OdiApp.DTOs.IslemlerDTOs;
using OdiApp.DTOs.SharedDTOs;
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
using OdiApp.EntityLayer.ProjelerModels.ProjeRolBilgisi;
using OdiApp.EntityLayer.ProjelerModels.ProjeRolOdi;

namespace OdiApp.BusinessLayer.Services.ProjelerLogicServices.KayitliRolLogicServices
{
    public class KayitliRolLogicService : IKayitliRolLogicService
    {
        private readonly IProjeRolDataService _projeRolDataService;
        private readonly IKayitliRolDataService _kayitliRolDataService;
        private readonly IProjeRolOdiDataService _projeRolOdiDataService;
        private readonly IMapper _mapper;

        public KayitliRolLogicService(IProjeRolDataService projeRolDataService, IMapper mapper, IKayitliRolDataService kayitliRolDataService, IProjeRolOdiDataService projeRolOdiDataService)
        {
            _projeRolDataService = projeRolDataService;
            _mapper = mapper;
            _kayitliRolDataService = kayitliRolDataService;
            _projeRolOdiDataService = projeRolOdiDataService;
        }

        public async Task<OdiResponse<bool>> RolKaydet(ProjeRolIdDTO projeRolIdDTO, OdiUser user)
        {
            DateTime dateTime = DateTime.Now;

            StringBaseModel stringBaseModel = new StringBaseModel();

            stringBaseModel.EklenmeTarihi = dateTime;
            stringBaseModel.GuncellenmeTarihi = dateTime;
            stringBaseModel.EkleyenId = user.Id;
            stringBaseModel.GuncelleyenId = user.Id;
            stringBaseModel.Ekleyen = user.AdSoyad;
            stringBaseModel.Guncelleyen = user.AdSoyad;

            BaseModel baseModel = new BaseModel();

            baseModel.EklenmeTarihi = dateTime;
            baseModel.GuncellenmeTarihi = dateTime;
            baseModel.EkleyenId = user.Id;
            baseModel.GuncelleyenId = user.Id;
            baseModel.Ekleyen = user.AdSoyad;
            baseModel.Guncelleyen = user.AdSoyad;

            //------

            ProjeRol projeRol = await _projeRolDataService.ProjeRolGetir(projeRolIdDTO.ProjeRolId);

            KayitliRol kayitliRol = _mapper.Map<KayitliRol>(projeRol);

            kayitliRol.KullaniciId = user.Id;

            kayitliRol = _mapper.Map<StringBaseModel, KayitliRol>(stringBaseModel, kayitliRol);

            if (kayitliRol.RolOzellik != null)
                kayitliRol.RolOzellik = _mapper.Map<StringBaseModel, KayitliRolOzellik>(stringBaseModel, kayitliRol.RolOzellik);

            if (kayitliRol.AnketSorulari?.Any() == true)
            {
                List<KayitliRolAnketSorusu> updatedAnketSorulari = new List<KayitliRolAnketSorusu>();

                foreach (var anketSorusu in kayitliRol.AnketSorulari)
                {
                    var updatedAnketSorusu = _mapper.Map<StringBaseModel, KayitliRolAnketSorusu>(stringBaseModel, anketSorusu);
                    updatedAnketSorulari.Add(updatedAnketSorusu);
                }

                kayitliRol.AnketSorulari = updatedAnketSorulari;
            }

            kayitliRol = await _kayitliRolDataService.YeniKayitliRol(kayitliRol);

            //------

            ProjeRolOdi projeRolOdi = await _projeRolOdiDataService.ProjeRolOdiGetir(projeRolIdDTO.ProjeRolId);

            KayitliRolOdi kayitliRolOdi = new KayitliRolOdi();
            kayitliRolOdi.KayitliRolId = kayitliRol.Id;

            kayitliRolOdi = _mapper.Map<StringBaseModel, KayitliRolOdi>(stringBaseModel, kayitliRolOdi);

            kayitliRolOdi = await _kayitliRolDataService.YeniKayitliRolOdi(kayitliRolOdi);

            //------

            List<RolOdiFotoOrnekFotograf> rolOdiFotoOrnekFotografList = await _projeRolOdiDataService.RolOdiFotoOrnekFotoListesiGetir(projeRolOdi.Id);

            if (rolOdiFotoOrnekFotografList?.Any() == true)
            {
                List<KayitliRolOdiFotoOrnekFotograf> kayitliRolOdiFotoOrnekFotografList = new List<KayitliRolOdiFotoOrnekFotograf>();

                foreach (var rolOdiFotoOrnekFotograf in rolOdiFotoOrnekFotografList)
                {
                    var kayitliRolOdiFotoOrnekFotograf = _mapper.Map<KayitliRolOdiFotoOrnekFotograf>(rolOdiFotoOrnekFotograf);
                    kayitliRolOdiFotoOrnekFotograf = _mapper.Map<StringBaseModel, KayitliRolOdiFotoOrnekFotograf>(stringBaseModel, kayitliRolOdiFotoOrnekFotograf);

                    kayitliRolOdiFotoOrnekFotograf.KayitliRolOdiId = kayitliRolOdi.Id;

                    kayitliRolOdiFotoOrnekFotografList.Add(kayitliRolOdiFotoOrnekFotograf);
                }

                await _kayitliRolDataService.YeniKayitliRolOdiFotoOrnekFotograf(kayitliRolOdiFotoOrnekFotografList);
            }

            //------

            List<RolOdiFotoPoz> rolOdiFotoPozList = await _projeRolOdiDataService.RolOdiFotoPozListesiGetir(projeRolOdi.Id);

            if (rolOdiFotoPozList?.Any() == true)
            {
                List<KayitliRolOdiFotoPoz> kayitliRolOdiFotoPozList = new List<KayitliRolOdiFotoPoz>();

                foreach (var rolOdiFotoPoz in rolOdiFotoPozList)
                {
                    var kayitliRolOdiFotoPoz = _mapper.Map<KayitliRolOdiFotoPoz>(rolOdiFotoPoz);
                    kayitliRolOdiFotoPoz = _mapper.Map<StringBaseModel, KayitliRolOdiFotoPoz>(stringBaseModel, kayitliRolOdiFotoPoz);

                    kayitliRolOdiFotoPoz.KayitliRolOdiId = kayitliRolOdi.Id;

                    kayitliRolOdiFotoPozList.Add(kayitliRolOdiFotoPoz);
                }

                await _kayitliRolDataService.YeniKayitliRolOdiFotoPoz(kayitliRolOdiFotoPozList);
            }

            //------

            List<RolOdiFotoYonetmenNotu> rolOdiFotoYonetmenNotuList = await _projeRolOdiDataService.RolOdiFotoYonetmenNotuListesiGetir(projeRolOdi.Id);

            if (rolOdiFotoYonetmenNotuList?.Any() == true)
            {
                List<KayitliRolOdiFotoYonetmenNotu> kayitliRolOdiFotoYonetmenNotuList = new List<KayitliRolOdiFotoYonetmenNotu>();

                foreach (var rolOdiFotoYonetmenNotu in rolOdiFotoYonetmenNotuList)
                {
                    var kayitliRolOdiFotoYonetmenNotu = _mapper.Map<KayitliRolOdiFotoYonetmenNotu>(rolOdiFotoYonetmenNotu);
                    kayitliRolOdiFotoYonetmenNotu = _mapper.Map<StringBaseModel, KayitliRolOdiFotoYonetmenNotu>(stringBaseModel, kayitliRolOdiFotoYonetmenNotu);

                    kayitliRolOdiFotoYonetmenNotu.KayitliRolOdiId = kayitliRolOdi.Id;

                    kayitliRolOdiFotoYonetmenNotuList.Add(kayitliRolOdiFotoYonetmenNotu);
                }

                await _kayitliRolDataService.YeniKayitliRolOdiFotoYonetmenNotu(kayitliRolOdiFotoYonetmenNotuList);
            }

            //------

            List<RolOdiSes> rolOdiSesList = await _projeRolOdiDataService.RolOdiSesListesiGetir(projeRolOdi.Id);

            if (rolOdiSesList?.Any() == true)
            {
                List<KayitliRolOdiSes> kayitliRolOdiSesList = new List<KayitliRolOdiSes>();

                foreach (var rolOdiSes in rolOdiSesList)
                {
                    var kayitliRolOdiSes = _mapper.Map<KayitliRolOdiSes>(rolOdiSes);
                    kayitliRolOdiSes = _mapper.Map<StringBaseModel, KayitliRolOdiSes>(stringBaseModel, kayitliRolOdiSes);

                    kayitliRolOdiSes.KayitliRolOdiId = kayitliRolOdi.Id;

                    kayitliRolOdiSesList.Add(kayitliRolOdiSes);
                }

                await _kayitliRolDataService.YeniKayitliRolOdiSes(kayitliRolOdiSesList);
            }

            //------

            List<RolOdiSesSenaryo> rolOdiSesSenaryoList = await _projeRolOdiDataService.RolOdiSesSenaryoListesiGetir(projeRolOdi.Id);

            if (rolOdiSesList?.Any() == true)
            {

                List<KayitliRolOdiSesSenaryo> kayitliRolOdiSesSenaryoList = new List<KayitliRolOdiSesSenaryo>();

                foreach (var rolOdiSesSenaryo in rolOdiSesSenaryoList)
                {
                    var kayitliRolOdiSesSenaryo = _mapper.Map<KayitliRolOdiSesSenaryo>(rolOdiSesSenaryo);
                    kayitliRolOdiSesSenaryo = _mapper.Map<StringBaseModel, KayitliRolOdiSesSenaryo>(stringBaseModel, kayitliRolOdiSesSenaryo);

                    kayitliRolOdiSesSenaryo.KayitliRolOdiId = kayitliRolOdi.Id;

                    kayitliRolOdiSesSenaryoList.Add(kayitliRolOdiSesSenaryo);
                }

                await _kayitliRolDataService.YeniKayitliRolOdiSesSenaryo(kayitliRolOdiSesSenaryoList);
            }

            //------

            List<RolOdiSesYonetmenNotu> rolRolOdiSesYonetmenNotuList = await _projeRolOdiDataService.RolOdiSesYonetmenNotuListesiGetir(projeRolOdi.Id);

            if (rolOdiSesList?.Any() == true)
            {
                List<KayitliRolOdiSesYonetmenNotu> kayitliRolRolOdiSesYonetmenNotuList = new List<KayitliRolOdiSesYonetmenNotu>();

                foreach (var rolRolOdiSesYonetmenNotu in rolRolOdiSesYonetmenNotuList)
                {
                    var kayitliRolRolOdiSesYonetmenNotu = _mapper.Map<KayitliRolOdiSesYonetmenNotu>(rolRolOdiSesYonetmenNotu);
                    kayitliRolRolOdiSesYonetmenNotu = _mapper.Map<StringBaseModel, KayitliRolOdiSesYonetmenNotu>(stringBaseModel, kayitliRolRolOdiSesYonetmenNotu);

                    kayitliRolRolOdiSesYonetmenNotu.KayitliRolOdiId = kayitliRolOdi.Id;

                    kayitliRolRolOdiSesYonetmenNotuList.Add(kayitliRolRolOdiSesYonetmenNotu);
                }

                await _kayitliRolDataService.YeniKayitliRolOdiSesYonetmenNotu(kayitliRolRolOdiSesYonetmenNotuList);
            }

            //------

            List<RolOdiSoru> rolOdiSoruList = await _projeRolOdiDataService.RolOdiSoruListGetir(projeRolOdi.Id);

            if (rolOdiSoruList?.Any() == true)
            {
                List<KayitliRolOdiSoru> kayitliRolOdiSoruList = new List<KayitliRolOdiSoru>();

                foreach (var rolOdiSoru in rolOdiSoruList)
                {
                    var kayitliRolOdiSoru = _mapper.Map<KayitliRolOdiSoru>(rolOdiSoru);
                    kayitliRolOdiSoru = _mapper.Map<StringBaseModel, KayitliRolOdiSoru>(stringBaseModel, kayitliRolOdiSoru);

                    kayitliRolOdiSoru.KayitliRolOdiId = kayitliRolOdi.Id;

                    if (kayitliRolOdiSoru.CevapSecenekleri?.Any() == true)
                    {
                        List<KayitliRolOdiSoruCevapSecenek> updatedKayitliRolOdiSoruCevapSecenekList = new List<KayitliRolOdiSoruCevapSecenek>();

                        foreach (var cevapSecenek in kayitliRolOdiSoru.CevapSecenekleri)
                        {
                            var updatedCevapSecenek = _mapper.Map<StringBaseModel, KayitliRolOdiSoruCevapSecenek>(stringBaseModel, cevapSecenek);
                            updatedKayitliRolOdiSoruCevapSecenekList.Add(updatedCevapSecenek);
                        }

                        kayitliRolOdiSoru.CevapSecenekleri = updatedKayitliRolOdiSoruCevapSecenekList;
                    }

                    kayitliRolOdiSoruList.Add(kayitliRolOdiSoru);
                }

                await _kayitliRolDataService.YeniKayitliRolOdiSoru(kayitliRolOdiSoruList);
            }

            //------

            List<RolOdiSoruAciklama> rolRolOdiSoruAciklamaList = await _projeRolOdiDataService.RolOdiSoruAciklamaListesiGetir(projeRolOdi.Id);

            if (rolOdiSesList?.Any() == true)
            {
                List<KayitliRolOdiSoruAciklama> kayitliRolRolOdiSoruAciklamaList = new List<KayitliRolOdiSoruAciklama>();

                foreach (var rolRolOdiSoruAciklama in rolRolOdiSoruAciklamaList)
                {
                    var kayitliRolRolOdiSoruAciklama = _mapper.Map<KayitliRolOdiSoruAciklama>(rolRolOdiSoruAciklama);
                    kayitliRolRolOdiSoruAciklama = _mapper.Map<StringBaseModel, KayitliRolOdiSoruAciklama>(stringBaseModel, kayitliRolRolOdiSoruAciklama);

                    kayitliRolRolOdiSoruAciklama.KayitliRolOdiId = kayitliRolOdi.Id;

                    kayitliRolRolOdiSoruAciklamaList.Add(kayitliRolRolOdiSoruAciklama);
                }

                await _kayitliRolDataService.YeniKayitliRolOdiSoruAciklama(kayitliRolRolOdiSoruAciklamaList);
            }

            //------

            List<RolOdiVideo> rolOdiVideoList = await _projeRolOdiDataService.RolOdiVideoListesiGetir(projeRolOdi.Id);

            if (rolOdiVideoList?.Any() == true)
            {
                List<KayitliRolOdiVideo> kayitliRolOdiVideoList = new List<KayitliRolOdiVideo>();

                foreach (var rolOdiVideo in rolOdiVideoList)
                {
                    var kayitliRolOdiVideo = _mapper.Map<KayitliRolOdiVideo>(rolOdiVideo);
                    kayitliRolOdiVideo = _mapper.Map<StringBaseModel, KayitliRolOdiVideo>(stringBaseModel, kayitliRolOdiVideo);

                    kayitliRolOdiVideo.KayitliRolOdiId = kayitliRolOdi.Id;

                    if (kayitliRolOdiVideo.DetayList?.Any() == true)
                    {
                        List<KayitliRolOdiVideoDetay> updatedVideoDetayList = new List<KayitliRolOdiVideoDetay>();

                        foreach (var videoDetay in kayitliRolOdiVideo.DetayList)
                        {
                            var updatedVideoDetay = _mapper.Map<StringBaseModel, KayitliRolOdiVideoDetay>(stringBaseModel, videoDetay);
                            updatedVideoDetayList.Add(updatedVideoDetay);
                        }

                        kayitliRolOdiVideo.DetayList = updatedVideoDetayList;
                    }

                    kayitliRolOdiVideoList.Add(kayitliRolOdiVideo);
                }

                await _kayitliRolDataService.YeniKayitliRolOdiVideo(kayitliRolOdiVideoList);
            }

            //------

            List<RolOdiVideoOrnekOyun> rolOdiVideoOrnekOyunList = await _projeRolOdiDataService.RolOdiVideoOrnekOyunListGetir(projeRolOdi.Id);

            if (rolOdiVideoOrnekOyunList?.Any() == true)
            {
                List<KayitliRolOdiVideoOrnekOyun> kayitliRolOdiVideoOrnekOyunList = new List<KayitliRolOdiVideoOrnekOyun>();

                foreach (var rolOdiVideoOrnekOyun in rolOdiVideoOrnekOyunList)
                {
                    var kayitliRolOdiVideoOrnekOyun = _mapper.Map<KayitliRolOdiVideoOrnekOyun>(rolOdiVideoOrnekOyun);
                    kayitliRolOdiVideoOrnekOyun = _mapper.Map<StringBaseModel, KayitliRolOdiVideoOrnekOyun>(stringBaseModel, kayitliRolOdiVideoOrnekOyun);

                    kayitliRolOdiVideoOrnekOyun.KayitliRolOdiId = kayitliRolOdi.Id;

                    kayitliRolOdiVideoOrnekOyunList.Add(kayitliRolOdiVideoOrnekOyun);
                }

                await _kayitliRolDataService.YeniKayitliRolOdiVideoOrnekOyun(kayitliRolOdiVideoOrnekOyunList);
            }

            //------

            List<RolOdiVideoSenaryo> rolOdiVideoSenaryoList = await _projeRolOdiDataService.RolOdiVideoSenaryoListesiGetir(projeRolOdi.Id);

            if (rolOdiVideoSenaryoList?.Any() == true)
            {
                List<KayitliRolOdiVideoSenaryo> kayitliRolOdiVideoSenaryoList = new List<KayitliRolOdiVideoSenaryo>();

                foreach (var rolOdiVideoSenaryo in rolOdiVideoSenaryoList)
                {
                    var kayitliRolOdiVideoSenaryo = _mapper.Map<KayitliRolOdiVideoSenaryo>(rolOdiVideoSenaryo);
                    kayitliRolOdiVideoSenaryo = _mapper.Map<StringBaseModel, KayitliRolOdiVideoSenaryo>(stringBaseModel, kayitliRolOdiVideoSenaryo);

                    kayitliRolOdiVideoSenaryo.KayitliRolOdiId = kayitliRolOdi.Id;

                    kayitliRolOdiVideoSenaryoList.Add(kayitliRolOdiVideoSenaryo);
                }

                await _kayitliRolDataService.YeniKayitliRolOdiVideoSenaryo(kayitliRolOdiVideoSenaryoList);
            }

            //------

            //RolOdiVideoYonetmenNotu rolOdiVideoYonetmenNotu = await _projeRolOdiDataService.RolOdiVideoYonetmenNotuGetir(projeRolOdi.Id);

            //if (rolOdiVideoYonetmenNotu != null)
            //{
            //    KayitliRolOdiVideoYonetmenNotu kayitliRolOdiVideoYonetmenNotu = _mapper.Map<KayitliRolOdiVideoYonetmenNotu>(rolOdiVideoYonetmenNotu);

            //    kayitliRolOdiVideoYonetmenNotu.KayitliRolOdiId = kayitliRolOdi.Id;

            //    kayitliRolOdiVideoYonetmenNotu = _mapper.Map<StringBaseModel, KayitliRolOdiVideoYonetmenNotu>(stringBaseModel, kayitliRolOdiVideoYonetmenNotu);

            //    await _kayitliRolDataService.YeniKayitliRolOdiVideoYonetmenNotu(kayitliRolOdiVideoYonetmenNotu);
            //}

            List<RolOdiVideoYonetmenNotu> rolOdiVideoYonetmenNotuList = await _projeRolOdiDataService.RolOdiVideoYonetmenNotuListesiGetir(projeRolOdi.Id);

            if (rolOdiVideoYonetmenNotuList?.Any() == true)
            {
                List<KayitliRolOdiVideoYonetmenNotu> kayitliRolOdiVideoYonetmenNotuList = new List<KayitliRolOdiVideoYonetmenNotu>();

                foreach (var rolOdiVideoYonetmenNotu in rolOdiVideoYonetmenNotuList)
                {
                    var kayitliRolOdiVideoYonetmenNotu = _mapper.Map<KayitliRolOdiVideoYonetmenNotu>(rolOdiVideoYonetmenNotu);
                    kayitliRolOdiVideoYonetmenNotu = _mapper.Map<StringBaseModel, KayitliRolOdiVideoYonetmenNotu>(stringBaseModel, kayitliRolOdiVideoYonetmenNotu);

                    kayitliRolOdiVideoYonetmenNotu.KayitliRolOdiId = kayitliRolOdi.Id;

                    kayitliRolOdiVideoYonetmenNotuList.Add(kayitliRolOdiVideoYonetmenNotu);
                }

                await _kayitliRolDataService.YeniKayitliRolOdiVideoYonetmenNotu(kayitliRolOdiVideoYonetmenNotuList);
            }

            //------

            return OdiResponse<bool>.Success("Rol kaydedildi.", true, 200);
        }
    }
}
