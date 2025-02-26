using OdiApp.EntityLayer.PerformerModels.SektorModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.SektorDataServices;

public interface ISektorDataService
{
    Task<List<Sektor>> SektorListesiGetir(int dilId);
}
