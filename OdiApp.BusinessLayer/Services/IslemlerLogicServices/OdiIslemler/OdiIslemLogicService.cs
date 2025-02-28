using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.DataAccessLayer.IslemlerDataServices.OdiIslemler;
using OdiApp.DTOs.IslemlerDTOs.OdiIslemler.OdiIzlemeListesiDTO;
using OdiApp.DTOs.IslemlerDTOs.OdiIslemler.OdiTalepDTOs;
using OdiApp.DTOs.IslemlerDTOs.OdiIslemler.PerformerOdiDTO;
using OdiApp.DTOs.IslemlerDTOs.OpsiyonIslemler;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeBilgileriDTOs;
using OdiApp.EntityLayer.IslemlerModels.OdiIslemler;

namespace OdiApp.BusinessLayer.Services.IslemlerLogicServices.OdiIslemler
{
    public class OdiIslemLogicService : IOdiIslemLogicService
    {
        private readonly IOdiIslemDataService _odiTalepDataService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUseOtherService _useOtherService;
        private readonly IAmazonS3Service _amazonS3Service;
        public OdiIslemLogicService(IOdiIslemDataService odiTalepDataService, IMapper mapper, IConfiguration configuration, IUseOtherService useOtherService, IAmazonS3Service amazonS3Service)
        {
            _odiTalepDataService = odiTalepDataService;
            _mapper = mapper;
            _configuration = configuration;
            _useOtherService = useOtherService;
            _amazonS3Service = amazonS3Service;
        }

        public async Task<OdiResponse<List<OdiTalepOutputDTO>>> YeniOdiTalep(List<OdiTalepCreateDTO> odiTalepList, OdiUser user)
        {
            List<OdiTalep> talepList = _mapper.Map<List<OdiTalep>>(odiTalepList);
            foreach (var talep in talepList)
            {
                if (await _odiTalepDataService.CheckOdiTalep(talep.TalepGonderilenPerformerId, talep.ProjeRolId))
                    return OdiResponse<List<OdiTalepOutputDTO>>.Fail("Listede daha önce bu rol için odi talebi yapılmış bir performer var", "Bad Request", 400);

                talep.EklenmeTarihi = DateTime.Now;
                talep.Ekleyen = user.AdSoyad;
                talep.EkleyenId = user.Id;

                talep.OdiTalepTarihi = DateTime.Now;
            }
            await _odiTalepDataService.YeniOdiTalep(talepList);


            List<OdiTalepOutputDTO> list = await _odiTalepDataService.OdiTalepListesiGetirByGonderen(talepList.FirstOrDefault().TalepGonderenId, talepList.Count);//sadece son ekleneleri getiriyoruz
            return OdiResponse<List<OdiTalepOutputDTO>>.Success("Odi talepleri gönderildi", OdiTalepListesiSetPresignedUrl(list), 200);
        }

        public async Task<OdiResponse<List<OdiTalepOutputDTO>>> YapimOdiTalepListesi(KullaniciIdDTO kid)
        {
            List<OdiTalepOutputDTO> list = await _odiTalepDataService.OdiTalepListesiGetirByGonderen(kid.KullaniciId);
            return OdiResponse<List<OdiTalepOutputDTO>>.Success("Odi Talep Listesi Getirildi", OdiTalepListesiSetPresignedUrl(list), 200);
        }
        public async Task<OdiResponse<List<OdiTalepOutputDTO>>> MenajerOdiTalepListesi(KullaniciIdDTO kid)
        {
            List<OdiTalepOutputDTO> list = await _odiTalepDataService.OdiTalepListesiGetirByMenajer(kid.KullaniciId);
            return OdiResponse<List<OdiTalepOutputDTO>>.Success("Odi Talep Listesi Getirildi", OdiTalepListesiSetPresignedUrl(list), 200);
        }


