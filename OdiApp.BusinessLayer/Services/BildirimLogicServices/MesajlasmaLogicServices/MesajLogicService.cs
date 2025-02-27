using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Hubs.MesajlasmaHubs;
using OdiApp.BusinessLayer.Services.BildirimLogicServices.OdiBildirimLogicServices;
using OdiApp.DataAccessLayer.BildirimDataServices.MesajlasmaDataServices;
using OdiApp.DTOs.BildirimDTOs.Mesajlasma;
using OdiApp.DTOs.Enums;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.BildirimDTOs;
using OdiApp.EntityLayer.BildirimModels.Mesajlasma;

namespace OdiApp.BusinessLayer.Services.BildirimLogicServices.MesajlasmaLogicServices
{
    public class MesajLogicService : IMesajLogicService
    {
        private readonly IMesajlasmaDataService _mesajDataService;
        private readonly IMapper _mapper;
        private readonly IAmazonS3Service _amazonS3Service;
        private readonly IConfiguration _configuration;
        private readonly IOdiBildirimLogicService _odiBildirimLogicService;
        private readonly IHubContext<MesajlasmaHub, IMesajlasmaHub> _mesajlasmaHub;

        public MesajLogicService(IMesajlasmaDataService mesajDataService, IMapper mapper, IAmazonS3Service amazonS3Service, IConfiguration configuration, IOdiBildirimLogicService odiBildirimLogicService, IHubContext<MesajlasmaHub, IMesajlasmaHub> mesajlasmaHub)
        {
            _mesajDataService = mesajDataService;
            _mapper = mapper;
            _amazonS3Service = amazonS3Service;
            _configuration = configuration;
            _odiBildirimLogicService = odiBildirimLogicService;
            _mesajlasmaHub = mesajlasmaHub;
        }

        public async Task<OdiResponse<MesajOutputDTO>> YeniMesaj(MesajCreateDTO mesajDTO, OdiUser user)
        {
            MesajOutputDTO result = await _mesajDataService.MesajGetir(mesajDTO.Kullanici1Id, mesajDTO.Kullanici2Id);

            if (result != null) return OdiResponse<MesajOutputDTO>.Success("Var olan mesaj bilgisi getirildi.", result, 200);

            Mesaj mesaj = new Mesaj();

            mesaj.Kullanici1Id = mesajDTO.Kullanici1Id;
            mesaj.Kullanici2Id = mesajDTO.Kullanici2Id;

            DateTime date = DateTime.Now;

            mesaj.EklenmeTarihi = date;
            mesaj.Ekleyen = user.AdSoyad;
            mesaj.EkleyenId = user.Id;

            mesaj.GuncellenmeTarihi = date;
            mesaj.Guncelleyen = user.AdSoyad;
            mesaj.GuncelleyenId = user.Id;

            mesaj = await _mesajDataService.YeniMesaj(mesaj);

            List<MesajDetay> mesajDetayList = _mapper.Map<List<MesajDetay>>(mesajDTO.MesajDetaylar);

            foreach (var item in mesajDetayList)
            {
                item.EklenmeTarihi = date;
                item.Ekleyen = user.AdSoyad;
                item.EkleyenId = user.Id;

                item.GuncellenmeTarihi = date;
                item.Guncelleyen = user.AdSoyad;
                item.GuncelleyenId = user.Id;

                item.MesajId = mesaj.Id;
            }

            mesajDetayList = await _mesajDataService.YeniMesajDetay(mesajDetayList);
            List<MesajDetayOutputDTO> mesajDetayDTOList = await _mesajDataService.MesajDetayListesi(mesajDetayList.Select(s => s.Id).ToList());

            var userConnection = MesajlasmaHub.KullaniciList.FirstOrDefault(t => t.Item1 == mesajDetayDTOList.Select(s => s.GonderilenKullaniciId).FirstOrDefault());

            foreach (var item in mesajDetayDTOList)
            {
                item.DosyaMesajPresignedUrl = item.MesajDosyami ? _amazonS3Service.GetPreSignedUrl(item.DosyaMesajDosyaYolu) : "";

                if (userConnection != null)
                {
                    var connectionId = userConnection.Item2;
                    await _mesajlasmaHub.Clients.Client(connectionId).YeniMesajDinle(item);
                }
                else
                {
                    OdiBildirimCreateDTO bildirim = new OdiBildirimCreateDTO();
                    bildirim.BildirimTipi = BildirimTipleri.YeniMesaj;
                    bildirim.Mesaj = $"{item.GonderenKullaniciAdSoyad} tarafından yeni bir mesajınız var";
                    bildirim.Baslik = "Yeni Mesaj";
                    bildirim.AltBaslik = "";
                    bildirim.KullaniciId = item.GonderilenKullaniciId;
                    bildirim.KullaniciAdSoyad = item.GonderilenKullaniciAdSoyad;
                    bildirim.GonderenKullaniciId = item.GonderenKullaniciId;
                    bildirim.GonderenKullaniciAdSoyad = item.GonderenKullaniciAdSoyad;
                    bildirim.DosyaYolu = item.GonderenKullaniciProfilResmiDosyaYolu ?? string.Empty;

                    await _odiBildirimLogicService.YeniOdiBildirim(bildirim, user);
                }
            }


            return OdiResponse<MesajOutputDTO>.Success("Yeni mesaj oluşturuldu.", await _mesajDataService.MesajGetir(mesaj.Id), 200);
        }

