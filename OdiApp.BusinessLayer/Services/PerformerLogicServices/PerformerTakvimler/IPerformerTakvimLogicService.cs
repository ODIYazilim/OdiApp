using OdiApp.DTOs.PerformerDTOs.PerformerTakvimler;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerTakvimler;

public interface IPerformerTakvimLogicService
{
    Task<OdiResponse<PerformerTakvimOutputDTO>> YeniTarihAraligi(PerformerTakvimCreateDTO performerTakvimDTO, OdiUser user);
    Task<OdiResponse<bool>> MusaitlikKontrolu(MusaitlikKontroluDTO musaitlikKontroluDTO);
    Task<OdiResponse<PerformerTakvimOutputDTO>> TarihAraligiDuzenle(PerformerTakvimUpdateDTO performerTakvimDTO, OdiUser user);
    Task<OdiResponse<NoContent>> TarihAraligiSil(PerformerTakvimIdDTO performerTakvimIdDTO);
    Task<OdiResponse<PerformerTakvimOutputListDTO>> ZamanAraligiSorgula(ZamanAraligiSorgulaDTO zamanAraligiSorgulaDTO);
    Task<OdiResponse<PerformerTakvimOutputListDTO>> AylikTakvimSorgula(AylikTakvimSorgulaDTO aylikTakvimSorgulaDTO);
}