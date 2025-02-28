using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OdiApp.BusinessLayer.Core;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.DataAccessLayer.IslemlerDataServices.OpsiyonIslemler;
using OdiApp.DTOs.Enums;
using OdiApp.DTOs.IslemlerDTOs;
using OdiApp.DTOs.IslemlerDTOs.OpsiyonIslemler;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeBilgileriDTOs;
using OdiApp.EntityLayer.IslemlerModels.OpsiyonIslemler;

namespace OdiApp.BusinessLayer.Services.IslemlerLogicServices.OpsiyonIslemler
{
    public class OpsiyonLogicService : IOpsiyonLogicService
    {
        private readonly IMapper _mapper;
        private readonly IAmazonS3Service _amazonS3Service;
        private readonly IOpsiyonDataService _opsiyonDataService;
        private readonly IUseOtherService _useOtherService;
        private readonly IConfiguration _configuration;

        public OpsiyonLogicService(IMapper mapper, IAmazonS3Service amazonS3Service, IOpsiyonDataService opsiyonDataService, IUseOtherService useOtherService, IConfiguration configuration)
        {
            _mapper = mapper;
            _amazonS3Service = amazonS3Service;
            _opsiyonDataService = opsiyonDataService;
            _useOtherService = useOtherService;
            _configuration = configuration;
        }
        #region OpsiyonListesi
        public async Task<OdiResponse<List<OpsiyonListesiOutputDTO>>> OpsiyonListesineEkle(List<OpsiyonListesiCreateDTO> opsList, OdiUser user)
        {
            List<OpsiyonListesi> list = new List<OpsiyonListesi>();
            foreach (var item in opsList)
            {
                bool check = await _opsiyonDataService.CheckOpsiyonListesi(item.PerformerId, item.ProjeRolId);
                if (!check)
                {
                    OpsiyonListesi listItem = _mapper.Map<OpsiyonListesi>(item);

                    listItem.ListeyeEklenemeTarihi = DateTime.Now;

                    listItem.EklenmeTarihi = DateTime.Now;
                    listItem.EkleyenId = user.Id;
                    listItem.Ekleyen = user.AdSoyad;

                    listItem.GuncellenmeTarihi = DateTime.Now;
                    listItem.GuncelleyenId = user.Id;
                    listItem.Guncelleyen = user.AdSoyad;

                    list.Add(listItem);
                }
            }
            list = await _opsiyonDataService.OpsiyonListesineEkle(list);
            List<OpsiyonListesiOutputDTO> outputList = _mapper.Map<List<OpsiyonListesiOutputDTO>>(list);
            return OdiResponse<List<OpsiyonListesiOutputDTO>>.Success("Oyuncular opsiyon listesine eklendi", outputList, 200);
        }

        public async Task<OdiResponse<List<OpsiyonListesiOutputDTO>>> OpsiyonListesiGetir(ProjeIdDTO projeId, string jwtToken)
        {
            string url = _configuration.GetSection("ProjeServerUrl").Value + "/proje-yetkili-listesi";
            ProjeIdDTO projeIdDTO = new ProjeIdDTO { ProjeId = projeId.ToString() };


            var dynamicData = await _useOtherService.PostMethod(projeIdDTO, url, jwtToken);
            string jsonString = JsonConvert.SerializeObject(dynamicData);
            List<YetkiliIdDTO> yetkiliIdList = JsonConvert.DeserializeObject<List<YetkiliIdDTO>>(jsonString);

            List<string> yetkililer = yetkiliIdList.Select(x => x.YetkiliId).Distinct().ToList();

            List<OpsiyonListesiViewDTO> opsList = await _opsiyonDataService.OpsiyonListesiGetir(yetkililer, projeId.ToString());
            foreach (var item in opsList)
            {
                item.PerformerProfilResmi = string.IsNullOrEmpty(item.PerformerProfilResmi) ? "" : _amazonS3Service.GetPreSignedUrl(item.PerformerProfilResmi);
                if (item.Opsiyon != null)
                {
                    item.Opsiyon.PerformerProfilFotografi = string.IsNullOrEmpty(item.Opsiyon.PerformerProfilFotografi) ? "" : _amazonS3Service.GetPreSignedUrl(item.Opsiyon.PerformerProfilFotografi);
                }

            }

            return OdiResponse<List<OpsiyonListesiOutputDTO>>.Success("Opsiyon Listesi Getirildi", _mapper.Map<List<OpsiyonListesiOutputDTO>>(opsList), 200);
        }