        public async Task<OdiResponse<MesajDetayOutputDTO>> YeniMesajDetay(MesajDetayCreateDTO mesajDetayDTO, OdiUser user)
        {
            MesajDetay mesajDetay = _mapper.Map<MesajDetay>(mesajDetayDTO);

            DateTime date = DateTime.Now;

            mesajDetay.EklenmeTarihi = date;
            mesajDetay.Ekleyen = user.AdSoyad;
            mesajDetay.EkleyenId = user.Id;

            mesajDetay.GuncellenmeTarihi = date;
            mesajDetay.Guncelleyen = user.AdSoyad;
            mesajDetay.GuncelleyenId = user.Id;

            mesajDetay = await _mesajDataService.YeniMesajDetay(mesajDetay);
            MesajDetayOutputDTO mesajDetayOutput = await _mesajDataService.MesajDetayGetir(mesajDetay.Id);

            mesajDetayOutput.GonderenKullaniciProfilResmi = _amazonS3Service.GetPreSignedUrl(mesajDetayOutput.GonderenKullaniciProfilResmiDosyaYolu);
            mesajDetayOutput.GonderilenKullaniciProfilResmi = _amazonS3Service.GetPreSignedUrl(mesajDetayOutput.GonderilenKullaniciProfilResmiDosyaYolu);
            mesajDetayOutput.DosyaMesajPresignedUrl = mesajDetayOutput.MesajDosyami ? _amazonS3Service.GetPreSignedUrl(mesajDetayOutput.DosyaMesajDosyaYolu) : "";

            var userConnection = MesajlasmaHub.KullaniciList.FirstOrDefault(t => t.Item1 == mesajDetayOutput.GonderilenKullaniciId);

            if (userConnection != null)
            {
                var connectionId = userConnection.Item2;
                await _mesajlasmaHub.Clients.Client(connectionId).YeniMesajDinle(mesajDetayOutput);
            }
            else
            {
                OdiBildirimCreateDTO bildirim = new OdiBildirimCreateDTO();
                bildirim.BildirimTipi = BildirimTipleri.YeniMesaj;
                bildirim.Mesaj = $"{mesajDetayOutput.GonderenKullaniciAdSoyad} tarafından yeni bir mesajınız var";
                bildirim.Baslik = "Yeni Mesaj";
                bildirim.AltBaslik = "";
                bildirim.KullaniciId = mesajDetayOutput.GonderilenKullaniciId;
                bildirim.KullaniciAdSoyad = mesajDetayOutput.GonderilenKullaniciAdSoyad;
                bildirim.GonderenKullaniciId = mesajDetayOutput.GonderenKullaniciId;
                bildirim.GonderenKullaniciAdSoyad = mesajDetayOutput.GonderenKullaniciAdSoyad;
                bildirim.DosyaYolu = mesajDetayOutput.GonderenKullaniciProfilResmiDosyaYolu ?? string.Empty;

                await _odiBildirimLogicService.YeniOdiBildirim(bildirim, user);
            }

            return OdiResponse<MesajDetayOutputDTO>.Success("Yeni mesaj detay oluşturuldu.", mesajDetayOutput, 200);
        }

