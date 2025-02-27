using AutoMapper;
using OdiApp.DataAccessLayer.BildirimDataServices.MutluCellSmsDataServices;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.BildirimModels.SmsAyarlariModels;

namespace OdiApp.BusinessLayer.Services.BildirimLogicServices.MutluCellSmsLogicServices
{
    public class MutluCellSmsLogicService : IMutluCellSmsLogicService
    {
        private readonly IMapper _mapper;
        private readonly IMutluCellSmsDataService _mutluCellSmsDataService;

        public MutluCellSmsLogicService(IMapper mapper, IMutluCellSmsDataService mutluCellSmsDataService)
        {
            _mapper = mapper;
            _mutluCellSmsDataService = mutluCellSmsDataService;
        }

        public async Task<OdiResponse<MutluCellSmsAyarlari>> AyarlariGuncelle(MutluCellSmsAyarlari model, OdiUser user)
        {
            MutluCellSmsAyarlari ayarlar = await _mutluCellSmsDataService.AyarlariGetir();

            if (ayarlar == null) return OdiResponse<MutluCellSmsAyarlari>.Fail("Ayarlar bulunamadı.", "Not Found", 404);

            ayarlar = _mapper.Map<MutluCellSmsAyarlari, MutluCellSmsAyarlari>(model, ayarlar);

            ayarlar.GuncellenmeTarihi = DateTime.Now;
            ayarlar.Guncelleyen = user.AdSoyad;
            ayarlar.GuncelleyenId = user.Id;

            ayarlar = await _mutluCellSmsDataService.AyarlariGuncelle(ayarlar);

            return OdiResponse<MutluCellSmsAyarlari>.Success("Ayarlar güncellendi.", ayarlar, 200);
        }

        public async Task<OdiResponse<MutluCellSmsAyarlari>> AyarlariGetir()
        {
            return OdiResponse<MutluCellSmsAyarlari>.Success("Ayarlar getirildi", await _mutluCellSmsDataService.AyarlariGetir(), 200);
        }
    }
}