        public async Task<OdiResponse<List<OpsiyonListesiOutputDTO>>> MenajerOpsiyonListesiGetir(MenajerOpsiyonListesiInputDTO input)
        {
            List<OpsiyonListesiViewDTO> opsList = await _opsiyonDataService.MenajerOpsiyonListesiGetir(input.MenajerId, input.ProjeId);
            foreach (var item in opsList)
            {
                item.PerformerProfilResmi = string.IsNullOrEmpty(item.PerformerProfilResmi) ? "" : _amazonS3Service.GetPreSignedUrl(item.PerformerProfilResmi);
                if (item.Opsiyon != null)
                {
                    item.Opsiyon.PerformerProfilFotografi = string.IsNullOrEmpty(item.Opsiyon.PerformerProfilFotografi) ? "" : _amazonS3Service.GetPreSignedUrl(item.Opsiyon.PerformerProfilFotografi);
                }

            }

            return OdiResponse<List<OpsiyonListesiOutputDTO>>.Success("Opsiyon Listesi Getirildi", _mapper.Map<List<OpsiyonListesiOutputDTO>>(opsList), 200);
        }

        public async Task<OdiResponse<bool>> OpsiyonListesindenCikar(OpsiyonListesiIdDTO opsiyonListesiId)
        {
            OpsiyonListesi ops = await _opsiyonDataService.OpsiyonListesiGetir(opsiyonListesiId.ToString());
            if (ops.Opsiyon == null)
            {
                await _opsiyonDataService.OpsiyonListesindenCikar(ops);
                return OdiResponse<bool>.Success("Oynucu opsiyon listesinden çıkartıldı.", true, 200);
            }
            else
            {
                return OdiResponse<bool>.Fail("Bu oyuncuya opsiyon gönderildiği için listeden çıkartamazsınız", "Bad Request", 404);
            }
        }

        #endregion
        #region Opsiyon
        public async Task<OdiResponse<List<OpsiyonOutputDTO>>> YeniOpsiyon(List<OpsiyonCreateDTO> opsiyonCreateDTOList, OdiUser user)
        {
            List<Opsiyon> listOpsiyon = new List<Opsiyon>();
            foreach (var opsiyonCreateDTO in opsiyonCreateDTOList)
            {
                bool checkOpsiyon = await _opsiyonDataService.CheckOpsiyon(opsiyonCreateDTO.ProjeId, opsiyonCreateDTO.MenajerId, opsiyonCreateDTO.PerformerId);

                if (!checkOpsiyon)
                {
                    Opsiyon opsiyon = _mapper.Map<Opsiyon>(opsiyonCreateDTO);

                    opsiyon.OpsiyonGonderimTarihi = DateTime.Now;

                    opsiyon.EklenmeTarihi = DateTime.Now;
                    opsiyon.Ekleyen = user.AdSoyad;
                    opsiyon.EkleyenId = user.Id;

                    opsiyon.GuncellenmeTarihi = DateTime.Now;
                    opsiyon.Guncelleyen = user.AdSoyad;
                    opsiyon.GuncelleyenId = user.Id;

                    if (opsiyon.AnketSorulari != null)
                    {
                        foreach (var item in opsiyon.AnketSorulari)
                        {
                            item.EklenmeTarihi = DateTime.Now;
                            item.Ekleyen = user.AdSoyad;
                            item.EkleyenId = user.Id;

                            item.GuncellenmeTarihi = DateTime.Now;
                            item.Guncelleyen = user.AdSoyad;
                            item.GuncelleyenId = user.Id;
                        }
                    }
                    listOpsiyon.Add(opsiyon);
                }
            }

            List<OpsiyonViewDTO> opsViewList = await _opsiyonDataService.YeniOpsiyon(listOpsiyon);



            return OdiResponse<List<OpsiyonOutputDTO>>.Success("Yeni opsiyon oluşturuldu.", OpsiyonOutputHazirla(opsViewList), 200);
        }