        public async Task<OdiResponse<OdiTalepOutputDTO>> OdiTalepMenajerGoruldu(OdiTalepIdDTO talepId, OdiUser user)
        {
            OdiTalep talep = await _odiTalepDataService.OdiTalepGetirById(talepId.ToString());

            talep.MenajerGordu = true;
            talep.MenajerGormeTarihi = DateTime.Now;

            talep.Guncelleyen = user.AdSoyad;
            talep.GuncelleyenId = user.Id;
            talep.GuncellenmeTarihi = DateTime.Now;

            await _odiTalepDataService.OdiTalepGuncelle(talep);
            OdiTalepOutputDTO odiTalep = await _odiTalepDataService.OdiTalepGetir(talep.Id);
            return OdiResponse<OdiTalepOutputDTO>.Success("Menajer görüldü işlemi başarıyla yapıldı", OdiTalepSetPresignedUrl(odiTalep), 200);
        }
        public async Task<OdiResponse<OdiTalepOutputDTO>> OdiTalepMenajerRed(OdiTalepRedInputDTO red, OdiUser user)
        {
            OdiTalep talep = await _odiTalepDataService.OdiTalepGetirById(red.OdiTalepId);

            talep.MenajerTalepRed = true;
            talep.MenajerTalepRedSebebi = red.RedSebebi;
            talep.MenajerTalepRedTarihi = DateTime.Now;

            talep.TalepKapadi = true;

            talep.Guncelleyen = user.AdSoyad;
            talep.GuncelleyenId = user.Id;
            talep.GuncellenmeTarihi = DateTime.Now;

            await _odiTalepDataService.OdiTalepGuncelle(talep);

            OdiTalepOutputDTO odiTalep = await _odiTalepDataService.OdiTalepGetir(talep.Id);
            return OdiResponse<OdiTalepOutputDTO>.Success("Menajer talebi reddetti", OdiTalepSetPresignedUrl(odiTalep), 200);
        }
        public async Task<OdiResponse<OdiTalepOutputDTO>> OdiTalepPerformeraIlet(OdiTalepPerformeraIletInput ilet, OdiUser user)
        {
            OdiTalep talep = await _odiTalepDataService.OdiTalepGetirById(ilet.OdiTalepId);

            if (talep == null) return OdiResponse<OdiTalepOutputDTO>.Fail("Bu id ile bir odi talebi bulunamadı", "Not Found", 404);
            talep.PerformeraIletildi = true;
            talep.PerformeraIletildiTarihi = DateTime.Now;
            talep.MenajerOnerisi = ilet.MenajerOnerisi;

            talep.Guncelleyen = user.AdSoyad;
            talep.GuncelleyenId = user.Id;
            talep.GuncellenmeTarihi = DateTime.Now;

            await _odiTalepDataService.OdiTalepGuncelle(talep);

            OdiTalepOutputDTO odiTalep = await _odiTalepDataService.OdiTalepGetir(ilet.OdiTalepId);
            return OdiResponse<OdiTalepOutputDTO>.Success("Odi talebi performera iletildi", OdiTalepSetPresignedUrl(odiTalep), 200);
        }

        public async Task<OdiResponse<OdiTalepOutputDTO>> OdiTalepPerformerGoruldu(OdiTalepIdDTO talepId, OdiUser user)
        {
            OdiTalep talep = await _odiTalepDataService.OdiTalepGetirById(talepId.ToString());

            talep.PerformerGordu = true;
            talep.PerformerGorduTarihi = DateTime.Now;

            talep.Guncelleyen = user.AdSoyad;
            talep.GuncelleyenId = user.Id;
            talep.GuncellenmeTarihi = DateTime.Now;

            await _odiTalepDataService.OdiTalepGuncelle(talep);

            OdiTalepOutputDTO odiTalep = await _odiTalepDataService.OdiTalepGetir(talepId.ToString());
            return OdiResponse<OdiTalepOutputDTO>.Success("Odi talebi performer tarafından  görüldü", OdiTalepSetPresignedUrl(odiTalep), 200);
        }


