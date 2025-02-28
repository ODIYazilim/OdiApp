using AutoMapper;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.DataAccessLayer.ProjelerDataServices.ProjeRolOdiBilgileri;
using OdiApp.DTOs.IslemlerDTOs;
using OdiApp.DTOs.ProjelerDTOs.OdiFotograf;
using OdiApp.DTOs.ProjelerDTOs.OdiSes;
using OdiApp.DTOs.ProjelerDTOs.OdiSoru;
using OdiApp.DTOs.ProjelerDTOs.OdiVideo;
using OdiApp.DTOs.ProjelerDTOs.ProjeRolOdiBilgisi;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.ProjelerModels.OdiFotograf;
using OdiApp.EntityLayer.ProjelerModels.OdiSes;
using OdiApp.EntityLayer.ProjelerModels.OdiSoru;
using OdiApp.EntityLayer.ProjelerModels.OdiVideo;
using OdiApp.EntityLayer.ProjelerModels.ProjeRolOdi;

namespace OdiApp.BusinessLayer.Services.ProjelerLogicServices.ProjeRolOdiBilgileri
{
    public class ProjeRolOdiLogicService : IProjeRolOdiLogicService
    {
        private readonly IProjeRolOdiDataService _projeRolOdiDataService;
        private readonly IMapper _mapper;
        private readonly IAmazonS3Service _amazonService;
        public ProjeRolOdiLogicService(IMapper mapper, IAmazonS3Service amazonService, IProjeRolOdiDataService projeRolOdiDataService)
        {
            _mapper = mapper;
            _projeRolOdiDataService = projeRolOdiDataService;
            _amazonService = amazonService;
        }

        #region ROL ODI

        public async Task<OdiResponse<ProjeRolOdiOutputDTO>> RolOdiBilgileriniGetir(ProjeRolIdDTO projeRolId)
        {
            return OdiResponse<ProjeRolOdiOutputDTO>.Success("Proje Rol Odi Oluşturuldu", await this.ProjeRolOdiGetir(projeRolId.ProjeRolId), 200);
        }

        public async Task<OdiResponse<ProjeRolOdiOutputDTO>> YeniProjeRolOdi(ProjeRolOdiCreateDTO odiDTO, OdiUser user)
        {
            ProjeRolOdi odi = await _projeRolOdiDataService.ProjeRolOdiGetir(odiDTO.ProjeRolId);
            if (odi != null) return OdiResponse<ProjeRolOdiOutputDTO>.Fail("Bu role ait odi bilgisi zaten oluşturulmuştur", "Bad Request", 400);
            //odi = _mapper.Map<ProjeRolOdi>(odiDTO);
            string time = Convert.ToDateTime(odiDTO.SonOdilemeTarihi).ToString("dd MM yyyy");
            string hours = Convert.ToDateTime(odiDTO.SonOdilemeSaati).ToString("HH:mm");
            DateTime Tarih = Convert.ToDateTime(time + " " + hours);

            odi = new ProjeRolOdi();
            odi.SonOdilemeTarihi = Tarih;
            odi.ProjeRolId = odiDTO.ProjeRolId;
            odi.EklenmeTarihi = DateTime.Now;
            odi.EkleyenId = user.Id;
            odi.Ekleyen = user.AdSoyad;

            odi.GuncellenmeTarihi = DateTime.Now;
            odi.GuncelleyenId = user.Id;
            odi.Guncelleyen = user.AdSoyad;

            odi = await _projeRolOdiDataService.YeniProjeRolOdi(odi);

            return OdiResponse<ProjeRolOdiOutputDTO>.Success("Proje Rol Odi Oluşturuldu", await ProjeRolOdiGetir(odi.ProjeRolId), 200);
        }

        public async Task<OdiResponse<ProjeRolOdiOutputDTO>> ProjeRolOdiGuncelle(ProjeRolOdiUpdateDTO odiDTO, OdiUser user)
        {
            ProjeRolOdi odi = await _projeRolOdiDataService.ProjeRolOdiGetir(odiDTO.ProjeRolId);
            if (odi == null) return OdiResponse<ProjeRolOdiOutputDTO>.Fail("Henüz Odi bilgisi oluşturulmamış", "Bad Request", 400);
            odi = _mapper.Map(odiDTO, odi);
            string time = Convert.ToDateTime(odiDTO.SonOdilemeTarihi).ToString("dd MM yyyy");
            string hours = Convert.ToDateTime(odiDTO.SonOdilemeSaati).ToString("HH:mm");
            DateTime Tarih = Convert.ToDateTime(time + " " + hours);
            odi.Id = odiDTO.ProjeRolOdiId;
            odi.SonOdilemeTarihi = Tarih;
            odi.ProjeRolId = odiDTO.ProjeRolId;
            odi.GuncellenmeTarihi = DateTime.Now;
            odi.GuncelleyenId = user.Id;
            odi.Guncelleyen = user.AdSoyad;

            odi = await _projeRolOdiDataService.ProjeRolOdiGuncelle(odi);

            return OdiResponse<ProjeRolOdiOutputDTO>.Success("Proje Rol Odi Güncellendi", await ProjeRolOdiGetir(odi.ProjeRolId), 200);
        }

        public async Task<OdiResponse<ProjeRolOdiOutputDTO>> ProjeRolOdiGetir(ProjeRolIdDTO id)
        {
            return OdiResponse<ProjeRolOdiOutputDTO>.Success("Proje Rol Odi Getirildi", await this.ProjeRolOdiGetir(id.ProjeRolId), 200);
        }

