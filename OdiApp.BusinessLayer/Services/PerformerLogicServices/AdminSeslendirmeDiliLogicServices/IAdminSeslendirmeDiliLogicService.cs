using OdiApp.DTOs.PerformerDTOs;
using OdiApp.DTOs.PerformerDTOs.SeslendirmeDiliDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.PerformerModels.SeslendirmeDiliModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminSeslendirmeDiliLogicServices;

public interface IAdminSeslendirmeDiliLogicService
{
    Task<OdiResponse<SeslendirmeDili>> SeslendirmeDiliOlustur(SeslendirmeDili model, OdiUser user);
    Task<OdiResponse<SeslendirmeDili>> SeslendirmeDiliGuncelle(SeslendirmeDili model, OdiUser user);
    Task<OdiResponse<SeslendirmeDili>> SeslendirmeDiliDurumDegistir(SeslendirmeDiliIdDTO model, OdiUser user);
    Task<OdiResponse<List<SeslendirmeDili>>> SeslendirmeDiliListele(DilIdDTO model);
    Task<OdiResponse<SeslendirmeDili>> SeslendirmeDiliGetir(SeslendirmeDiliIdDTO model);
}