        public async Task<OdiResponse<OdiTalepOutputDTO>> OdiTalepPerformerRed(OdiTalepRedInputDTO red, OdiUser user)
        {
            OdiTalep talep = await _odiTalepDataService.OdiTalepGetirById(red.OdiTalepId);

            talep.PerformerTalepRed = true;
            talep.PerformerTalepRedSebebi = red.RedSebebi;
            talep.PerformerTalepRedTarihi = DateTime.Now;

            talep.Guncelleyen = user.AdSoyad;
            talep.GuncelleyenId = user.Id;
            talep.GuncellenmeTarihi = DateTime.Now;

            await _odiTalepDataService.OdiTalepGuncelle(talep);

            OdiTalepOutputDTO odiTalep = await _odiTalepDataService.OdiTalepGetir(talep.Id);
            return OdiResponse<OdiTalepOutputDTO>.Success("Performer talebi reddetti", OdiTalepSetPresignedUrl(odiTalep), 200);
        }

        public async Task<OdiResponse<OdiTalepOutputDTO>> OdiTalepMenajerPerformerRedOnayi(OdiTalepIdDTO talepId, OdiUser user)
        {
            OdiTalep talep = await _odiTalepDataService.OdiTalepGetirById(talepId.ToString());

            talep.MenajerRedOnayi = true;
            talep.MenajerRedOnayiTarihi = DateTime.Now;

            talep.TalepKapadi = true;

            talep.Guncelleyen = user.AdSoyad;
            talep.GuncelleyenId = user.Id;
            talep.GuncellenmeTarihi = DateTime.Now;

            await _odiTalepDataService.OdiTalepGuncelle(talep);

            OdiTalepOutputDTO odiTalep = await _odiTalepDataService.OdiTalepGetir(talep.Id);
            return OdiResponse<OdiTalepOutputDTO>.Success("Performerın red talebini menajer onayladı", OdiTalepSetPresignedUrl(odiTalep), 200);
        }
        public async Task<OdiResponse<OdiTalepOutputDTO>> OdiTalepMenajerRedIptal(OdiTalepIdDTO talepId, OdiUser user)
        {
            OdiTalep talep = await _odiTalepDataService.OdiTalepGetirById(talepId.ToString());

            talep.MenajerTalepRed = false;
            talep.MenajerTalepRedSebebi = string.Empty;
            talep.MenajerTalepRedTarihi = null;

            talep.Guncelleyen = user.AdSoyad;
            talep.GuncelleyenId = user.Id;
            talep.GuncellenmeTarihi = DateTime.Now;

            await _odiTalepDataService.OdiTalepGuncelle(talep);

            OdiTalepOutputDTO odiTalep = await _odiTalepDataService.OdiTalepGetir(talep.Id);
            return OdiResponse<OdiTalepOutputDTO>.Success("Menajer talep reddini geri çekti", OdiTalepSetPresignedUrl(odiTalep), 200);
        }

        public async Task<OdiResponse<OdiTalepOutputDTO>> OdiTalepMenajerPerformerRedIptal(OdiTalepIdDTO talepId, OdiUser user)
        {
            OdiTalep talep = await _odiTalepDataService.OdiTalepGetirById(talepId.ToString());

            talep.PerformerTalepRed = false;
            talep.PerformerTalepRedSebebi = string.Empty;
            talep.PerformerTalepRedTarihi = null;

            talep.Guncelleyen = user.AdSoyad;
            talep.GuncelleyenId = user.Id;
            talep.GuncellenmeTarihi = DateTime.Now;

            await _odiTalepDataService.OdiTalepGuncelle(talep);

            OdiTalepOutputDTO odiTalep = await _odiTalepDataService.OdiTalepGetir(talep.Id);
            return OdiResponse<OdiTalepOutputDTO>.Success("Menajer performerın red talebini iptal etti", OdiTalepSetPresignedUrl(odiTalep), 200);
        }