        public async Task<ProjeRolOdiOutputDTO> ProjeRolOdiGetir(string projeRolId)
        {
            ProjeRolOdiOutputDTO rolOdiDTO = new ProjeRolOdiOutputDTO();
            ProjeRolOdi rolOdi = await _projeRolOdiDataService.ProjeRolOdiGetir(projeRolId);
            rolOdiDTO = _mapper.Map<ProjeRolOdiOutputDTO>(rolOdi);
            if (rolOdiDTO != null)
            {
                rolOdiDTO.Fotomatik = await RolOdiFotoGetir(rolOdiDTO.ProjeRolOdiId);
                rolOdiDTO.Sesmatik = await RolOdiSesGetir(rolOdiDTO.ProjeRolOdiId);
                rolOdiDTO.Videomatik = await RolOdiVideoGetir(rolOdiDTO.ProjeRolOdiId);
                rolOdiDTO.Sorumatik = await RolOdiSoruGetir(rolOdiDTO.ProjeRolOdiId);
            }

            return rolOdiDTO;
        }

        #endregion

        #region FOTOMATİK

        public async Task<OdiResponse<RolOdiFotoDTO>> YeniRolOdiFoto(RolOdiFotoCreateDTO odiFoto, OdiUser user)
        {
            List<RolOdiFotoPoz> fotoList = new List<RolOdiFotoPoz>();
            foreach (var poz in odiFoto.PozListesi)
            {
                RolOdiFotoPoz foto = new RolOdiFotoPoz();
                foto.Poz = poz;
                foto.ProjeRolOdiId = odiFoto.ProjeRolOdiId;

                foto.EklenmeTarihi = DateTime.Now;
                foto.EkleyenId = user.Id;
                foto.Ekleyen = user.AdSoyad;

                foto.GuncellenmeTarihi = DateTime.Now;
                foto.GuncelleyenId = user.Id;
                foto.Guncelleyen = user.AdSoyad;

                fotoList.Add(foto);
            }

            await _projeRolOdiDataService.YeniRolOdiFotoPozList(fotoList);

            RolOdiFotoYonetmenNotu not = new RolOdiFotoYonetmenNotu();
            not.YonetmenNotu = odiFoto.YonetmenNotu;
            not.ProjeRolOdiId = odiFoto.ProjeRolOdiId;

            not.EklenmeTarihi = DateTime.Now;
            not.EkleyenId = user.Id;
            not.Ekleyen = user.AdSoyad;

            not.GuncellenmeTarihi = DateTime.Now;
            not.GuncelleyenId = user.Id;
            not.Guncelleyen = user.AdSoyad;

            await _projeRolOdiDataService.YeniRolOdiFotoYonetmenNotu(not);

            List<RolOdiFotoOrnekFotograf> ornekFotoList = new List<RolOdiFotoOrnekFotograf>();

            foreach (var dosyaYolu in odiFoto.OrnekFotoDosyaYoluListesi)
            {
                RolOdiFotoOrnekFotograf ornFoto = new RolOdiFotoOrnekFotograf();

                ornFoto.ProjeRolOdiId = odiFoto.ProjeRolOdiId;
                ornFoto.OrnekFoto = dosyaYolu;

                ornFoto.EklenmeTarihi = DateTime.Now;
                ornFoto.EkleyenId = user.Id;
                ornFoto.Ekleyen = user.AdSoyad;

                ornFoto.GuncellenmeTarihi = DateTime.Now;
                ornFoto.GuncelleyenId = user.Id;
                ornFoto.Guncelleyen = user.AdSoyad;

                ornekFotoList.Add(ornFoto);
            }

            await _projeRolOdiDataService.YeniRolOdiFotoOrnekFotolar(ornekFotoList);


            return OdiResponse<RolOdiFotoDTO>.Success("Rol Odi Foto Eklendi", await RolOdiFotoGetir(odiFoto.ProjeRolOdiId), 200);

        }

        //poz
        public async Task<OdiResponse<RolOdiFotoDTO>> RolOdiFotoPozGuncelle(RolOdiFotoPozUpdateListDTO updateDTO, OdiUser user)
        {
            List<RolOdiFotoPoz> eskiPozListesi = await _projeRolOdiDataService.RolOdiFotoPozListesiGetir(updateDTO.ProjeRolOdiId);
            List<RolOdiFotoPoz> guncellenecekPozListesi = new List<RolOdiFotoPoz>();
            List<RolOdiFotoPoz> yeniPozListesi = new List<RolOdiFotoPoz>();



            foreach (var item in updateDTO.pozListesi)
            {
                RolOdiFotoPoz poz = _mapper.Map<RolOdiFotoPoz>(item);
                poz.GuncellenmeTarihi = DateTime.Now;
                poz.Guncelleyen = user.AdSoyad;
                poz.GuncelleyenId = user.Id;

                guncellenecekPozListesi.Add(poz);
            }
            await _projeRolOdiDataService.RolOdiFotoPozListesiGuncelle(guncellenecekPozListesi);
            List<RolOdiFotoPoz> silinenPozlar = new List<RolOdiFotoPoz>();

            foreach (var item in eskiPozListesi)
            {
                if (!guncellenecekPozListesi.Any(x => x.Id == item.Id)) silinenPozlar.Add(item);
            }
            if (silinenPozlar != null)
            {
                await _projeRolOdiDataService.RolOdiFotoPozListesiSil(silinenPozlar);
            }

            if (updateDTO.yeniPozListesi != null)
            {
                yeniPozListesi = _mapper.Map<List<RolOdiFotoPoz>>(updateDTO.yeniPozListesi);
                foreach (var poz in yeniPozListesi)
                {
                    poz.Ekleyen = user.AdSoyad;
                    poz.EkleyenId = user.Id;
                    poz.EklenmeTarihi = DateTime.Now;
                    poz.Guncelleyen = user.AdSoyad;
                    poz.GuncelleyenId = user.Id;
                    poz.GuncellenmeTarihi = DateTime.Now;
                }
                yeniPozListesi = await _projeRolOdiDataService.YeniRolOdiFotoPozList(yeniPozListesi);
            }

            return OdiResponse<RolOdiFotoDTO>.Success("Rol Odi Foto Poz listesi güncellendi", await RolOdiFotoGetir(updateDTO.ProjeRolOdiId), 200);
        }

