using OdiApp.DTOs.PerformerDTOs.PerformerProfilAlanlariDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.PerformerModels.PerformerProfilModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminPerformerProfilAlanlariLogicServices;

public interface IAdminPerformerProfilAlanlariLogicService
{
    Task<OdiResponse<PerformerProfilAlanlari>> PerformerProfilAlanlariOlustur(PerformerProfilAlanlari model, OdiUser user);
    Task<OdiResponse<PerformerProfilAlanlari>> PerformerProfilAlanlariGuncelle(PerformerProfilAlanlari model, OdiUser user);
    Task<OdiResponse<PerformerProfilAlanlari>> PerformerProfilAlanlariDurumDegistir(PerformerProfilAlanlariIdDTO model, OdiUser user);
    Task<OdiResponse<List<PerformerProfilAlanlari>>> PerformerProfilAlanlariListele(PerformerProfilAlanlariListeleInputDTO model);
}