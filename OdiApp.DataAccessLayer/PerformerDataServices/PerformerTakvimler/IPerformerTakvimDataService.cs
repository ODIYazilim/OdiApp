using OdiApp.EntityLayer.PerformerModels.PerformerTakvimModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerTakvimler;

public interface IPerformerTakvimDataService
{
    Task<PerformerTakvim> YeniTarihAraligi(PerformerTakvim performerTakvim);
    Task<bool> MusaitlikKontrolu(string performerId, DateTime startDate, DateTime endDate);
    Task<PerformerTakvim> TarihAraligiDuzenle(PerformerTakvim performerTakvim);
    Task<PerformerTakvim> TarihAraligiGetir(string performerTakvimId);
    Task<bool> TarihAraligiSil(string performerTakvimId);
    Task<List<PerformerTakvim>> ZamanAraligiSorgula(string performerId, DateTime baslangicTarihi, DateTime bitisTarihi);
    Task<List<PerformerTakvim>> AylikTakvimSorgula(string performerId, int ay, int yil);
}