using AutoMapper;
using OdiApp.BusinessLayer.Services.IslemlerLogicServices.OdiIslemler;
using OdiApp.DataAccessLayer.IslemlerDataServices.OdiListeler;
using OdiApp.DTOs.IslemlerDTOs.OdiListeler;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.IslemlerModels.OdiIslemler;
using OdiApp.EntityLayer.IslemlerModels.OdiListeler;

namespace OdiApp.BusinessLayer.Services.IslemlerLogicServices.OdiListeler
{
    public class OdiListeLogicService : IOdiListeLogicService
    {
        private readonly IMapper _mapper;
        IOdiListeDataService _odiListeDataServis;

        IPerformerOdiLogicService _performerOdiLogicService;
        public OdiListeLogicService(IMapper mapper, IOdiListeDataService odiListeDataService, IPerformerOdiLogicService performerOdiLogicService)
        {
            _mapper = mapper;
            _odiListeDataServis = odiListeDataService;

            _performerOdiLogicService = performerOdiLogicService;
        }
        public async Task<OdiResponse<OdiListeIdDTO>> YeniOdiListe(OdiListeCreateDTO odiListe, OdiUser user)
        {
            OdiListe liste = _mapper.Map<OdiListe>(odiListe);

            liste.OlusturulmaTarihi = DateTime.Now;

            liste.EklenmeTarihi = DateTime.Now;
            liste.Ekleyen = user.AdSoyad;
            liste.EkleyenId = user.Id;

            liste.GuncellenmeTarihi = DateTime.Now;
            liste.Guncelleyen = user.AdSoyad;
            liste.GuncelleyenId = user.Id;

            foreach (var item in liste.Odiler)
            {
                item.ListeyeEklenmeTarihi = DateTime.Now;

                item.EklenmeTarihi = DateTime.Now;
                item.Ekleyen = user.AdSoyad;
                item.EkleyenId = user.Id;

                item.GuncellenmeTarihi = DateTime.Now;
                item.Guncelleyen = user.AdSoyad;
                item.GuncelleyenId = user.Id;
            }
            await _odiListeDataServis.YeniOdiListe(liste);

            return OdiResponse<OdiListeIdDTO>.Success("Odi Liste Oluşturuldu", new OdiListeIdDTO { OdiListeId = liste.Id }, 200);
        }

        public async Task<OdiResponse<bool>> YeniOdiListeDetay(List<OdiListeDetayCreateDTO> detayList, OdiUser user)
        {
            List<OdiListeDetay> list = _mapper.Map<List<OdiListeDetay>>(detayList);
            List<OdiListeDetay> eklenecekOdiler = new List<OdiListeDetay>();
            foreach (var item in list)
            {
                bool check = await _odiListeDataServis.CheckOdiListeDetay(item.OdiListeId, item.OdiTalepId);
                //if(!check) return OdiResponse<bool>.Fail("Eklemek istediğiniz odilerden birisi daha önce eklenmiş")
                if (!check)
                {
                    item.ListeyeEklenmeTarihi = DateTime.Now;

                    item.EklenmeTarihi = DateTime.Now;
                    item.Ekleyen = user.AdSoyad;
                    item.EkleyenId = user.Id;

                    item.GuncellenmeTarihi = DateTime.Now;
                    item.Guncelleyen = user.AdSoyad;
                    item.GuncelleyenId = user.Id;
                    eklenecekOdiler.Add(item);
                }
            }

            await _odiListeDataServis.YeniOdiListeDetay(eklenecekOdiler);
            return OdiResponse<bool>.Success("Odiler listeye eklendi", true, 200);
        }

        public async Task<OdiResponse<List<OdiListeAdlariOutputDTO>>> OdiListeleriGetir(KullaniciIdDTO kullaniciId)
        {
            List<OdiListeAdlariOutputDTO> list = await _odiListeDataServis.OdiListeListesi(kullaniciId.ToString());
            return OdiResponse<List<OdiListeAdlariOutputDTO>>.Success("Odi favori listeleri getirildi", list, 200);
        }

        public async Task<OdiResponse<OdiListeOutputDTO>> OdiListeDetayGetir(OdiListeIdDTO listeId)
        {
            OdiListeOutputDTO odiListe = await _odiListeDataServis.OdiListeGetirById(listeId.ToString());
            List<OdiListeDetay> listDetay = await _odiListeDataServis.OdiListeDetayListesi(listeId.ToString());
            List<OdiListeDetayOutputDTO> outputListe = new List<OdiListeDetayOutputDTO>();

            foreach (var item in listDetay)
            {
                OdiListeDetayOutputDTO output = new OdiListeDetayOutputDTO();
                output.OdiListeDetayId = item.Id;
                output.ListeyeEklenmeTarihi = item.ListeyeEklenmeTarihi;
                output.OdiTalep = await _performerOdiLogicService.OdiTalepGetir(item.OdiTalepId);
                PerformerOdi performerOdi = await _performerOdiLogicService.PerformerOdiGetir(item.OdiTalepId);
                output.PerformerOdi = _performerOdiLogicService.PerformerOdiOuputGetir(performerOdi);
                outputListe.Add(output);
            }
            odiListe.Odiler = outputListe;
            return OdiResponse<OdiListeOutputDTO>.Success("Odi Liste odileri getirildi", odiListe, 200);
        }

        public async Task<OdiResponse<bool>> OdiListeSil(OdiListeIdDTO listeId)
        {
            await _odiListeDataServis.OdiListeSil(listeId.ToString());
            return OdiResponse<bool>.Success("Odi Listesi silindi", true, 200);
        }

        public async Task<OdiResponse<bool>> OdiListeDetaySil(List<OdiListeDetayIdDTO> detayIdList)
        {
            await _odiListeDataServis.OdiListeDetaySil(detayIdList.Select(x => x.OdiListeDetayId).ToList());
            return OdiResponse<bool>.Success("Odiler listeden silindi", true, 200);
        }
    }
}
