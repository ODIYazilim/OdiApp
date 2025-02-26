using OdiApp.EntityLayer.PerformerModels.KisiselOzellikler;

namespace OdiApp.DataAccessLayer.PerformerDataServices.KisiselOzelliklerDataServices;

public interface IKisiselOzelliklerDataService
{
    Task<KisiselOzellik> KisiselOzellikEkle(KisiselOzellik tip);
    Task<KisiselOzellik> KisiselOzellikGuncelle(KisiselOzellik tip);
    Task<KisiselOzellik> KisiselOzellikGetir(int tipId);
    Task<bool> KisiselOzellikSil(KisiselOzellik tip);
    Task<List<KisiselOzellik>> KisiselOzellikListe(int dilId);
}