        public async Task<OdiResponse<List<IzlemeListesiItem>>> MenajerIzlemeListesi(MenajerIdDTO menajerId)
        {
            List<(OdiTalepOutputDTO, PerformerOdi)> list = await _odiTalepDataService.MenajerIzlemeListesi(menajerId.ToString());
            if (list == null) OdiResponse<List<IzlemeListesiItem>>.Success("Menajer Odi İzleme Listesi Getirildi", null, 200);
            List<IzlemeListesiItem> izlemeListesi = new List<IzlemeListesiItem>();
            foreach (var item in list)
            {
                IzlemeListesiItem izlemeItem = new IzlemeListesiItem();

                OdiTalepOutputDTO talep = item.Item1;
                PerformerOdiOutputDTO odiOutput = new PerformerOdiOutputDTO();


                if (item.Item2 != null)
                {
                    odiOutput = _mapper.Map<PerformerOdiOutputDTO>(item.Item2);
                }

                if (odiOutput.PerformerOdiFotograflar != null)
                {
                    foreach (var foto in odiOutput.PerformerOdiFotograflar)
                    {
                        if (!string.IsNullOrEmpty(foto.Fotograf)) foto.FotografPresignedURL = _amazonS3Service.GetPreSignedUrl(foto.Fotograf);
                    }
                }
                if (odiOutput.PerformerOdiVideo != null && !string.IsNullOrEmpty(odiOutput.PerformerOdiVideo.Video)) odiOutput.PerformerOdiVideo.VideoPresignedUrl = _amazonS3Service.GetPreSignedUrl(odiOutput.PerformerOdiVideo.Video);
                if (odiOutput.PerformerOdiSes != null && !string.IsNullOrEmpty(odiOutput.PerformerOdiSes.Video)) odiOutput.PerformerOdiSes.VideoPresignedUrl = _amazonS3Service.GetPreSignedUrl(odiOutput.PerformerOdiSes.Video);

                izlemeItem.OdiTalep = OdiTalepSetPresignedUrl(talep);
                izlemeItem.PerformerOdi = odiOutput;
                izlemeListesi.Add(izlemeItem);
            }

            return OdiResponse<List<IzlemeListesiItem>>.Success("Menajer Odi İzleme Listesi Getirildi", izlemeListesi, 200);
        }

        public async Task<OdiResponse<List<IzlemeListesiItem>>> PerformerOdiIzlemeListesi(PerformerIdDTO model)
        {
            List<(OdiTalepOutputDTO, PerformerOdi)> list = await _odiTalepDataService.PerformerIzlemeListesi(model.PerformerId);
            if (list == null) OdiResponse<List<IzlemeListesiItem>>.Success("Performer Odi İzleme Listesi Getirildi", null, 200);
            List<IzlemeListesiItem> izlemeListesi = new List<IzlemeListesiItem>();

            foreach (var item in list)
            {
                IzlemeListesiItem izlemeItem = new IzlemeListesiItem();

                OdiTalepOutputDTO talep = item.Item1;
                PerformerOdiOutputDTO odiOutput = new PerformerOdiOutputDTO();


                if (item.Item2 != null)
                {
                    odiOutput = _mapper.Map<PerformerOdiOutputDTO>(item.Item2);
                }

                if (odiOutput.PerformerOdiFotograflar != null)
                {
                    foreach (var foto in odiOutput.PerformerOdiFotograflar)
                    {
                        if (!string.IsNullOrEmpty(foto.Fotograf)) foto.FotografPresignedURL = _amazonS3Service.GetPreSignedUrl(foto.Fotograf);
                    }
                }
                if (odiOutput.PerformerOdiVideo != null && !string.IsNullOrEmpty(odiOutput.PerformerOdiVideo.Video)) odiOutput.PerformerOdiVideo.VideoPresignedUrl = _amazonS3Service.GetPreSignedUrl(odiOutput.PerformerOdiVideo.Video);
                if (odiOutput.PerformerOdiSes != null && !string.IsNullOrEmpty(odiOutput.PerformerOdiSes.Video)) odiOutput.PerformerOdiSes.VideoPresignedUrl = _amazonS3Service.GetPreSignedUrl(odiOutput.PerformerOdiSes.Video);

                izlemeItem.OdiTalep = OdiTalepSetPresignedUrl(talep);
                izlemeItem.PerformerOdi = odiOutput;
                izlemeListesi.Add(izlemeItem);
            }

            return OdiResponse<List<IzlemeListesiItem>>.Success("Performer Odi İzleme Listesi Getirildi", izlemeListesi, 200);
        }

