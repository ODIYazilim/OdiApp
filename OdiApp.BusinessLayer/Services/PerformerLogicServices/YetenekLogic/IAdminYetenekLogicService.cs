using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.PerformerModels.YetenekModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.YetenekLogic;

public interface IAdminYetenekLogicService
{
    Task<OdiResponse<YetenekTipi>> YeniYetenekTipi(YetenekTipi yetenekTipi, OdiUser user);
    Task<OdiResponse<Yetenek>> YeniYetenek(Yetenek yetenek, OdiUser user);
}
