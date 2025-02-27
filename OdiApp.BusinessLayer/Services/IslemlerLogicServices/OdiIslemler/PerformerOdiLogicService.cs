using AutoMapper;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.DataAccessLayer.IslemlerDataServices.OdiIslemler;
using OdiApp.DTOs.Enums;
using OdiApp.DTOs.IslemlerDTOs.OdiIslemler.OdiTalepDTOs;
using OdiApp.DTOs.IslemlerDTOs.OdiIslemler.PerformerOdiDTO;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.IslemlerModels.OdiIslemler;

namespace OdiApp.BusinessLayer.Services.IslemlerLogicServices.OdiIslemler
{
    public class PerformerOdiLogicService : IPerformerOdiLogicService
    {
        IMapper _mapper;
        IPerformerOdiDataService _performerOdiDataService;
        IOdiIslemDataService _odiIslemDataService;
        IAmazonS3Service _amazonS3Service;
        public PerformerOdiLogicService(IMapper mapper, IPerformerOdiDataService performerOdiDataService, IOdiIslemDataService odiIslemDataService, IAmazonS3Service amazonS3Service)
        {
            _mapper = mapper;
            _performerOdiDataService = performerOdiDataService;
            _odiIslemDataService = odiIslemDataService;
            _amazonS3Service = amazonS3Service;
        }
        public async Task<OdiResponse<PerformerOdiOutputDTO>> YeniPerformerOdi(PerformerOdiCreateDTO performerOdi, OdiUser user)
        {
            PerformerOdi odi = await _performerOdiDataService.PerformerOdiGetir(performerOdi.OdiTalepId);
            OdiTalep odiTalep = await _odiIslemDataService.OdiTalepGetirById(performerOdi.OdiTalepId);
            if (odi != null) return OdiResponse<PerformerOdiOutputDTO>.Fail("Performer  bu proje ve rol için odi oluşturmuş.", "Bad Request", 400);

            odi = _mapper.Map<PerformerOdi>(performerOdi);
            odi.EklenmeTarihi = DateTime.Now;
            odi.Ekleyen = user.AdSoyad;
            odi.EkleyenId = user.Id;

            odi.GuncellenmeTarihi = DateTime.Now;
            odi.Guncelleyen = user.AdSoyad;
            odi.GuncelleyenId = user.Id;

            if (odi.PerformerOdiFotograflar != null)
            {
                foreach (var foto in odi.PerformerOdiFotograflar)
                {
                    foto.EklenmeTarihi = DateTime.Now;
                    foto.Ekleyen = user.AdSoyad;
                    foto.EkleyenId = user.Id;

                    foto.GuncellenmeTarihi = DateTime.Now;
                    foto.Guncelleyen = user.AdSoyad;
                    foto.GuncelleyenId = user.Id;
                }
            }
            if (odi.PerformerOdiSorular != null)
            {
                foreach (var soru in odi.PerformerOdiSorular)
                {
                    soru.EklenmeTarihi = DateTime.Now;
                    soru.Ekleyen = user.AdSoyad;
                    soru.EkleyenId = user.Id;

                    soru.GuncellenmeTarihi = DateTime.Now;
                    soru.Guncelleyen = user.AdSoyad;
                    soru.GuncelleyenId = user.Id;
                }
            }
            if (odi.PerformerOdiVideo != null)
            {
                odi.PerformerOdiVideo.EklenmeTarihi = DateTime.Now;
                odi.PerformerOdiVideo.Ekleyen = user.AdSoyad;
                odi.PerformerOdiVideo.EkleyenId = user.Id;

                odi.PerformerOdiVideo.GuncellenmeTarihi = DateTime.Now;
                odi.PerformerOdiVideo.Guncelleyen = user.AdSoyad;
                odi.PerformerOdiVideo.GuncelleyenId = user.Id;
            }
            if (odi.PerformerOdiSes != null)
            {
                odi.PerformerOdiSes.EklenmeTarihi = DateTime.Now;
                odi.PerformerOdiSes.Ekleyen = user.AdSoyad;
                odi.PerformerOdiSes.EkleyenId = user.Id;

                odi.PerformerOdiSes.GuncellenmeTarihi = DateTime.Now;
                odi.PerformerOdiSes.Guncelleyen = user.AdSoyad;
                odi.PerformerOdiSes.GuncelleyenId = user.Id;
            }

            //yükleyen performer değil ise menajerdir. İzlenebilmesi MenajerOnayinaGonder=true olmalı
            if (!string.Equals(user.KayitGrubuKodu, KayitGrupKodlari.Yetenek))
            {
                odi.MenajerOnayinaGonder = true;
                odi.MenajerInceledi = true;
                odi.MenajerOnayinaGonderTarih = DateTime.Now;
                odi.MenajerIncelediTarih = DateTime.Now;


            }
            odi = await _performerOdiDataService.YeniPerformerOdi(odi);

            //yükleyen yetenek değil ise menajer onayı var demektir. Odi yüklendi bilgisi bu nedenle doldurulmalı. Menajer onayı nı yapmıyoruz. Menajer onayı demek yapım ekibinin izleyebilmesi anlamına gelir. Onun için menajerin yapım ekibine gönder(odi onayla) methoduyla onaylamalı. 
            if (!string.Equals(user.KayitGrubuKodu, KayitGrupKodlari.Yetenek))
            {
                odiTalep.OdiYuklendi = true;
                odiTalep.OdiYuklendiTarihi = DateTime.Now;



                odiTalep.GuncellenmeTarihi = DateTime.Now;
                odiTalep.Guncelleyen = user.AdSoyad;
                odiTalep.GuncelleyenId = user.Id;

                await _odiIslemDataService.OdiTalepGuncelle(odiTalep);
            }

            return OdiResponse<PerformerOdiOutputDTO>.Success("Performer Odi kayıt edildi", _mapper.Map<PerformerOdiOutputDTO>(odi), 200);
        }