        public async Task<OdiResponse<List<IzlemeListesiItem>>> YapımIzlemeListesi(ProjeIdDTO projeId, string jwtToken)
        {

            string url = _configuration.GetSection("ProjeServerUrl").Value + "/proje-yetkili-listesi";
            var dynamicData = await _useOtherService.PostMethod(projeId, url, jwtToken);
            string jsonString = JsonConvert.SerializeObject(dynamicData);
            List<ProjeYetkiliBilgisiDTO> yetkiliListesi = JsonConvert.DeserializeObject<List<ProjeYetkiliBilgisiDTO>>(jsonString);

            List<(OdiTalepOutputDTO, PerformerOdi)> list = await _odiTalepDataService.YapimIzlemeListesi(yetkiliListesi.Select(x => x.YetkiliId).Distinct().ToList());
            if (list == null) OdiResponse<List<IzlemeListesiItem>>.Success("Yapım Odi İzleme Listesi Getirildi", null, 200);

            List<IzlemeListesiItem> izlemeListesi = new List<IzlemeListesiItem>();


            foreach (var item in list)
            {
                IzlemeListesiItem izlemeItem = new IzlemeListesiItem();

                OdiTalepOutputDTO talep = item.Item1;

                PerformerOdiOutputDTO odiOutput = _mapper.Map<PerformerOdiOutputDTO>(item.Item2);

                if (odiOutput.PerformerOdiFotograflar != null)
                {
                    foreach (var foto in odiOutput.PerformerOdiFotograflar)
                    {
                        if (!string.IsNullOrEmpty(foto.Fotograf)) foto.FotografPresignedURL = _amazonS3Service.GetPreSignedUrl(foto.Fotograf);
                    }
                }
                if (!string.IsNullOrEmpty(odiOutput.PerformerOdiVideo.Video)) odiOutput.PerformerOdiVideo.VideoPresignedUrl = _amazonS3Service.GetPreSignedUrl(odiOutput.PerformerOdiVideo.Video);
                if (!string.IsNullOrEmpty(odiOutput.PerformerOdiSes.Video)) odiOutput.PerformerOdiSes.VideoPresignedUrl = _amazonS3Service.GetPreSignedUrl(odiOutput.PerformerOdiSes.Video);

                izlemeItem.OdiTalep = OdiTalepSetPresignedUrl(talep);
                izlemeItem.PerformerOdi = odiOutput;
                izlemeListesi.Add(izlemeItem);
            }

            return OdiResponse<List<IzlemeListesiItem>>.Success("Yapım Odi İzleme Listesi Getirildi", izlemeListesi, 200);
        }

        #region yardımcı methodlar
        private OdiTalepOutputDTO OdiTalepSetPresignedUrl(OdiTalepOutputDTO talep)
        {
            if (!string.IsNullOrEmpty(talep.TalepGonderenProfilResmi)) talep.TalepGonderenProfilResmi = _amazonS3Service.GetPreSignedUrl(talep.TalepGonderenProfilResmi);
            if (!string.IsNullOrEmpty(talep.TalepGonderilenMenajerProfilResmi)) talep.TalepGonderilenMenajerProfilResmi = _amazonS3Service.GetPreSignedUrl(talep.TalepGonderilenMenajerProfilResmi);
            if (!string.IsNullOrEmpty(talep.TalepGonderilenPerformerProfilResmi)) talep.TalepGonderilenPerformerProfilResmi = _amazonS3Service.GetPreSignedUrl(talep.TalepGonderilenPerformerProfilResmi);
            return talep;
        }

        private List<OdiTalepOutputDTO> OdiTalepListesiSetPresignedUrl(List<OdiTalepOutputDTO> list)
        {
            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.TalepGonderenProfilResmi)) item.TalepGonderenProfilResmi = _amazonS3Service.GetPreSignedUrl(item.TalepGonderenProfilResmi);
                if (!string.IsNullOrEmpty(item.TalepGonderilenMenajerProfilResmi)) item.TalepGonderilenMenajerProfilResmi = _amazonS3Service.GetPreSignedUrl(item.TalepGonderilenMenajerProfilResmi);
                if (!string.IsNullOrEmpty(item.TalepGonderilenPerformerProfilResmi)) item.TalepGonderilenPerformerProfilResmi = _amazonS3Service.GetPreSignedUrl(item.TalepGonderilenPerformerProfilResmi);
            }
            return list;
        }

        #endregion
    }
}