        //YonetmenNotu
        public async Task<OdiResponse<RolOdiFotoDTO>> RolOdiFotoYonetmenNotGuncelle(RolOdiFotoYonetmenNotuUpdateDTO not, OdiUser user)
        {
            RolOdiFotoYonetmenNotu ynotu = await _projeRolOdiDataService.RolOdiFotoYonetmenNotuGetir(not.ProjeRolOdiId);
            ynotu = _mapper.Map<RolOdiFotoYonetmenNotuUpdateDTO, RolOdiFotoYonetmenNotu>(not);

            await _projeRolOdiDataService.OdiRolFotoYonetmenNotuGuncelle(ynotu);


            return OdiResponse<RolOdiFotoDTO>.Success("Rol Odi Foto Yönetmen Notu güncellendi", await RolOdiFotoGetir(not.ProjeRolOdiId), 200);
        }

        //OrnekFoto
        public async Task<OdiResponse<RolOdiFotoDTO>> RolOdiFotoOrnekFotoListesiGuncelle(RolOdiFotoOrnekFotografUpdateListDTO updateDTO, OdiUser user)
        {
            List<RolOdiFotoOrnekFotograf> eskiOrnekFotoListesi = await _projeRolOdiDataService.RolOdiFotoOrnekFotoListesiGetir(updateDTO.ProjeRolOdiId);
            List<RolOdiFotoOrnekFotograf> guncellenecekOrnekFotoListesi = new List<RolOdiFotoOrnekFotograf>();
            List<RolOdiFotoOrnekFotograf> yeniOrnekFotoListesi = new List<RolOdiFotoOrnekFotograf>();

            foreach (var item in updateDTO.OrnekFotograflar)
            {
                RolOdiFotoOrnekFotograf poz = _mapper.Map<RolOdiFotoOrnekFotograf>(item);
                poz.GuncellenmeTarihi = DateTime.Now;
                poz.Guncelleyen = user.AdSoyad;
                poz.GuncelleyenId = user.Id;

                guncellenecekOrnekFotoListesi.Add(poz);
            }
            await _projeRolOdiDataService.RolOdiFotoOrnekFotoListeGuncelle(guncellenecekOrnekFotoListesi);
            List<RolOdiFotoOrnekFotograf> silinenOrnekFotolar = new List<RolOdiFotoOrnekFotograf>();

            foreach (var item in eskiOrnekFotoListesi)
            {
                if (!guncellenecekOrnekFotoListesi.Any(x => x.Id == item.Id)) silinenOrnekFotolar.Add(item);
            }
            if (silinenOrnekFotolar != null)
            {
                await _projeRolOdiDataService.RolOdiOrnekFotoListesiSil(silinenOrnekFotolar);
            }

            if (updateDTO.YeniOrnekFotograflar != null)
            {
                yeniOrnekFotoListesi = _mapper.Map<List<RolOdiFotoOrnekFotograf>>(updateDTO.YeniOrnekFotograflar);
                foreach (var foto in yeniOrnekFotoListesi)
                {
                    foto.Ekleyen = user.AdSoyad;
                    foto.EkleyenId = user.Id;
                    foto.EklenmeTarihi = DateTime.Now;
                    foto.Guncelleyen = user.AdSoyad;
                    foto.GuncelleyenId = user.Id;
                    foto.GuncellenmeTarihi = DateTime.Now;
                }
                yeniOrnekFotoListesi = await _projeRolOdiDataService.YeniRolOdiFotoOrnekFotolar(yeniOrnekFotoListesi);
            }

            return OdiResponse<RolOdiFotoDTO>.Success("Rol Odi Foto Örnek Poz Listesi güncellendi", await RolOdiFotoGetir(updateDTO.ProjeRolOdiId), 200);
        }
        public async Task<RolOdiFotoDTO> RolOdiFotoGetir(string rolOdiId)
        {
            List<RolOdiFotoPoz> odiFotoList = await _projeRolOdiDataService.RolOdiFotoPozListesiGetir(rolOdiId);
            RolOdiFotoYonetmenNotu yonetmenNotu = await _projeRolOdiDataService.RolOdiFotoYonetmenNotuGetir(rolOdiId);
            List<RolOdiFotoOrnekFotograf> ornekFotoList = await _projeRolOdiDataService.RolOdiFotoOrnekFotoListesiGetir(rolOdiId);

            if (odiFotoList?.Any() == false && yonetmenNotu == null && ornekFotoList?.Any() == false)
                return null;

            RolOdiFotoDTO odiFoto = new RolOdiFotoDTO();

            odiFoto.OdiFotolar = _mapper.Map<List<RolOdiFotoPozOutputDTO>>(odiFotoList);
            odiFoto.YonetmenNotu = _mapper.Map<RolOdiFotoYonetmenNotuOutputDTO>(yonetmenNotu);
            odiFoto.OrnekFotograflar = _mapper.Map<List<RolOdiFotoOrnekFotografOutputDTO>>(ornekFotoList);

            if (odiFoto.OrnekFotograflar != null)
            {
                foreach (var foto in odiFoto.OrnekFotograflar)
                {
                    if (!string.IsNullOrEmpty(foto.OrnekFoto)) foto.OrnekFotoDisplay = _amazonService.GetPreSignedUrl(foto.OrnekFoto);
                }
            }

            return odiFoto;
        }

