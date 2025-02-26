using OdiApp.EntityLayer.PerformerModels.PerformerYorumModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerYorumDataServices;

public interface IPerformerYorumDataService
{
    Task<PerformerYorum> YeniPerformerYorum(PerformerYorum model);
    Task<List<PerformerYorum>> PerformerYorumListesiGetir(string performerId);
    Task<PerformerYorum> PerformerYorumGetirById(string id);
    Task<bool> PerformerYorumSil(PerformerYorum model);
}