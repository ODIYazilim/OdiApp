using OdiApp.EntityLayer.IslemlerModels.OdiIslemler;
using System.Threading.Tasks;

namespace OdiApp.DataAccessLayer.IslemlerDataServices.OdiIslemler
{
    public interface IPerformerOdiDataService
    {
        //odi Yükelme
        Task<PerformerOdi> YeniPerformerOdi(PerformerOdi odi);
        Task<List<PerformerOdiSoru>> YeniPerformerOdiSoru(List<PerformerOdiSoru> soru);
        Task<List<PerformerOdiFotograf>> YeniPerformerOdiFotograf(List<PerformerOdiFotograf> fotograflar);
        Task<PerformerOdiSes> YeniPerformerOdiSes(PerformerOdiSes ses);
        Task<PerformerOdiVideo> YeniPerformerOdiVideo(PerformerOdiVideo video);

        //odi güncelleme
        Task<PerformerOdi> PerformerOdiGuncelle(PerformerOdi odi);
        Task<PerformerOdiSoru> PerformerOdiSoruGuncelle(PerformerOdiSoru soru);
        Task<PerformerOdiFotograf> PerformerOdiFotografGuncelle(PerformerOdiFotograf fotograf);
        Task<PerformerOdiSes> PerformerOdiSesGuncelle(PerformerOdiSes ses);
        Task<PerformerOdiVideo> PerformerOdiVideoGuncelle(PerformerOdiVideo video);

        // Odi getirme ve Listeleme
        Task<PerformerOdi> PerformerOdiGetir(string OdiTalepId);
        Task<PerformerOdi> PerformerOdiGetirbyId(string performerOdiId);
        Task<List<PerformerOdiSoru>> PerformerOdiSoruListesi(string performerOdiId);
        Task<List<PerformerOdiFotograf>> PerformerOdiFotografListesi(string performerOdiId);
        Task<PerformerOdiSes> PerformerOdiSesGetir(string performerOdi);
        Task<PerformerOdiVideo> PerformerOdiVideoGetir(string performerOdiId);

        //Tekrar Çekim önerileri

        Task<PerformerOdiTekrarCekOneri> YeniPerformerOdiTekrarCekOnerisi(PerformerOdiTekrarCekOneri oneri);
        Task<List<PerformerOdiTekrarCekOneri>> PerformerOdiTekrarCekOneriListesi(string performerOdiId);


    }
}