        public async Task<OdiResponse<PerformerOdiOutputDTO>> PerformerOdiGuncelle(PerformerOdiUpdateDTO performerOdi, OdiUser user)
        {
            PerformerOdi odi = await _performerOdiDataService.PerformerOdiGetir(performerOdi.OdiTalepId);
            if (odi == null) return OdiResponse<PerformerOdiOutputDTO>.Fail("Bu id ile  bir odi bulunmamaktadır", "Not found", 404);
            if (odi.MenajerOnayinaGonder == true && string.Equals(user.KayitGrubuKodu, KayitGrupKodlari.Yetenek))
                return OdiResponse<PerformerOdiOutputDTO>.Fail("Odi menajer onayına sunulduğu için güncellenemez", "Bad Request", 400);

            odi = _mapper.Map(performerOdi, odi);

            //Eğer tekrar çekilme talebi varsa; yeni odi yüklendiği için önce tekrar çekme talebini kaldırıyoruz. Sonra tekrar çekildiğine dair bilgileri ekliyoruz
            if (odi.TekrarCekTalebi)
            {
                //tekrar çekme talebinin kaldırılması
                odi.TekrarCekTalebi = false;
                odi.SonTekrarCekTalepTarihi = null;
                odi.SonTekrarCekTalebiMenajerOnerisi = null;

                //tekrar odi çekildiğinin bilgileri
                odi.PerformerSonTekrarCekti = true;
                odi.PerformerSonTekrarCektiTarihi = DateTime.Now;
            }

            odi.GuncellenmeTarihi = DateTime.Now;
            odi.Guncelleyen = user.AdSoyad;
            odi.GuncelleyenId = user.Id;

            odi.PerformerOdiFotograflar = null;
            odi.PerformerOdiSes = null;
            odi.PerformerOdiSorular = null;
            odi.PerformerOdiVideo = null;

            odi = await _performerOdiDataService.PerformerOdiGuncelle(odi);
            if (odi.OdiSoru)
            {
                List<PerformerOdiSoru> sorular = await _performerOdiDataService.PerformerOdiSoruListesi(odi.Id);


                foreach (var soru in sorular)
                {
                    PerformerOdiSoru soru2 = _mapper.Map<PerformerOdiSoruUpdateDTO, PerformerOdiSoru>(performerOdi.PerformerOdiSorular.FirstOrDefault(x => x.PerformerOdiSoruId == soru.Id), soru);
                    soru2.GuncellenmeTarihi = DateTime.Now;
                    soru2.Guncelleyen = user.AdSoyad;
                    soru2.GuncelleyenId = user.Id;
                    await _performerOdiDataService.PerformerOdiSoruGuncelle(soru2);
                }

            }
            if (odi.OdiFotograf)
            {
                List<PerformerOdiFotograf> fotolar = await _performerOdiDataService.PerformerOdiFotografListesi(odi.Id);

                foreach (var foto in fotolar)
                {
                    PerformerOdiFotograf foto2 = _mapper.Map<PerformerOdiFotografUpdateDTO, PerformerOdiFotograf>(performerOdi.PerformerOdiFotograflar.FirstOrDefault(x => x.PerformerOdiFotografId == foto.Id), foto);
                    foto2.GuncellenmeTarihi = DateTime.Now;
                    foto2.Guncelleyen = user.AdSoyad;
                    foto2.GuncelleyenId = user.Id;
                    await _performerOdiDataService.PerformerOdiFotografGuncelle(foto2);
                }

            }
            if (odi.OdiSes)
            {
                PerformerOdiSes ses = await _performerOdiDataService.PerformerOdiSesGetir(odi.Id);
                ses = _mapper.Map(performerOdi.PerformerOdiSes, ses);

                ses.GuncellenmeTarihi = DateTime.Now;
                ses.GuncelleyenId = user.Id;
                ses.Guncelleyen = user.AdSoyad;

                await _performerOdiDataService.PerformerOdiSesGuncelle(ses);
            }
            if (odi.OdiVideo)
            {
                PerformerOdiVideo video = await _performerOdiDataService.PerformerOdiVideoGetir(odi.Id);
                video = _mapper.Map(performerOdi.PerformerOdiVideo, video);

                video.GuncellenmeTarihi = DateTime.Now;
                video.GuncelleyenId = user.Id;
                video.Guncelleyen = user.AdSoyad;

                await _performerOdiDataService.PerformerOdiVideoGuncelle(video);
            }
            odi = await _performerOdiDataService.PerformerOdiGetirbyId(odi.Id);
            return OdiResponse<PerformerOdiOutputDTO>.Success("Odi Guncellendi", PerformerOdiOuputGetir(odi), 200);
        }
        public async Task<OdiResponse<bool>> MenajerOnayinaGonder(string performerOdiId)
        {
            PerformerOdi odi = await _performerOdiDataService.PerformerOdiGetirbyId(performerOdiId);
            if (odi == null) return OdiResponse<bool>.Fail("Bu id ile  bir odi bulunmamaktadır", "Not found", 404);

            odi.MenajerOnayinaGonder = true;
            await _performerOdiDataService.PerformerOdiGuncelle(odi);

            OdiTalep talep = await _odiIslemDataService.OdiTalepGetirById(odi.OdiTalepId);
            talep.OdiYuklendi = true;
            talep.OdiYuklendiTarihi = DateTime.Now;
            await _odiIslemDataService.OdiTalepGuncelle(talep);
            return OdiResponse<bool>.Success("Odi menajer onayına gönderdildi", true, 200);
        }
        public async Task<OdiResponse<PerformerOdiOutputDTO>> PerformerOdiDetayGetir(OdiTalepIdDTO talepId)
        {
            PerformerOdi odi = await _performerOdiDataService.PerformerOdiGetir(talepId.ToString());
            if (odi == null) return OdiResponse<PerformerOdiOutputDTO>.Fail("Bu id ile  bir odi bulunmamaktadır", "Not found", 404);


            return OdiResponse<PerformerOdiOutputDTO>.Success("Performer Odi getirildi", PerformerOdiOuputGetir(odi), 200);

        }
        public async Task<OdiResponse<PerformerOdiOutputDTO>> PerformerOdiMenajerInceledi(PerformerOdiIdDTO talepId)
        {
            PerformerOdi odi = await _performerOdiDataService.PerformerOdiGetirbyId(talepId.ToString());
            string result = "";
            if (odi.MenajerInceledi)
            {
                odi.MenajerInceledi = false;
                odi.MenajerIncelediTarih = null;
                result = "Odi menajer tarafından incelendi bilgisi kaldırıldı";
            }
            else
            {
                odi.MenajerInceledi = true;
                odi.MenajerIncelediTarih = DateTime.Now;
                result = "Odi menajer tarafından incelendi";
            }


            odi = await _performerOdiDataService.PerformerOdiGuncelle(odi);

            return OdiResponse<PerformerOdiOutputDTO>.Success(result, PerformerOdiOuputGetir(odi), 200);
        }
        public async Task<OdiResponse<PerformerOdiOutputDTO>> PerformerOdiMenajerAktifPasif(PerformerOdiIdDTO talepId)
        {
            PerformerOdi odi = await _performerOdiDataService.PerformerOdiGetirbyId(talepId.ToString());
            string result = "";
            if (odi.MenajerAktif)
            {
                odi.MenajerAktif = false;
                result = "Odi menajer tarafından pasif olarak işaretlendi";
            }
            else
            {
                odi.MenajerAktif = true;
                result = "Odi menajer tarafından aktif olarak işaretlendi";
            }


            odi = await _performerOdiDataService.PerformerOdiGuncelle(odi);

            return OdiResponse<PerformerOdiOutputDTO>.Success(result, PerformerOdiOuputGetir(odi), 200);
        }
        public async Task<OdiResponse<PerformerOdiOutputDTO>> PerformerOdiMenajerGizle(PerformerOdiIdDTO talepId)
        {
            PerformerOdi odi = await _performerOdiDataService.PerformerOdiGetirbyId(talepId.ToString());
            if (!odi.MenajerGizle)
            {
                odi.MenajerGizle = true;
            }

            odi = await _performerOdiDataService.PerformerOdiGuncelle(odi);

            return OdiResponse<PerformerOdiOutputDTO>.Success("Odi menajer tarafından gizlendi. Menajer Odi listesinde e gösterilmeyecek.", PerformerOdiOuputGetir(odi), 200);
        }
        public async Task<OdiResponse<PerformerOdiOutputDTO>> PerformerOdiMenajerOnayi(PerformerOdiIdDTO talepId)
        {
            PerformerOdi odi = await _performerOdiDataService.PerformerOdiGetirbyId(talepId.ToString());
            OdiTalep otalep = await _odiIslemDataService.OdiTalepGetirById(odi.OdiTalepId);
            if (!odi.MenajerOnayi)
            {
                odi.MenajerOnayi = true;
                odi.MenajerOnayTarihi = DateTime.Now;
                otalep.MenajerOdiOnayi = true;
                otalep.MenajerOdiOnayTarihi = DateTime.Now;


            }
            otalep = await _odiIslemDataService.OdiTalepGuncelle(otalep);
            odi = await _performerOdiDataService.PerformerOdiGuncelle(odi);

            return OdiResponse<PerformerOdiOutputDTO>.Success("Odi menajer tarafından onaylandı. Artık yapım ekibi odi yi görünteleyebilecek.", PerformerOdiOuputGetir(odi), 200);
        }
        public async Task<OdiResponse<PerformerOdiOutputDTO>> PerformerOdiMenajerTekrarCekTalebi(PerformerOdiMenajerTekrarCekInputDTO tekrarCek)
        {
            PerformerOdi odi = await _performerOdiDataService.PerformerOdiGetirbyId(tekrarCek.PerformerOdiId);
            if (!odi.TekrarCekTalebi)
            {
                odi.TekrarCekTalebi = true;
                odi.SonTekrarCekTalepTarihi = DateTime.Now;
                odi.SonTekrarCekTalebiMenajerOnerisi = tekrarCek.MenajerOnerisi;
            }
            else
            {
                odi.TekrarCekTalebi = false;
                odi.SonTekrarCekTalepTarihi = null;
                odi.SonTekrarCekTalebiMenajerOnerisi = "";
            }

            odi = await _performerOdiDataService.PerformerOdiGuncelle(odi);

            PerformerOdiTekrarCekOneri oneri = new PerformerOdiTekrarCekOneri();
            oneri.PerformerOdiId = tekrarCek.PerformerOdiId;
            oneri.MenajerOnerisi = tekrarCek.MenajerOnerisi;
            oneri.OneriTarihi = DateTime.Now;

            oneri = await _performerOdiDataService.YeniPerformerOdiTekrarCekOnerisi(oneri);

            odi.TekrarCekOneriListesi = await _performerOdiDataService.PerformerOdiTekrarCekOneriListesi(oneri.PerformerOdiId);

            return OdiResponse<PerformerOdiOutputDTO>.Success("MenajerOdi tekrar çekilmesini talep etti", PerformerOdiOuputGetir(odi), 200);
        }
        //
        public PerformerOdiOutputDTO PerformerOdiOuputGetir(PerformerOdi odi)
        {
            PerformerOdiOutputDTO odiOutput = _mapper.Map<PerformerOdiOutputDTO>(odi);

            if (odiOutput.PerformerOdiFotograflar != null)
            {
                foreach (var foto in odiOutput.PerformerOdiFotograflar)
                {
                    if (!string.IsNullOrEmpty(foto.Fotograf)) foto.FotografPresignedURL = _amazonS3Service.GetPreSignedUrl(foto.Fotograf);
                }
            }
            if (!string.IsNullOrEmpty(odiOutput.PerformerOdiVideo.Video)) odiOutput.PerformerOdiVideo.VideoPresignedUrl = _amazonS3Service.GetPreSignedUrl(odiOutput.PerformerOdiVideo.Video);
            if (!string.IsNullOrEmpty(odiOutput.PerformerOdiSes.Video)) odiOutput.PerformerOdiSes.VideoPresignedUrl = _amazonS3Service.GetPreSignedUrl(odiOutput.PerformerOdiSes.Video);
            return odiOutput;
        }
        public async Task<PerformerOdi> PerformerOdiGetir(string odiTalepId)
        {
            return await _performerOdiDataService.PerformerOdiGetir(odiTalepId);
        }
        public async Task<OdiTalepOutputDTO> OdiTalepGetir(string odiTalepId)
        {
            return await _odiIslemDataService.OdiTalepGetir(odiTalepId);
        }

