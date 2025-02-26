using OdiApp.EntityLayer.PerformerModels.PerformerCVModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.MenajerPerformerGuncellenenAlaniDataServices;

public interface IMenajerPerformerGuncellenenAlaniDataService
{
    Task<MenajerPerformerGuncellenenAlani> YeniMenajerPerformerGuncellenenAlani(MenajerPerformerGuncellenenAlani model);
    Task<List<MenajerPerformerGuncellenenAlani>> YeniTopluMenajerPerformerGuncellenenAlani(List<MenajerPerformerGuncellenenAlani> model);
    Task<MenajerPerformerGuncellenenAlani> MenajerPerformerGuncellenenAlaniGuncelle(MenajerPerformerGuncellenenAlani model);
    Task<MenajerPerformerGuncellenenAlani> MenajerPerformerGuncellenenAlaniGetir(string id);
    Task<List<MenajerPerformerGuncellenenAlani>> MenajerPerformerGuncellenenAlaniListesiSon1AyGetir(string performerId, string menajerId);
    //Task<List<string>> MenajerGuncellenenPerformerlarIdListesi(string yetenekTemsilcisiId, bool gorulduOlanlariDahilEt, DateTime? eklenmeTarihindenItibaren);
}