        #endregion
        #region SESMATİK
        public async Task<OdiResponse<RolOdiSesDTO>> YeniRolOdiSes(RolOdiSesmatikCreateDTO ses, OdiUser user)
        {
            List<RolOdiSes> sesList = _mapper.Map<List<RolOdiSes>>(ses.SesList);
            RolOdiSesYonetmenNotu not = _mapper.Map<RolOdiSesYonetmenNotu>(ses.YonetmenNotu);
            RolOdiSesYonetmenNotu ynotu = await _projeRolOdiDataService.RolOdiSesYonetmenNotuGetir(ses.ProjeRolOdiId);
            if (ynotu != null) return OdiResponse<RolOdiSesDTO>.Fail("Sesmatik Oluşturulamadı.Daha önce Sesmatik oluşturulmuş. Lütfen Güncelleme yapınız", "BadRequest", 400);
            RolOdiSesSenaryo senaryo = _mapper.Map<RolOdiSesSenaryo>(ses.Senaryo);
            RolOdiSesSenaryo snr = await _projeRolOdiDataService.RolOdiSesSenaryoGetir(ses.ProjeRolOdiId);
            if (snr != null) return OdiResponse<RolOdiSesDTO>.Fail("Sesmatik Oluşturulamadı.Daha önce Sesmatik oluşturulmuş. Lütfen Güncelleme yapınız", "BadRequest", 400);
            foreach (var odises in sesList)
            {
                odises.EklenmeTarihi = DateTime.Now;
                odises.Ekleyen = user.AdSoyad;
                odises.EkleyenId = user.Id;

                odises.GuncellenmeTarihi = DateTime.Now;
                odises.Guncelleyen = user.AdSoyad;
                odises.GuncelleyenId = user.Id;
            }

            await _projeRolOdiDataService.YeniRolOdiSesList(sesList);
            await _projeRolOdiDataService.YeniRolOdiSesYonetmenNotu(not);
            await _projeRolOdiDataService.YeniRolOdiSesSenaryo(senaryo);

            return OdiResponse<RolOdiSesDTO>.Success("Yeni Rol Odi Sesmatik oluşturuldu", await RolOdiSesGetir(ses.ProjeRolOdiId), 200);
        }

        public async Task<OdiResponse<RolOdiSesDTO>> RolOdiSesListesiGuncelle(RolOdiSesUpdateListDTO sesListDTO, OdiUser user)
        {
            List<RolOdiSes> eskiSesList = await _projeRolOdiDataService.RolOdiSesListesiGetir(sesListDTO.ProjeRolOdiId);
            List<RolOdiSes> guncellenecekSesListesi = new List<RolOdiSes>();
            List<RolOdiSes> yeniSesListesi = new List<RolOdiSes>();

            foreach (var sesDTO in sesListDTO.sesList)
            {
                RolOdiSes ses = _mapper.Map<RolOdiSes>(sesDTO);
                ses.GuncellenmeTarihi = DateTime.Now;
                ses.Guncelleyen = user.AdSoyad;
                ses.GuncelleyenId = user.Id;

                guncellenecekSesListesi.Add(ses);
            }

            await _projeRolOdiDataService.RolOdiSesListesiGuncelle(guncellenecekSesListesi);
            List<RolOdiSes> silinenSesler = new List<RolOdiSes>();

            foreach (var ses in eskiSesList)
            {
                if (!guncellenecekSesListesi.Any(x => x.Id == ses.Id)) silinenSesler.Add(ses);
            }
            if (silinenSesler != null)
            {
                await _projeRolOdiDataService.RolOdiSesListesiSil(silinenSesler);
            }

            if (sesListDTO.yeniSesList != null)
            {
                yeniSesListesi = _mapper.Map<List<RolOdiSes>>(sesListDTO.yeniSesList);
                foreach (var ses in yeniSesListesi)
                {
                    ses.Ekleyen = user.AdSoyad;
                    ses.EkleyenId = user.Id;
                    ses.EklenmeTarihi = DateTime.Now;
                    ses.Guncelleyen = user.AdSoyad;
                    ses.GuncelleyenId = user.Id;
                    ses.GuncellenmeTarihi = DateTime.Now;
                }
                yeniSesListesi = await _projeRolOdiDataService.YeniRolOdiSesList(yeniSesListesi);
            }

            return OdiResponse<RolOdiSesDTO>.Success("Yeni Rol Odi Ses Listesi Güncellendi", await RolOdiSesGetir(sesListDTO.ProjeRolOdiId), 200);
        }

        public async Task<OdiResponse<RolOdiSesDTO>> RolOdiSesYonetmenNotuGuncelle(RolOdiSesYonetmenNotuUpdateDTO not, OdiUser user)
        {
            RolOdiSesYonetmenNotu ynotu = await _projeRolOdiDataService.RolOdiSesYonetmenNotuGetir(not.ProjeRolOdiId);

            ynotu = _mapper.Map(not, ynotu);

            ynotu.GuncellenmeTarihi = DateTime.Now;
            ynotu.Guncelleyen = user.AdSoyad;
            ynotu.Guncelleyen = user.Id;

            await _projeRolOdiDataService.OdiRolSesYonetmenNotuGuncelle(ynotu);

            return OdiResponse<RolOdiSesDTO>.Success("Yeni Rol Odi Ses Yönetmen Notu Güncellendi", await RolOdiSesGetir(not.ProjeRolOdiId), 200);
        }