        public async Task<OdiResponse<PerformerOdiOutputDTO>> PerformerOdiYapimInceledi(PerformerOdiIdDTO talepId)
        {
            PerformerOdi odi = await _performerOdiDataService.PerformerOdiGetirbyId(talepId.ToString());
            string result = "";
            if (odi.YapimInceledi)
            {
                odi.YapimInceledi = false;
                odi.YapimIncelediTarih = null;
                result = "Odi yapım tarafından incelendi bilgisi kaldırıldı";
            }
            else
            {
                odi.YapimInceledi = true;
                odi.YapimIncelediTarih = DateTime.Now;
                result = "Odi yapım tarafından incelendi";
            }


            odi = await _performerOdiDataService.PerformerOdiGuncelle(odi);

            return OdiResponse<PerformerOdiOutputDTO>.Success(result, PerformerOdiOuputGetir(odi), 200);
        }

        public async Task<OdiResponse<PerformerOdiOutputDTO>> PerformerOdiYapimAktifPasif(PerformerOdiIdDTO talepId)
        {
            PerformerOdi odi = await _performerOdiDataService.PerformerOdiGetirbyId(talepId.ToString());
            string result = "";
            if (odi.YapimAktif)
            {
                odi.YapimAktif = false;
                result = "Odi yapım tarafından pasif olarak işaretlendi";
            }
            else
            {
                odi.YapimAktif = true;
                result = "Odi yapım tarafından aktif olarak işaretlendi";
            }


            odi = await _performerOdiDataService.PerformerOdiGuncelle(odi);

            return OdiResponse<PerformerOdiOutputDTO>.Success(result, PerformerOdiOuputGetir(odi), 200);
        }

        public async Task<OdiResponse<PerformerOdiOutputDTO>> PerformerOdiYapimGizle(PerformerOdiIdDTO talepId)
        {
            PerformerOdi odi = await _performerOdiDataService.PerformerOdiGetirbyId(talepId.ToString());
            if (!odi.YapimGizle)
            {
                odi.YapimGizle = true;
            }

            odi = await _performerOdiDataService.PerformerOdiGuncelle(odi);

            return OdiResponse<PerformerOdiOutputDTO>.Success("Odi yapım tarafından gizlendi. Yapım Odi listesinde e gösterilmeyecek.", PerformerOdiOuputGetir(odi), 200);
        }
    }
}
