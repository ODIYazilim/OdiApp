using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.PerformerModels.Egitim;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.Egitim;

public interface IAdminEgitimLogicService
{
    Task<OdiResponse<EgitimTipi>> YeniEgitimTipi(EgitimTipi egitimTipi, OdiUser user);
    Task<OdiResponse<Okul>> YeniOkul(Okul okul, OdiUser user);
    Task<OdiResponse<OkulBolum>> YeniOkulBolum(OkulBolum bolum, OdiUser user);
}