        public async Task<OdiResponse<List<MesajOutputDTO>>> MesajListesi(KullaniciIdDTO kullaniciIdDTO)
        {
            List<MesajOutputDTO> list = await _mesajDataService.MesajListesi(kullaniciIdDTO.KullaniciId);

            if (list?.Any() == true)
            {
                foreach (var item in list)
                {
                    item.Kullanici1ProfilResmi = _amazonS3Service.GetPreSignedUrl(item.Kullanici1ProfilResmiDosyaYolu);
                    item.Kullanici2ProfilResmi = _amazonS3Service.GetPreSignedUrl(item.Kullanici2ProfilResmiDosyaYolu);
                }
            }

            return OdiResponse<List<MesajOutputDTO>>.Success("Liste getirildi.", list, 200);
        }

        public async Task<OdiResponse<PagedData<MesajDetayOutputDTO>>> MesajDetayListesi(MesajDetayListesiInputDTO mesajDetayListesiInputDTO)
        {
            PagedData<MesajDetayOutputDTO> pagedData = await _mesajDataService.MesajDetayListesi(mesajDetayListesiInputDTO.Kullanici1Id, mesajDetayListesiInputDTO.Kullanici2Id, mesajDetayListesiInputDTO.PageNo, mesajDetayListesiInputDTO.RecordsPerPage);

            if (pagedData?.DataList?.Any() == true)
            {
                foreach (var item in pagedData.DataList)
                {
                    item.GonderenKullaniciProfilResmi = _amazonS3Service.GetPreSignedUrl(item.GonderenKullaniciProfilResmiDosyaYolu);
                    item.GonderilenKullaniciProfilResmi = _amazonS3Service.GetPreSignedUrl(item.GonderilenKullaniciProfilResmiDosyaYolu);
                    item.DosyaMesajPresignedUrl = item.MesajDosyami ? _amazonS3Service.GetPreSignedUrl(item.DosyaMesajDosyaYolu) : "";
                }
            }

            return OdiResponse<PagedData<MesajDetayOutputDTO>>.Success("Liste getirildi.", pagedData, 200);
        }

        public async Task<OdiResponse<bool>> MesajGoruldu(MesajDetayIdDTO mesajDetayId)
        {
            bool result = await _mesajDataService.MesajGoruldu(mesajDetayId.MesajDetayId);

            if (result)
            {
                MesajDetayOutputDTO mesajDetayOutput = await _mesajDataService.MesajDetayGetir(mesajDetayId.MesajDetayId);

                var gonderenUserConnection = MesajlasmaHub.KullaniciList.FirstOrDefault(t => t.Item1 == mesajDetayOutput.GonderenKullaniciId);
                var gonderilenUserConnection = MesajlasmaHub.KullaniciList.FirstOrDefault(t => t.Item1 == mesajDetayOutput.GonderilenKullaniciId);

                if (gonderenUserConnection != null || gonderilenUserConnection != null)
                {
                    List<MesajOkunduDinlemeOutputDTO> mesajOkunduDinlemeOutputDTOList = new List<MesajOkunduDinlemeOutputDTO>
                    {
                        new MesajOkunduDinlemeOutputDTO
                        {
                            OkunduMu = true,
                            MesajDetayId = mesajDetayOutput.MesajDetayId
                        }
                    };

                    if (gonderenUserConnection != null)
                    {
                        var connectionId = gonderenUserConnection.Item2;
                        await _mesajlasmaHub.Clients.Client(connectionId).MesajOkunduDinleme(mesajOkunduDinlemeOutputDTOList);
                    }
                    if (gonderilenUserConnection != null)
                    {
                        var connectionId = gonderilenUserConnection.Item2;
                        await _mesajlasmaHub.Clients.Client(connectionId).MesajOkunduDinleme(mesajOkunduDinlemeOutputDTOList);
                    }
                }

                return OdiResponse<bool>.Success("Görüldü bilgisi iletildi.", result, 200);
            }

            else return OdiResponse<bool>.Fail("Mesaj bulunamadı. Görüldü işlemi başarısız.", "Not Found", 404);
        }

