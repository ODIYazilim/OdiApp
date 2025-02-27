using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Hubs.MesajlasmaHubs;
using OdiApp.BusinessLayer.Services.BildirimLogicServices.OdiBildirimLogicServices;
using OdiApp.DataAccessLayer.BildirimDataServices.ProjeMesajlasmaDataServices;
using OdiApp.DTOs.BildirimDTOs.ProjeMesajlasma;
using OdiApp.DTOs.Enums;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.BildirimDTOs;
using OdiApp.EntityLayer.BildirimModels.ProjeMesajlasma;

namespace OdiApp.BusinessLayer.Services.BildirimLogicServices.ProjeMesajlasmaLogicServices
{
    public class ProjeMesajLogicService : IProjeMesajLogicService
    {
        private readonly IProjeMesajlasmaDataService _projeMesajDataService;
        private readonly IMapper _mapper;
        private readonly IAmazonS3Service _amazonS3Service;
        private readonly IOdiBildirimLogicService _odiBildirimLogicService;
        private readonly IHubContext<MesajlasmaHub, IMesajlasmaHub> _mesajlasmaHub;

        public ProjeMesajLogicService(IProjeMesajlasmaDataService projeMesajDataService, IMapper mapper, IAmazonS3Service amazonS3Service, IHubContext<MesajlasmaHub, IMesajlasmaHub> mesajlasmaHub, IOdiBildirimLogicService odiBildirimLogicService)
        {
            _projeMesajDataService = projeMesajDataService;
            _mapper = mapper;
            _amazonS3Service = amazonS3Service;
            _mesajlasmaHub = mesajlasmaHub;
            _odiBildirimLogicService = odiBildirimLogicService;
        }