        public async Task<OdiResponse<RolOdiSesDTO>> RolOdiSesSenaryoGuncelle(RolOdiSesSenaryoUpdateDTO senaryo, OdiUser user)
        {
            RolOdiSesSenaryo sesSenaryo = await _projeRolOdiDataService.RolOdiSesSenaryoGetir(senaryo.ProjeRolOdiId);
            sesSenaryo = _mapper.Map(senaryo, sesSenaryo);

            sesSenaryo.GuncellenmeTarihi = DateTime.Now;
            sesSenaryo.Guncelleyen = user.AdSoyad;
            sesSenaryo.GuncelleyenId = user.Id;

            await _projeRolOdiDataService.RolOdiSesSenaryoGuncelle(sesSenaryo);

            return OdiResponse<RolOdiSesDTO>.Success("Yeni Rol Odi Ses Senaryo Notu Güncellendi", await RolOdiSesGetir(sesSenaryo.ProjeRolOdiId), 200);
        }

        public async Task<RolOdiSesDTO> RolOdiSesGetir(string rolOdiId)
        {
            List<RolOdiSes> rolOdiSesList = await _projeRolOdiDataService.RolOdiSesListesiGetir(rolOdiId);
            RolOdiSesYonetmenNotu rolOdiSesYonetmenNotu = await _projeRolOdiDataService.RolOdiSesYonetmenNotuGetir(rolOdiId);
            RolOdiSesSenaryo rolOdiSesSenaryo = await _projeRolOdiDataService.RolOdiSesSenaryoGetir(rolOdiId);

            if (rolOdiSesList?.Any() == false && rolOdiSesYonetmenNotu == null && rolOdiSesSenaryo == null) return null;

            RolOdiSesDTO sesDTO = new RolOdiSesDTO();

            sesDTO.SesList = _mapper.Map<List<RolOdiSesOutputDTO>>(rolOdiSesList);
            sesDTO.YonetmenNotu = _mapper.Map<RolOdiSesYonetmenNotuOutputDTO>(rolOdiSesYonetmenNotu);
            sesDTO.Senaryo = _mapper.Map<RolOdiSesSenaryoOutputDTO>(rolOdiSesSenaryo);

            if (sesDTO.Senaryo != null && sesDTO.Senaryo.Dosyami && !string.IsNullOrEmpty(sesDTO.Senaryo.Senaryo)) sesDTO.Senaryo.SenaryoDisplay = _amazonService.GetPreSignedUrl(sesDTO.Senaryo.Senaryo);

            return sesDTO;
        }

        #endregion

        #region VİDEOMATİK
        public async Task<OdiResponse<RolOdiVideomatikDTO>> YeniRolOdiVideomatik(RolOdiVideomatikCreateDTO video, OdiUser user)
        {
            RolOdiVideo odiVideo = _mapper.Map<RolOdiVideo>(video.Video);
            odiVideo.EklenmeTarihi = DateTime.Now;
            odiVideo.Ekleyen = user.AdSoyad;
            odiVideo.EkleyenId = user.Id;

            odiVideo.GuncellenmeTarihi = DateTime.Now;
            odiVideo.Guncelleyen = user.AdSoyad;
            odiVideo.GuncelleyenId = user.Id;
            if (odiVideo.DetayList != null)
            {
                foreach (var detay in odiVideo.DetayList)
                {
                    detay.GuncellenmeTarihi = DateTime.Now;
                    detay.Guncelleyen = user.AdSoyad;
                    detay.GuncelleyenId = user.Id;
                }
            }

            await _projeRolOdiDataService.YeniRolOdiVideo(odiVideo);

            List<RolOdiVideoOrnekOyun> ornoyunList = _mapper.Map<List<RolOdiVideoOrnekOyun>>(video.OrnekOyunList);
            await _projeRolOdiDataService.YeniRolOdiVideoOrnekOyunList(ornoyunList);

            RolOdiVideoSenaryo vSenaryo = _mapper.Map<RolOdiVideoSenaryo>(video.Senaryo);
            await _projeRolOdiDataService.YeniRolOdiVideoSenaryo(vSenaryo);

            RolOdiVideoYonetmenNotu yNotu = _mapper.Map<RolOdiVideoYonetmenNotu>(video.YonetmenNotu);
            await _projeRolOdiDataService.YeniRolOdiVideoYonetmenNotu(yNotu);

            return OdiResponse<RolOdiVideomatikDTO>.Success("Rol Odi Videomatik Eklendi", await RolOdiVideoGetir(video.ProjeRolOdiId), 200);
        }
        public async Task<OdiResponse<RolOdiVideomatikDTO>> RolOdiVideoGuncelle(RolOdiVideoUpdateDTO video, OdiUser user)
        {
            RolOdiVideo odiVideo = await _projeRolOdiDataService.RolOdiVideoGetir(video.ProjeRolOdiId);
            odiVideo.Baslik = video.Baslik;
            odiVideo.GuncellenmeTarihi = DateTime.Now;
            odiVideo.Guncelleyen = user.AdSoyad;
            odiVideo.GuncelleyenId = user.Id;

            await _projeRolOdiDataService.RolOdiVideoGuncelle(odiVideo);

            return OdiResponse<RolOdiVideomatikDTO>.Success("Rol Odi Video Güncellendi", await RolOdiVideoGetir(video.ProjeRolOdiId), 200);
        }

