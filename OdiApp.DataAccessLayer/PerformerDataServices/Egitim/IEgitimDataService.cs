using OdiApp.EntityLayer.PerformerModels.Egitim;

namespace OdiApp.DataAccessLayer.PerformerDataServices.Egitim;

public interface IEgitimDataService
{
    Task<OkulBolum> YeniOkulBolum(OkulBolum okulBolum);
    Task<Okul> YeniOkul(Okul okul);
    Task<EgitimTipi> YeniEgitimTipi(EgitimTipi tip);

    Task<List<EgitimTipi>> EgitimListesi();
    Task<List<EgitimTipi>> EgitimTipiListesi();
}