        public async Task<OdiResponse<OpsiyonOutputDTO>> PerformeraYonlendir(OpsiyonIdDTO id)
        {
            Opsiyon opsiyon = await _opsiyonDataService.OpsiyonGetir(id.OpsiyonId);
            if (opsiyon == null) return OdiResponse<OpsiyonOutputDTO>.Fail("Bu id ile bir kayıt bulunamadı", "Not Found", 404);

            opsiyon.PerformeraIletildi = true;

            opsiyon = await _opsiyonDataService.OpsiyonGuncelle(opsiyon);
            opsiyon.PerformeraIletimTarihi = DateTime.Now;
            OpsiyonViewDTO ops = await _opsiyonDataService.OpsiyonViewGetirById(opsiyon.Id);

            return OdiResponse<OpsiyonOutputDTO>.Success("Opsiyon performer'a yönlendirildi.", OpsiyonOutputHazirla(ops), 200);
        }

        public async Task<OdiResponse<OpsiyonOutputDTO>> GeriCevir(OpsiyonGeriCevirDTO dto)
        {
            Opsiyon opsiyon = await _opsiyonDataService.OpsiyonGetir(dto.OpsiyonId);
            if (opsiyon == null) return OdiResponse<OpsiyonOutputDTO>.Fail("Bu id ile bir kayıt bulunamadı", "Not Found", 404);

            opsiyon.GeriCevrildi = true;
            opsiyon.YanitlayanPerformer = dto.YanitlayanPerformer;
            opsiyon.YanitlayanMenajer = dto.YanitlayanMenajer;
            opsiyon.Tamamlandi = true;
            opsiyon.TamamlanmaTarihi = DateTime.Now;


            opsiyon = await _opsiyonDataService.OpsiyonGuncelle(opsiyon);
            opsiyon = await _opsiyonDataService.OpsiyonGetir(opsiyon.Id);

            return OdiResponse<OpsiyonOutputDTO>.Success("Geri çevirme işlemi başarılı.", _mapper.Map<OpsiyonOutputDTO>(opsiyon), 200);
        }

        public async Task<OdiResponse<OpsiyonOutputDTO>> Yanitla(OpsiyonYanitlaDTO dto, OdiUser user, string jwtToken)
        {
            Opsiyon opsiyon = await _opsiyonDataService.OpsiyonGetir(dto.YanitlaDetay.OpsiyonId);
            if (opsiyon == null) return OdiResponse<OpsiyonOutputDTO>.Fail("Bu id ile bir kayıt bulunamadı", "Not Found", 404);

            opsiyon = _mapper.Map(dto.YanitlaDetay, opsiyon);
            if (dto.YanitlaDetay.MusaitOlmadigimGunler.Count > 0)
                opsiyon.MusaitOlmadigimGunler = Fonksiyonlar.convertDateTimeListToString(dto.YanitlaDetay.MusaitOlmadigimGunler);
            //if (dto?.MusaitOlmadigimGunler?.Any() == true)
            //    opsiyon.MusaitOlmadigimGunler = string.Join(',', dto.MusaitOlmadigimGunler.Select(date => date.ToString()));

            opsiyon.GuncellenmeTarihi = DateTime.Now;
            opsiyon.GuncelleyenId = user.Id;
            opsiyon.Guncelleyen = user.AdSoyad;

            opsiyon.Onaylandi = false;
            opsiyon.Tamamlandi = true;
            opsiyon.TamamlanmaTarihi = DateTime.Now;
            opsiyon = await _opsiyonDataService.OpsiyonGuncelle(opsiyon);

            if (dto.AnketSorulari.Count > 0)
            {
                foreach (var soruDTO in dto.AnketSorulari)
                {
                    OpsiyonAnketSorulari soru = await _opsiyonDataService.OpsiyonAnketSorusuGetir(soruDTO.OpsiyonAnketSorulariId);
                    soru.Cevap = soruDTO.Cevap;
                    await _opsiyonDataService.OpsiyonAnketSorusuGuncelle(soru);
                }
            }

            Opsiyon ops = await _opsiyonDataService.OpsiyonGetir(opsiyon.Id);
            OpsiyonViewDTO opsView = await _opsiyonDataService.OpsiyonViewGetir(ops.OpsiyonListesiId);
            OpsiyonOutputDTO outputDTO = OpsiyonOutputHazirla(opsView);
            //OpsiyonViewDTO ops = await _opsiyonDataService.OpsiyonViewGetirById(opsiyon.Id);

            PerformerTakvimCreateDTO performerTakvimCreateDTO = new PerformerTakvimCreateDTO();

            performerTakvimCreateDTO.PerformerId = opsiyon.PerformerId;
            performerTakvimCreateDTO.BaslangicTarihi = opsiyon.CekimBaslagicTarihi;
            performerTakvimCreateDTO.BitisTarihi = opsiyon.CekimBitisTarihi;
            performerTakvimCreateDTO.DolulukAciklamasi = opsiyon.ProjeAdi;
            performerTakvimCreateDTO.DolulukTuru = TakvimDolulukTuru.OPSIYON;

            var dynamicData = await _useOtherService.PostMethod(performerTakvimCreateDTO, _configuration.GetSection("PerformerServerUrl").Value + "/api/yeni-tarih-araligi", jwtToken);

            return OdiResponse<OpsiyonOutputDTO>.Success("Yanıtla işlemi başarılı.", outputDTO, 200);
        }