        public async Task<OdiResponse<RolOdiVideomatikDTO>> RolOdiVideoDetayGuncelle(RolOdiVideoDetayUpdateListDTO detayUpdate, OdiUser user)
        {
            List<RolOdiVideoDetay> eskiVideoDetayList = await _projeRolOdiDataService.RolOdiVideoDetayListGetir(detayUpdate.RolOdiVideoId);
            List<RolOdiVideoDetay> guncellenecekVideoDetayListesi = new List<RolOdiVideoDetay>();
            List<RolOdiVideoDetay> yeniVideoDetayListesi = new List<RolOdiVideoDetay>();

            foreach (var videoDetayDTO in detayUpdate.VideoDetayList)
            {
                RolOdiVideoDetay vDetay = _mapper.Map<RolOdiVideoDetay>(videoDetayDTO);
                vDetay.GuncellenmeTarihi = DateTime.Now;
                vDetay.Guncelleyen = user.AdSoyad;
                vDetay.GuncelleyenId = user.Id;

                guncellenecekVideoDetayListesi.Add(vDetay);
            }

            await _projeRolOdiDataService.RolOdiVideoDetayListGuncelle(guncellenecekVideoDetayListesi);
            List<RolOdiVideoDetay> silinecekVideoDetayList = new List<RolOdiVideoDetay>();

            foreach (var vDetay in eskiVideoDetayList)
            {
                if (!guncellenecekVideoDetayListesi.Any(x => x.Id == vDetay.Id)) silinecekVideoDetayList.Add(vDetay);
            }
            if (silinecekVideoDetayList != null)
            {
                await _projeRolOdiDataService.RolOdiVideoDetayListSil(silinecekVideoDetayList);
            }

            if (detayUpdate.YeniVideoDetayList != null)
            {
                yeniVideoDetayListesi = _mapper.Map<List<RolOdiVideoDetay>>(detayUpdate.YeniVideoDetayList);
                foreach (var vDetay in yeniVideoDetayListesi)
                {
                    vDetay.Id = Guid.NewGuid().ToString();
                    vDetay.Ekleyen = user.AdSoyad;
                    vDetay.EkleyenId = user.Id;
                    vDetay.EklenmeTarihi = DateTime.Now;
                    vDetay.Guncelleyen = user.AdSoyad;
                    vDetay.GuncelleyenId = user.Id;
                    vDetay.GuncellenmeTarihi = DateTime.Now;
                }
                yeniVideoDetayListesi = await _projeRolOdiDataService.YeniRolOdiVideoDetayList(yeniVideoDetayListesi);
            }

            return OdiResponse<RolOdiVideomatikDTO>.Success("Rol Odi Video Detay Listesi Güncellendi", await RolOdiVideoGetir(detayUpdate.ProjeRolOdiId), 200);
        }

        public async Task<OdiResponse<RolOdiVideomatikDTO>> RolOdiVideoOrnekOyunGuncelle(RolOdiVideoOrnekOyunUpdateDTO videoOrnekOyun, OdiUser user)
        {
            RolOdiVideoOrnekOyun ornekOyun = await _projeRolOdiDataService.RolOdiVideoOrnekOyunGetir(videoOrnekOyun.RolOdiVideoOrnekOyunId);
            ornekOyun = _mapper.Map(videoOrnekOyun, ornekOyun);

            ornekOyun.GuncellenmeTarihi = DateTime.Now;
            ornekOyun.Guncelleyen = user.AdSoyad;
            ornekOyun.GuncelleyenId = user.Id;

            await _projeRolOdiDataService.RolOdiVideoOrnekOyunGuncelle(ornekOyun);

            return OdiResponse<RolOdiVideomatikDTO>.Success("Rol Odi Video Örnek Oyun Güncellendi", await RolOdiVideoGetir(videoOrnekOyun.ProjeRolOdiId), 200);
        }

        public async Task<OdiResponse<RolOdiVideomatikDTO>> YeniRolOdiVideoOrnek(RolOdiVideoOrnekOyunCreateDTO ornekOyun, OdiUser user)
        {
            RolOdiVideoOrnekOyun oyun = _mapper.Map<RolOdiVideoOrnekOyun>(ornekOyun);

            oyun.EklenmeTarihi = DateTime.Now;
            oyun.Ekleyen = user.AdSoyad;
            oyun.EkleyenId = user.Id;

            oyun.GuncellenmeTarihi = DateTime.Now;
            oyun.Guncelleyen = user.AdSoyad;
            oyun.GuncelleyenId = user.Id;

            await _projeRolOdiDataService.YeniRolOdiVideoOrnekOyun(oyun);

            return OdiResponse<RolOdiVideomatikDTO>.Success("Yeni Rol Odi Video Örnek Oyun eklendi", await RolOdiVideoGetir(ornekOyun.ProjeRolOdiId), 200);
        }

        public async Task<OdiResponse<RolOdiVideomatikDTO>> RolOdiVideoOrnekOyunSil(RolOdiVideoOrnekOyunIdDTO id)
        {
            bool sonuc = await _projeRolOdiDataService.RolOdiVideoOrnekOyunSil(id.RolOdiVideoOrnekOyunId);
            if (!sonuc) return OdiResponse<RolOdiVideomatikDTO>.Fail("Bu Id ile Video örnek oyun bulunmamaktadır", "Bad Request", 400);

            return OdiResponse<RolOdiVideomatikDTO>.Success("Rol Odi Video Örnek Oyun Silindi", await RolOdiVideoGetir(id.ProjeRolOdiId), 200);
        }

