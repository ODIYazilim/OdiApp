using OdiApp.DTOs.PerformerDTOs.PerformerProfilAlanlariDTOs;
using OdiApp.EntityLayer.PerformerModels.PerformerProfilModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerProfilAlanlariDataServices;

public interface IPerformerProfilAlanlariDataService
{
    Task<PerformerProfilAlanlari> YeniPerformerProfilAlanlari(PerformerProfilAlanlari model);
    Task<PerformerProfilAlanlari> PerformerProfilAlanlariGuncelle(PerformerProfilAlanlari model);
    Task<List<PerformerProfilAlanlari>> PerformerProfilAlanlariListesiGetir(string? kayitTuru);
    Task<PerformerProfilAlanlari> PerformerProfilAlanlariGetir(int id);
    Task<ProfilDolulukOraniOutputDTO> ProfilDolulukOrani(string kullaniciId);
}