        public async Task<OdiResponse<bool>> MesajGorulduListe(List<MesajDetayIdDTO> mesajDetayIdListe)
        {

            bool result = await _mesajDataService.MesajGoruldu(mesajDetayIdListe.Select(s => s.MesajDetayId).ToList());

            if (result)
            {
                List<MesajDetayOutputDTO> mesajDetayOutputList = await _mesajDataService.MesajDetayListesi(mesajDetayIdListe.Select(s => s.MesajDetayId).ToList());

                var gonderenUserConnection = MesajlasmaHub.KullaniciList.FirstOrDefault(t => t.Item1 == mesajDetayOutputList.FirstOrDefault().GonderenKullaniciId);
                var gonderilenUserConnection = MesajlasmaHub.KullaniciList.FirstOrDefault(t => t.Item1 == mesajDetayOutputList.FirstOrDefault().GonderilenKullaniciId);

                if (gonderenUserConnection != null || gonderilenUserConnection != null)
                {
                    List<MesajOkunduDinlemeOutputDTO> mesajOkunduDinlemeOutputDTOList = mesajDetayOutputList.Select(s => new MesajOkunduDinlemeOutputDTO() { MesajDetayId = s.MesajDetayId, OkunduMu = true }).ToList();

                    if (gonderenUserConnection != null)
                    {
                        var connectionId = gonderenUserConnection.Item2;
                        await _mesajlasmaHub.Clients.Client(connectionId).MesajOkunduDinleme(mesajOkunduDinlemeOutputDTOList);
                    }

                    if (gonderilenUserConnection != null)
                    {
                        var connectionId = gonderilenUserConnection.Item2;
                        await _mesajlasmaHub.Clients.Client(connectionId).MesajOkunduDinleme(mesajOkunduDinlemeOutputDTOList);
                    }
                }

                return OdiResponse<bool>.Success("Görüldü bilgisi iletildi.", result, 200);
            }
            else return OdiResponse<bool>.Fail("Mesaj bulunamadı. Görüldü işlemi başarısız.", "Not Found", 404);
        }

        public async Task<OdiResponse<bool>> MesajSilme(MesajIdDTO mesajId)
        {
            bool result = await _mesajDataService.MesajSilme(mesajId.MesajId);
            if (result) return OdiResponse<bool>.Success("Mesaj silindi.", result, 200);
            else return OdiResponse<bool>.Fail("Mesaj bulunamadı. Silme işlemi başarısız.", "Not Found", 404);
        }

        public async Task<OdiResponse<bool>> MesajSilmeListe(List<MesajIdDTO> mesajIdList)
        {
            bool result = await _mesajDataService.MesajSilme(mesajIdList.Select(s => s.MesajId).ToList());
            if (result) return OdiResponse<bool>.Success("Mesajlar silindi.", result, 200);
            else return OdiResponse<bool>.Fail("Mesajlar bulunamadı. Silme işlemi başarısız.", "Not Found", 404);
        }

        public async Task<OdiResponse<bool>> MesajDetaySilme(MesajDetayIdDTO mesajDetayId)
        {
            bool result = await _mesajDataService.MesajDetaySilme(mesajDetayId.MesajDetayId);
            if (result) return OdiResponse<bool>.Success("Mesaj silindi.", result, 200);
            else return OdiResponse<bool>.Fail("Mesaj bulunamadı. Silme işlemi başarısız.", "Not Found", 404);
        }

        public async Task<OdiResponse<bool>> MesajDetaySilmeListe(List<MesajDetayIdDTO> mesajDetayIdList)
        {
            bool result = await _mesajDataService.MesajDetaySilme(mesajDetayIdList.Select(s => s.MesajDetayId).ToList());
            if (result) return OdiResponse<bool>.Success("Mesaj detayları silindi.", result, 200);
            else return OdiResponse<bool>.Fail("Mesaj detayları bulunamadı. Silme işlemi başarısız.", "Not Found", 404);
        }
    }
}