        public async Task<OdiResponse<RolOdiVideomatikDTO>> RolOdiVideoSenaryoGuncelle(RolOdiVideoSenaryoUpdateDTO senaryo, OdiUser user)
        {
            RolOdiVideoSenaryo vSenaryo = await _projeRolOdiDataService.RolOdiVideoSenaryoGetir(senaryo.ProjeRolOdiId);
            vSenaryo = _mapper.Map(senaryo, vSenaryo);

            vSenaryo.GuncellenmeTarihi = DateTime.Now;
            vSenaryo.Guncelleyen = user.AdSoyad;
            vSenaryo.GuncelleyenId = user.Id;
            await _projeRolOdiDataService.RolOdiVideoSenaryoGuncelle(vSenaryo);
            return OdiResponse<RolOdiVideomatikDTO>.Success("Rol Odi Video Senaryo Güncellendi", await RolOdiVideoGetir(senaryo.ProjeRolOdiId), 200);
        }

        public async Task<OdiResponse<RolOdiVideomatikDTO>> RolOdiVideoYonetmenNotuGuncelle(RolOdiVideoYonetmenNotuUpdateDTO not, OdiUser user)
        {
            RolOdiVideoYonetmenNotu ynot = await _projeRolOdiDataService.RolOdiVideoYonetmenNotuGetir(not.ProjeRolOdiId);
            ynot = _mapper.Map(not, ynot);

            ynot.GuncellenmeTarihi = DateTime.Now;
            ynot.Guncelleyen = user.AdSoyad;
            ynot.GuncelleyenId = user.Id;
            await _projeRolOdiDataService.RolOdiVideoYonetmenNotuGuncelle(ynot);
            return OdiResponse<RolOdiVideomatikDTO>.Success("Rol Odi Video Yonetmen Notu Güncellendi", await RolOdiVideoGetir(not.ProjeRolOdiId), 200);
        }

        public async Task<RolOdiVideomatikDTO> RolOdiVideoGetir(string rolOdiId)
        {
            RolOdiVideomatikDTO videomatik = new RolOdiVideomatikDTO();

            videomatik.Video = _mapper.Map<RolOdiVideoOutputDTO>(await _projeRolOdiDataService.RolOdiVideoGetir(rolOdiId));
            videomatik.OrnekOyun = _mapper.Map<List<RolOdiVideoOrnekOyunOutputDTO>>(await _projeRolOdiDataService.RolOdiVideoOrnekOyunListGetir(rolOdiId));
            videomatik.Senaryo = _mapper.Map<RolOdiVideoSenaryoOutputDTO>(await _projeRolOdiDataService.RolOdiVideoSenaryoGetir(rolOdiId));
            videomatik.YonetmenNotu = _mapper.Map<RolOdiVideoYonetmenNotuOutputDTO>(await _projeRolOdiDataService.RolOdiVideoYonetmenNotuGetir(rolOdiId));

            if (videomatik.OrnekOyun?.Any() == false
                && videomatik.Video == null
                && videomatik.Senaryo == null
                && videomatik.YonetmenNotu == null)
            {
                return null;
            }

            if (videomatik != null && videomatik.Video != null && videomatik.Video.DetayList != null)
            {
                foreach (var detay in videomatik.Video.DetayList)
                {
                    if (detay != null && detay.Dosyami && !string.IsNullOrEmpty(detay.SesDosyasi)) detay.SesDosyasiDisplay = _amazonService.GetPreSignedUrl(detay.SesDosyasi);
                }
            }

            if (videomatik != null && videomatik.OrnekOyun != null)
            {
                foreach (var oyun in videomatik.OrnekOyun)
                {
                    if (oyun != null && !string.IsNullOrEmpty(oyun.OrnekOyun)) oyun.OrnekOyunDisplay = _amazonService.GetPreSignedUrl(oyun.OrnekOyun);
                }
            }

            if (videomatik.Senaryo != null && videomatik.Senaryo.Dosyami
                && !string.IsNullOrEmpty(videomatik.Senaryo.Senaryo))
                videomatik.Senaryo.SenaryoDisplay = _amazonService.GetPreSignedUrl(videomatik.Senaryo.Senaryo);

            return videomatik;
        }

        #endregion

        #region SORUMATİK

