using OdiApp.EntityLayer.PerformerModels.SeslendirmeDiliModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.SeslendirmeDiliDataServices;

public interface ISeslendirmeDiliDataService
{
    Task<SeslendirmeDili> YeniSeslendirmeDili(SeslendirmeDili model);
    Task<SeslendirmeDili> SeslendirmeDiliGuncelle(SeslendirmeDili model);
    Task<List<SeslendirmeDili>> SeslendirmeDiliListesiGetir(int dilId);
    Task<SeslendirmeDili> SeslendirmeDiliGetir(int id);
}