using OdiApp.DTOs.PerformerDTOs;
using OdiApp.DTOs.PerformerDTOs.SesRengiDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.PerformerModels.SesRengiModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminSesRengiLogicServices;

public interface IAdminSesRengiLogicService
{
    Task<OdiResponse<SesRengi>> SesRengiOlustur(SesRengi model, OdiUser user);
    Task<OdiResponse<SesRengi>> SesRengiGuncelle(SesRengi model, OdiUser user);
    Task<OdiResponse<SesRengi>> SesRengiDurumDegistir(SesRengiIdDTO model, OdiUser user);
    Task<OdiResponse<List<SesRengi>>> SesRengiListele(DilIdDTO model);
    Task<OdiResponse<SesRengi>> SesRengiGetir(SesRengiIdDTO model);
}