        public async Task<OdiResponse<ProjeMesajOutputDTO>> YeniProjeMesaj(ProjeMesajCreateDTO projeMesajDTO, OdiUser user, string jwtToken)
        {
            ProjeMesajOutputDTO result = await _projeMesajDataService.ProjeMesajGetir(projeMesajDTO.Kullanici1Id, projeMesajDTO.Kullanici2Id);

            if (result != null) return OdiResponse<ProjeMesajOutputDTO>.Success("Var olan mesaj bilgisi getirildi.", result, 200);

            ProjeMesaj projeMesaj = new ProjeMesaj();

            projeMesaj.Kullanici1Id = projeMesajDTO.Kullanici1Id;
            projeMesaj.Kullanici2Id = projeMesajDTO.Kullanici2Id;
            projeMesaj.PerformerId = projeMesajDTO.PerformerId;
            projeMesaj.ProjeId = projeMesajDTO.ProjeId;
            projeMesaj.ProjeAdi = projeMesajDTO.ProjeAdi;
            projeMesaj.ProjeResmi = projeMesajDTO.ProjeResmi;
            projeMesaj.ProjeRolId = projeMesajDTO.ProjeRolId;
            projeMesaj.ProjeRolAdi = projeMesajDTO.ProjeRolAdi;

            DateTime date = DateTime.Now;

            projeMesaj.EklenmeTarihi = date;
            projeMesaj.Ekleyen = user.AdSoyad;
            projeMesaj.EkleyenId = user.Id;

            projeMesaj.GuncellenmeTarihi = date;
            projeMesaj.Guncelleyen = user.AdSoyad;
            projeMesaj.GuncelleyenId = user.Id;

            projeMesaj = await _projeMesajDataService.YeniProjeMesaj(projeMesaj);

            List<ProjeMesajDetay> projeMesajDetayList = _mapper.Map<List<ProjeMesajDetay>>(projeMesajDTO.ProjeMesajDetaylar);

            foreach (var item in projeMesajDetayList)
            {
                item.EklenmeTarihi = date;
                item.Ekleyen = user.AdSoyad;
                item.EkleyenId = user.Id;

                item.GuncellenmeTarihi = date;
                item.Guncelleyen = user.AdSoyad;
                item.GuncelleyenId = user.Id;

                item.ProjeMesajId = projeMesaj.Id;
            }

            await _projeMesajDataService.YeniProjeMesajDetay(projeMesajDetayList);

            List<ProjeMesajDetayOutputDTO> projeMesajDetayDTOList = await _projeMesajDataService.ProjeMesajDetayListesi(projeMesajDetayList.Select(s => s.Id).ToList());

            var userConnection = MesajlasmaHub.KullaniciList.FirstOrDefault(t => t.Item1 == projeMesajDetayDTOList.Select(s => s.GonderilenKullaniciId).FirstOrDefault());

            foreach (var item in projeMesajDetayDTOList)
            {
                item.DosyaMesajPresignedUrl = item.MesajDosyami ? _amazonS3Service.GetPreSignedUrl(item.DosyaMesajDosyaYolu) : "";

                if (userConnection != null)
                {
                    var connectionId = userConnection.Item2;
                    await _mesajlasmaHub.Clients.Client(connectionId).YeniProjeMesajDinle(item);
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

            return OdiResponse<ProjeMesajOutputDTO>.Success("Yeni mesaj oluşturuldu.", await _projeMesajDataService.ProjeMesajGetir(projeMesaj.Id), 200);
        }

        public async Task<OdiResponse<ProjeMesajDetayOutputDTO>> YeniProjeMesajDetay(ProjeMesajDetayCreateDTO projeMesajDetayDTO, OdiUser user, string jwtToken)
        {
            ProjeMesajDetay projeMesajDetay = _mapper.Map<ProjeMesajDetay>(projeMesajDetayDTO);

            DateTime date = DateTime.Now;

            projeMesajDetay.EklenmeTarihi = date;
            projeMesajDetay.Ekleyen = user.AdSoyad;
            projeMesajDetay.EkleyenId = user.Id;

            projeMesajDetay.GuncellenmeTarihi = date;
            projeMesajDetay.Guncelleyen = user.AdSoyad;
            projeMesajDetay.GuncelleyenId = user.Id;

            projeMesajDetay = await _projeMesajDataService.YeniProjeMesajDetay(projeMesajDetay);
            ProjeMesajDetayOutputDTO projeMesajDetayOutput = await _projeMesajDataService.ProjeMesajDetayGetir(projeMesajDetay.Id);

            projeMesajDetayOutput.GonderenKullaniciProfilResmi = _amazonS3Service.GetPreSignedUrl(projeMesajDetayOutput.GonderenKullaniciProfilResmiDosyaYolu);
            projeMesajDetayOutput.GonderilenKullaniciProfilResmi = _amazonS3Service.GetPreSignedUrl(projeMesajDetayOutput.GonderilenKullaniciProfilResmiDosyaYolu);
            projeMesajDetayOutput.DosyaMesajPresignedUrl = projeMesajDetayOutput.MesajDosyami ? _amazonS3Service.GetPreSignedUrl(projeMesajDetayOutput.DosyaMesajDosyaYolu) : "";

            var userConnection = MesajlasmaHub.KullaniciList.FirstOrDefault(t => t.Item1 == projeMesajDetayOutput.GonderilenKullaniciId);

            if (userConnection != null)
            {
                var connectionId = userConnection.Item2;
                await _mesajlasmaHub.Clients.Client(connectionId).YeniProjeMesajDinle(projeMesajDetayOutput);
            }
            else
            {
                OdiBildirimCreateDTO bildirim = new OdiBildirimCreateDTO();
                bildirim.BildirimTipi = BildirimTipleri.YeniMesaj;
                bildirim.Mesaj = $"{projeMesajDetayOutput.GonderenKullaniciAdSoyad} tarafından yeni bir mesajınız var";
                bildirim.Baslik = "Yeni Mesaj";
                bildirim.AltBaslik = "";
                bildirim.KullaniciId = projeMesajDetayOutput.GonderilenKullaniciId;
                bildirim.KullaniciAdSoyad = projeMesajDetayOutput.GonderilenKullaniciAdSoyad;
                bildirim.GonderenKullaniciId = projeMesajDetayOutput.GonderenKullaniciId;
                bildirim.GonderenKullaniciAdSoyad = projeMesajDetayOutput.GonderenKullaniciAdSoyad;
                bildirim.DosyaYolu = projeMesajDetayOutput.GonderenKullaniciProfilResmiDosyaYolu ?? string.Empty;

                await _odiBildirimLogicService.YeniOdiBildirim(bildirim, user);
            }

            return OdiResponse<ProjeMesajDetayOutputDTO>.Success("Yeni mesaj detay oluşturuldu.", projeMesajDetayOutput, 200);
        }

        public async Task<OdiResponse<List<ProjeMesajProjeDTO>>> ProjeMesajListesi(ProjeMesajListesiInputDTO requestModel)
        {
            List<ProjeMesajOutputDTO> mesajList = await _projeMesajDataService.ProjeMesajListesiWithKullaniciId(requestModel.KullaniciId);

            List<ProjeMesajProjeDTO> result = new List<ProjeMesajProjeDTO>();

            if (mesajList?.Any() == true)
            {
                foreach (var item in mesajList)
                {
                    item.Kullanici1ProfilResmi = _amazonS3Service.GetPreSignedUrl(item.Kullanici1ProfilResmiDosyaYolu);
                    item.Kullanici2ProfilResmi = _amazonS3Service.GetPreSignedUrl(item.Kullanici2ProfilResmiDosyaYolu);
                    item.PerformerProfilResmi = _amazonS3Service.GetPreSignedUrl(item.PerformerProfilResmiDosyaYolu);

                    var existingProje = result.FirstOrDefault(x => x.ProjeId == item.ProjeId);

                    if (existingProje == null)
                    {
                        var proje = new ProjeMesajProjeDTO
                        {
                            ProjeId = item.ProjeId,
                            ProjeAdi = item.ProjeAdi,
                            ProjeResmi = item.ProjeResmi,
                            ProjeMesajlari = new List<ProjeMesajOutputDTO> { item }
                        };

                        result.Add(proje);
                    }
                    else
                    {
                        existingProje.ProjeMesajlari.Add(item);
                    }
                }
            }

            return OdiResponse<List<ProjeMesajProjeDTO>>.Success("Liste getirildi.", result, 200);
        }

        public async Task<OdiResponse<PagedData<ProjeMesajDetayOutputDTO>>> ProjeMesajDetayListesi(ProjeMesajDetayListesiInputDTO projeMesajDetayListesiInputDTO)
        {
            PagedData<ProjeMesajDetayOutputDTO> pagedData = await _projeMesajDataService.ProjeMesajDetayListesi(projeMesajDetayListesiInputDTO.Kullanici1Id, projeMesajDetayListesiInputDTO.Kullanici2Id, projeMesajDetayListesiInputDTO.PageNo, projeMesajDetayListesiInputDTO.RecordsPerPage);

            if (pagedData?.DataList?.Any() == true)
            {
                foreach (var item in pagedData.DataList)
                {
                    item.GonderenKullaniciProfilResmi = _amazonS3Service.GetPreSignedUrl(item.GonderenKullaniciProfilResmiDosyaYolu);
                    item.GonderilenKullaniciProfilResmi = _amazonS3Service.GetPreSignedUrl(item.GonderilenKullaniciProfilResmiDosyaYolu);
                    item.DosyaMesajPresignedUrl = item.MesajDosyami ? _amazonS3Service.GetPreSignedUrl(item.DosyaMesajDosyaYolu) : "";
                }
            }

            return OdiResponse<PagedData<ProjeMesajDetayOutputDTO>>.Success("Liste getirildi.", pagedData, 200);
        }

        public async Task<OdiResponse<bool>> ProjeMesajGoruldu(ProjeMesajDetayIdDTO projeMesajDetayId)
        {
            bool result = await _projeMesajDataService.ProjeMesajGoruldu(projeMesajDetayId.ProjeMesajDetayId);

            if (result)
            {
                ProjeMesajDetayOutputDTO projeMesajDetayOutput = await _projeMesajDataService.ProjeMesajDetayGetir(projeMesajDetayId.ProjeMesajDetayId);

                var gonderenUserConnection = MesajlasmaHub.KullaniciList.FirstOrDefault(t => t.Item1 == projeMesajDetayOutput.GonderenKullaniciId);
                var gonderilenUserConnection = MesajlasmaHub.KullaniciList.FirstOrDefault(t => t.Item1 == projeMesajDetayOutput.GonderilenKullaniciId);

                if (gonderenUserConnection != null || gonderilenUserConnection != null)
                {
                    List<ProjeMesajOkunduDinlemeOutputDTO> projeMesajOkunduDinlemeOutputDTOList = new List<ProjeMesajOkunduDinlemeOutputDTO>
                    {
                        new ProjeMesajOkunduDinlemeOutputDTO
                        {
                            OkunduMu = true,
                            ProjeMesajDetayId = projeMesajDetayOutput.ProjeMesajDetayId
                        }
                    };

                    if (gonderenUserConnection != null)
                    {
                        var connectionId = gonderenUserConnection.Item2;
                        await _mesajlasmaHub.Clients.Client(connectionId).ProjeMesajOkunduDinleme(projeMesajOkunduDinlemeOutputDTOList);
                    }

                    if (gonderilenUserConnection != null)
                    {
                        var connectionId = gonderilenUserConnection.Item2;
                        await _mesajlasmaHub.Clients.Client(connectionId).ProjeMesajOkunduDinleme(projeMesajOkunduDinlemeOutputDTOList);
                    }
                }

                return OdiResponse<bool>.Success("Görüldü bilgisi iletildi.", result, 200);
            }
            else return OdiResponse<bool>.Fail("Mesaj bulunamadı. Görüldü işlemi başarısız.", "Not Found", 404);
        }

        public async Task<OdiResponse<bool>> ProjeMesajGorulduListe(List<ProjeMesajDetayIdDTO> projeMesajDetayIdListe)
        {
            bool result = await _projeMesajDataService.ProjeMesajGoruldu(projeMesajDetayIdListe.Select(s => s.ProjeMesajDetayId).ToList());

            if (result)
            {
                List<ProjeMesajDetayOutputDTO> projeMesajDetayOutputList = await _projeMesajDataService.ProjeMesajDetayListesi(projeMesajDetayIdListe.Select(s => s.ProjeMesajDetayId).ToList());

                var gonderenUserConnection = MesajlasmaHub.KullaniciList.FirstOrDefault(t => t.Item1 == projeMesajDetayOutputList.FirstOrDefault().GonderenKullaniciId);
                var gonderilenUserConnection = MesajlasmaHub.KullaniciList.FirstOrDefault(t => t.Item1 == projeMesajDetayOutputList.FirstOrDefault().GonderilenKullaniciId);

                if (gonderenUserConnection != null || gonderilenUserConnection != null)
                {
                    List<ProjeMesajOkunduDinlemeOutputDTO> projeMesajOkunduDinlemeOutputDTOList = projeMesajDetayOutputList.Select(s => new ProjeMesajOkunduDinlemeOutputDTO() { ProjeMesajDetayId = s.ProjeMesajDetayId, OkunduMu = true }).ToList();

                    if (gonderenUserConnection != null)
                    {
                        var connectionId = gonderenUserConnection.Item2;
                        await _mesajlasmaHub.Clients.Client(connectionId).ProjeMesajOkunduDinleme(projeMesajOkunduDinlemeOutputDTOList);
                    }

                    if (gonderilenUserConnection != null)
                    {
                        var connectionId = gonderilenUserConnection.Item2;
                        await _mesajlasmaHub.Clients.Client(connectionId).ProjeMesajOkunduDinleme(projeMesajOkunduDinlemeOutputDTOList);
                    }
                }

                return OdiResponse<bool>.Success("Görüldü bilgisi iletildi.", result, 200);
            }
            else return OdiResponse<bool>.Fail("Mesaj bulunamadı. Görüldü işlemi başarısız.", "Not Found", 404);
        }

        public async Task<OdiResponse<bool>> ProjeMesajSilme(ProjeMesajIdDTO projeMesajId)
        {
            bool result = await _projeMesajDataService.ProjeMesajSilme(projeMesajId.ProjeMesajId);
            if (result) return OdiResponse<bool>.Success("Mesaj silindi.", result, 200);
            else return OdiResponse<bool>.Fail("Mesaj bulunamadı. Silme işlemi başarısız.", "Not Found", 404);
        }

        public async Task<OdiResponse<bool>> ProjeMesajSilmeListe(List<ProjeMesajIdDTO> projeMesajIdList)
        {
            bool result = await _projeMesajDataService.ProjeMesajSilme(projeMesajIdList.Select(s => s.ProjeMesajId).ToList());
            if (result) return OdiResponse<bool>.Success("Mesajlar silindi.", result, 200);
            else return OdiResponse<bool>.Fail("Mesajlar bulunamadı. Silme işlemi başarısız.", "Not Found", 404);
        }

        public async Task<OdiResponse<bool>> ProjeMesajDetaySilme(ProjeMesajDetayIdDTO projeMesajDetayId)
        {
            bool result = await _projeMesajDataService.ProjeMesajDetaySilme(projeMesajDetayId.ProjeMesajDetayId);
            if (result) return OdiResponse<bool>.Success("Mesaj detay silindi.", result, 200);
            else return OdiResponse<bool>.Fail("Mesaj bulunamadı. Silme işlemi başarısız.", "Not Found", 404);
        }

        public async Task<OdiResponse<bool>> ProjeMesajDetaySilmeListe(List<ProjeMesajDetayIdDTO> projeMesajDetayIdList)
        {
            bool result = await _projeMesajDataService.ProjeMesajDetaySilme(projeMesajDetayIdList.Select(s => s.ProjeMesajDetayId).ToList());
            if (result) return OdiResponse<bool>.Success("Mesaj detayları silindi.", result, 200);
            else return OdiResponse<bool>.Fail("Mesaj detayları bulunamadı. Silme işlemi başarısız.", "Not Found", 404);
        }
    }
}