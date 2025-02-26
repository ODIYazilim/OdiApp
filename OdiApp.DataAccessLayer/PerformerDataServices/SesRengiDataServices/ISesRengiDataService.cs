using OdiApp.EntityLayer.PerformerModels.SesRengiModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.SesRengiDataServices;

public interface ISesRengiDataService
{
    Task<SesRengi> YeniSesRengi(SesRengi model);
    Task<SesRengi> SesRengiGuncelle(SesRengi model);
    Task<List<SesRengi>> SesRengiListesiGetir(int dilId);
    Task<SesRengi> SesRengiGetir(int id);
}