        #endregion
        public async Task<OdiResponse<bool>> MenajerInceledi(OpsiyonIdDTO opsId)
        {
            Opsiyon ops = await _opsiyonDataService.OpsiyonGetir(opsId.ToString());

            ops.MenajerInceledi = true;

            await _opsiyonDataService.OpsiyonGuncelle(ops);
            return OdiResponse<bool>.Success("Menajer Opsiyonu inceledi", true, 200);
        }

        public async Task<OdiResponse<bool>> MenajerOnayi(OpsiyonIdDTO opsId)
        {
            Opsiyon opsiyon = await _opsiyonDataService.OpsiyonGetir(opsId.ToString());
            if (opsiyon == null) return OdiResponse<bool>.Fail("Bu id ile bir kayıt bulunamadı", "Not Found", 404);

            opsiyon.Onaylandi = true;
            await _opsiyonDataService.OpsiyonGuncelle(opsiyon);

            return OdiResponse<bool>.Success("Menajer Opsiyonu onayladı", true, 200);
        }

        public async Task<OdiResponse<bool>> OpsiyonuPerformeraIlet(OpsiyonIdDTO opsId)
        {
            Opsiyon ops = await _opsiyonDataService.OpsiyonGetir(opsId.ToString());

            ops.PerformeraIletildi = true;
            ops.PerformeraIletimTarihi = DateTime.Now;
            await _opsiyonDataService.OpsiyonGuncelle(ops);
            return OdiResponse<bool>.Success("Opsiyon performera iletildi.", true, 200);
        }

        public async Task<OdiResponse<bool>> PerformerInceledi(OpsiyonIdDTO opsId)
        {
            Opsiyon ops = await _opsiyonDataService.OpsiyonGetir(opsId.ToString());

            ops.PerformerInceledi = true;
            await _opsiyonDataService.OpsiyonGuncelle(ops);
            return OdiResponse<bool>.Success("Performer Opsiyonu inceledi", true, 200);
        }

        #region 
        public OpsiyonOutputDTO OpsiyonOutputHazirla(OpsiyonViewDTO opsiyon)
        {
            OpsiyonOutputDTO outputDTO = new OpsiyonOutputDTO();
            outputDTO = _mapper.Map(opsiyon, outputDTO);

            if (!string.IsNullOrEmpty(opsiyon.PerformerProfilFotografi)) outputDTO.PerformerProfilFotografi = _amazonS3Service.GetPreSignedUrl(opsiyon.PerformerProfilFotografi);
            return outputDTO;
        }
        public List<OpsiyonOutputDTO> OpsiyonOutputHazirla(List<OpsiyonViewDTO> opsiyonList)
        {
            List<OpsiyonOutputDTO> list = new List<OpsiyonOutputDTO>();
            foreach (var ops in opsiyonList)
            {
                list.Add(OpsiyonOutputHazirla(ops));
            }
            return list;
        }

        public async Task<OdiResponse<OpsiyonOutputDTO>> OpsiyonGetir(OpsiyonIdDTO opsId)
        {
            Opsiyon ops = await _opsiyonDataService.OpsiyonGetir(opsId.ToString());
            if (ops == null) return OdiResponse<OpsiyonOutputDTO>.Fail("Bu id ile opsiyon bulunmamaktadır", "Not Found", 404);
            OpsiyonViewDTO opsView = await _opsiyonDataService.OpsiyonViewGetir(ops.OpsiyonListesiId);
            OpsiyonOutputDTO outputDTO = OpsiyonOutputHazirla(opsView);
            return OdiResponse<OpsiyonOutputDTO>.Success("Opsiyon Getirildi", outputDTO, 200);
        }

        #endregion
    }
}