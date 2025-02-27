using OdiApp.EntityLayer.BildirimModels.SmsAyarlariModels;

namespace OdiApp.DataAccessLayer.BildirimDataServices.MutluCellSmsDataServices
{
    public interface IMutluCellSmsDataService
    {
        Task<MutluCellSmsAyarlari> AyarlariGetir();
        Task<MutluCellSmsAyarlari> AyarlariGuncelle(MutluCellSmsAyarlari model);
    }
}