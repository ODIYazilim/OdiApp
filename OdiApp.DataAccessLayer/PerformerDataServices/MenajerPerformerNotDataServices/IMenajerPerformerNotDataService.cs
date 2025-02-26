using OdiApp.EntityLayer.PerformerModels.MenajerPerformerNotModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.MenajerPerformerNotDataServices;

public interface IMenajerPerformerNotDataService
{
    Task<MenajerPerformerNot> YeniMenajerPerformerNot(MenajerPerformerNot model);
    Task<MenajerPerformerNot> MenajerPerformerNotGuncelle(MenajerPerformerNot model);
    Task<MenajerPerformerNot> MenajerPerformerNotGetir(string performerId, string menajerId);
    Task<bool> MenajerPerformerNotKontrolEt(string performerId, string menajerId);
}