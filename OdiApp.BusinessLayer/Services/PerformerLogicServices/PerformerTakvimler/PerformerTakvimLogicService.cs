using AutoMapper;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerTakvimler;
using OdiApp.DTOs.PerformerDTOs.PerformerTakvimler;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.PerformerModels.PerformerTakvimModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerTakvimler;

public class PerformerTakvimLogicService : IPerformerTakvimLogicService
{
    private readonly IPerformerTakvimDataService _performerTakvimDataService;
    private readonly IMapper _mapper;

    public PerformerTakvimLogicService(IPerformerTakvimDataService performerTakvimDataService, IMapper mapper)
    {
        _performerTakvimDataService = performerTakvimDataService;
        _mapper = mapper;
    }

    public async Task<OdiResponse<PerformerTakvimOutputDTO>> YeniTarihAraligi(PerformerTakvimCreateDTO performerTakvimDTO, OdiUser user)
    {
        PerformerTakvim performerTakvim = _mapper.Map<PerformerTakvim>(performerTakvimDTO);

        DateTime date = DateTime.Now;

        performerTakvim.EklenmeTarihi = date;
        performerTakvim.Ekleyen = user.AdSoyad;
        performerTakvim.EkleyenId = user.Id;

        performerTakvim.GuncellenmeTarihi = date;
        performerTakvim.Guncelleyen = user.AdSoyad;
        performerTakvim.GuncelleyenId = user.Id;

        performerTakvim = await _performerTakvimDataService.YeniTarihAraligi(performerTakvim);

        return OdiResponse<PerformerTakvimOutputDTO>.Success("Takvime ekleme tamamlandı.", _mapper.Map<PerformerTakvimOutputDTO>(performerTakvim), 200);
    }

    public async Task<OdiResponse<bool>> MusaitlikKontrolu(MusaitlikKontroluDTO musaitlikKontroluDTO)
    {
        return OdiResponse<bool>.Success("Müsaitlik kontrolü yapıldı.", await _performerTakvimDataService.MusaitlikKontrolu(musaitlikKontroluDTO.PerformerId, musaitlikKontroluDTO.BaslangicTarihi, musaitlikKontroluDTO.BitisTarihi), 200);
    }

    public async Task<OdiResponse<PerformerTakvimOutputDTO>> TarihAraligiDuzenle(PerformerTakvimUpdateDTO performerTakvimDTO, OdiUser user)
    {
        PerformerTakvim performerTakvim = await _performerTakvimDataService.TarihAraligiGetir(performerTakvimDTO.PerformerTakvimId);

        if (performerTakvim == null) return OdiResponse<PerformerTakvimOutputDTO>.Fail("Bu id ile bir tarih aralığı bulunamadı.", "Not Found", 404);

        performerTakvim = _mapper.Map(performerTakvimDTO, performerTakvim);

        performerTakvim.GuncellenmeTarihi = DateTime.Now;
        performerTakvim.Guncelleyen = user.AdSoyad;
        performerTakvim.GuncelleyenId = user.Id;

        performerTakvim = await _performerTakvimDataService.TarihAraligiDuzenle(performerTakvim);

        return OdiResponse<PerformerTakvimOutputDTO>.Success("Tarih aralığı güncellendi.", _mapper.Map<PerformerTakvimOutputDTO>(performerTakvim), 200);
    }

    public async Task<OdiResponse<NoContent>> TarihAraligiSil(PerformerTakvimIdDTO performerTakvimIdDTO)
    {
        bool result = await _performerTakvimDataService.TarihAraligiSil(performerTakvimIdDTO.PerformerTakvimId);
        if (result) return OdiResponse<NoContent>.Success("Tarih aralığı silindi.", 200);
        else return OdiResponse<NoContent>.Fail("Tarih aralığı bulunamadı. Silme işlemi başarısız.", "Not Found", 404);
    }

    public async Task<OdiResponse<PerformerTakvimOutputListDTO>> ZamanAraligiSorgula(ZamanAraligiSorgulaDTO zamanAraligiSorgulaDTO)
    {
        List<PerformerTakvim> performerTakvimList = await _performerTakvimDataService.ZamanAraligiSorgula(zamanAraligiSorgulaDTO.PerformerId, zamanAraligiSorgulaDTO.BaslangicTarihi, zamanAraligiSorgulaDTO.BitisTarihi);

        PerformerTakvimOutputListDTO result = new PerformerTakvimOutputListDTO();
        result.TarihAraligiListesi = _mapper.Map<List<PerformerTakvimOutputDTO>>(performerTakvimList);

        return OdiResponse<PerformerTakvimOutputListDTO>.Success("Tarih aralığı kayıtları getirildi.", result, 200);
    }

    public async Task<OdiResponse<PerformerTakvimOutputListDTO>> AylikTakvimSorgula(AylikTakvimSorgulaDTO zamanAraligiSorgulaDTO)
    {
        List<PerformerTakvim> performerTakvimList = await _performerTakvimDataService.AylikTakvimSorgula(zamanAraligiSorgulaDTO.PerformerId, zamanAraligiSorgulaDTO.Ay, zamanAraligiSorgulaDTO.Yil);

        PerformerTakvimOutputListDTO result = new PerformerTakvimOutputListDTO();
        result.TarihAraligiListesi = _mapper.Map<List<PerformerTakvimOutputDTO>>(performerTakvimList);

        return OdiResponse<PerformerTakvimOutputListDTO>.Success("Tarih aralığı kayıtları getirildi.", result, 200);
    }
}