        public async Task<RolOdiSorumatikDTO> RolOdiSoruGetir(string rolOdiId)
        {
            RolOdiSorumatikDTO sorumatik = new RolOdiSorumatikDTO();
            sorumatik.ProjRolOdiId = rolOdiId;
            sorumatik.SoruList = _mapper.Map<List<RolOdiSoruOutputDTO>>(await _projeRolOdiDataService.RolOdiSoruListGetir(rolOdiId));
            sorumatik.Aciklama = _mapper.Map<RolOdiSoruAciklamaOutputDTO>(await _projeRolOdiDataService.RolOdiSoruAciklamaGetir(rolOdiId));

            if (sorumatik.SoruList?.Any() == false && sorumatik.Aciklama == null) return null;

            return sorumatik;
        }
        public async Task<OdiResponse<RolOdiSorumatikDTO>> YeniRolOdiSorumatik(RolOdiSorumatikCreateDTO sorumatik, OdiUser user)
        {
            List<RolOdiSoru> soruList = _mapper.Map<List<RolOdiSoru>>(sorumatik.SoruList);
            RolOdiSoruAciklama aciklama = _mapper.Map<RolOdiSoruAciklama>(sorumatik.Aciklama);

            foreach (var soru in soruList)
            {

                soru.EklenmeTarihi = DateTime.Now;
                soru.Ekleyen = user.AdSoyad;
                soru.EkleyenId = user.Id;

                soru.GuncellenmeTarihi = DateTime.Now;
                soru.Guncelleyen = user.AdSoyad;
                soru.GuncelleyenId = user.Id;

                foreach (var cevap in soru.CevapSecenekleri)
                {

                    cevap.EklenmeTarihi = DateTime.Now;
                    cevap.Ekleyen = user.AdSoyad;
                    cevap.EkleyenId = user.Id;

                    cevap.GuncellenmeTarihi = DateTime.Now;
                    cevap.Guncelleyen = user.AdSoyad;
                    cevap.GuncelleyenId = user.Id;
                }




            }
            await _projeRolOdiDataService.YeniRolOdiSoruList(soruList);

            aciklama.EklenmeTarihi = DateTime.Now;
            aciklama.Ekleyen = user.AdSoyad;
            aciklama.EkleyenId = user.Id;

            aciklama.GuncellenmeTarihi = DateTime.Now;
            aciklama.Guncelleyen = user.AdSoyad;
            aciklama.GuncelleyenId = user.Id;
            RolOdiSoruAciklama acklm = await _projeRolOdiDataService.RolOdiSoruAciklamaGetir(sorumatik.ProjeRolOdiId);

            if (acklm == null)
                await _projeRolOdiDataService.YeniRolOdiSoruAciklama(aciklama);

            return OdiResponse<RolOdiSorumatikDTO>.Success("Yeni Sorumatik oluşturuldu", await RolOdiSoruGetir(sorumatik.ProjeRolOdiId), 200);
        }
        public async Task<OdiResponse<RolOdiSorumatikDTO>> RolOdiSoruGuncelle(RolOdiSoruUpdateDTO soru, OdiUser user)
        {
            RolOdiSoru odiSoru = _mapper.Map<RolOdiSoru>(soru);

            odiSoru.GuncellenmeTarihi = DateTime.Now;
            odiSoru.Guncelleyen = user.AdSoyad;
            odiSoru.GuncelleyenId = user.Id;

            await _projeRolOdiDataService.RolOdiSoruGuncelle(odiSoru);

            List<RolOdiSoruCevapSecenek> eskiSoruCevapList = await _projeRolOdiDataService.RolOdiSoruCevapListGetir(soru.RolOdiSoruId);
            List<RolOdiSoruCevapSecenek> guncellenecekSoruCevapListesi = new List<RolOdiSoruCevapSecenek>();
            List<RolOdiSoruCevapSecenek> yeniSoruCevapListesi = new List<RolOdiSoruCevapSecenek>();

            foreach (var cevapDTO in soru.CevapSecenekleri)
            {
                RolOdiSoruCevapSecenek cevap = _mapper.Map<RolOdiSoruCevapSecenek>(cevapDTO);
                cevap.GuncellenmeTarihi = DateTime.Now;
                cevap.Guncelleyen = user.AdSoyad;
                cevap.GuncelleyenId = user.Id;

                guncellenecekSoruCevapListesi.Add(cevap);
            }

            await _projeRolOdiDataService.RolOdiSoruCevapListGuncelle(guncellenecekSoruCevapListesi);
            List<RolOdiSoruCevapSecenek> silinecekSoruCevapList = new List<RolOdiSoruCevapSecenek>();

            foreach (var cevap in eskiSoruCevapList)
            {
                if (!guncellenecekSoruCevapListesi.Any(x => x.Id == cevap.Id)) silinecekSoruCevapList.Add(cevap);
            }
            if (silinecekSoruCevapList != null)
            {
                await _projeRolOdiDataService.RolOdiSoruCevapListSil(silinecekSoruCevapList);
            }

            if (soru.YeniCevapSecekleri != null)
            {
                yeniSoruCevapListesi = _mapper.Map<List<RolOdiSoruCevapSecenek>>(soru.YeniCevapSecekleri);
                foreach (var cevap in yeniSoruCevapListesi)
                {
                    cevap.Id = Guid.NewGuid().ToString();
                    cevap.Ekleyen = user.AdSoyad;
                    cevap.EkleyenId = user.Id;
                    cevap.EklenmeTarihi = DateTime.Now;
                    cevap.Guncelleyen = user.AdSoyad;
                    cevap.GuncelleyenId = user.Id;
                    cevap.GuncellenmeTarihi = DateTime.Now;
                }
                yeniSoruCevapListesi = await _projeRolOdiDataService.YeniRolOdiSoruCevapList(yeniSoruCevapListesi);
            }

            return OdiResponse<RolOdiSorumatikDTO>.Success("Rol Odi Soru güncellendi", await RolOdiSoruGetir(soru.ProjeRolOdiId), 200);
        }
        public async Task<OdiResponse<RolOdiSorumatikDTO>> RolOdiSoruSil(RolOdiSoruIdDTO id)
        {
            RolOdiSoru soru = await _projeRolOdiDataService.RolOdiSoruGetir(id.RolOdiSoruId);
            string odiId = soru.ProjeRolOdiId;
            await _projeRolOdiDataService.RolOdiSoruSil(id.RolOdiSoruId);
            return OdiResponse<RolOdiSorumatikDTO>.Success("RolOdiSoruSilindi", await RolOdiSoruGetir(odiId), 200);
        }
        public async Task<OdiResponse<RolOdiSorumatikDTO>> RolOdiSoruAciklamaGuncelle(RolOdiSoruAciklamaUpdateDTO aciklama, OdiUser user)
        {
            RolOdiSoruAciklama sAciklama = _mapper.Map<RolOdiSoruAciklama>(aciklama);

            sAciklama.GuncellenmeTarihi = DateTime.Now;
            sAciklama.Guncelleyen = user.AdSoyad;
            sAciklama.GuncelleyenId = user.Id;

            await _projeRolOdiDataService.RolOdiSoruAciklamaGuncelle(sAciklama);
            return OdiResponse<RolOdiSorumatikDTO>.Success("Rol Odi Soru Açıklama Güncellendi", await RolOdiSoruGetir(aciklama.ProjeRolOdiId), 200);
        }
        #endregion
    }
}
