using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.BildirimModels.SmsAyarlariModels;

namespace OdiApp.BusinessLayer.Services.BildirimLogicServices.MutluCellSmsLogicServices
{
    public interface IMutluCellSmsLogicService
    {
        Task<OdiResponse<MutluCellSmsAyarlari>> AyarlariGuncelle(MutluCellSmsAyarlari model, OdiUser user);
        Task<OdiResponse<MutluCellSmsAyarlari>> AyarlariGetir();
    }
}