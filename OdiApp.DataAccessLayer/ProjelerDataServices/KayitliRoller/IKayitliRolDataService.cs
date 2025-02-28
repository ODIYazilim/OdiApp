using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiFotograf;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiSes;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiSoru;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiVideo;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliRolBilgisi;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliRolOdi;

namespace OdiApp.DataAccessLayer.ProjelerDataServices.KayitliRoller
{
    public interface IKayitliRolDataService
    {
        Task<KayitliRolOdiFotoOrnekFotograf> YeniKayitliRolOdiFotoOrnekFotograf(KayitliRolOdiFotoOrnekFotograf model);
        Task<List<KayitliRolOdiFotoOrnekFotograf>> YeniKayitliRolOdiFotoOrnekFotograf(List<KayitliRolOdiFotoOrnekFotograf> model);
        Task<KayitliRolOdiFotoPoz> YeniKayitliRolOdiFotoPoz(KayitliRolOdiFotoPoz model);
        Task<List<KayitliRolOdiFotoPoz>> YeniKayitliRolOdiFotoPoz(List<KayitliRolOdiFotoPoz> model);
        Task<KayitliRolOdiFotoYonetmenNotu> YeniKayitliRolOdiFotoYonetmenNotu(KayitliRolOdiFotoYonetmenNotu model);
        Task<List<KayitliRolOdiFotoYonetmenNotu>> YeniKayitliRolOdiFotoYonetmenNotu(List<KayitliRolOdiFotoYonetmenNotu> model);
        Task<KayitliRolOdiSes> YeniKayitliRolOdiSes(KayitliRolOdiSes model);
        Task<List<KayitliRolOdiSes>> YeniKayitliRolOdiSes(List<KayitliRolOdiSes> model);
        Task<KayitliRolOdiSesYonetmenNotu> YeniKayitliRolOdiSesYonetmenNotu(KayitliRolOdiSesYonetmenNotu model);
        Task<List<KayitliRolOdiSesYonetmenNotu>> YeniKayitliRolOdiSesYonetmenNotu(List<KayitliRolOdiSesYonetmenNotu> model);
        Task<KayitliRolOdiSesSenaryo> YeniKayitliRolOdiSesSenaryo(KayitliRolOdiSesSenaryo model);
        Task<List<KayitliRolOdiSesSenaryo>> YeniKayitliRolOdiSesSenaryo(List<KayitliRolOdiSesSenaryo> model);
        Task<KayitliRolOdiSoru> YeniKayitliRolOdiSoru(KayitliRolOdiSoru model);
        Task<List<KayitliRolOdiSoru>> YeniKayitliRolOdiSoru(List<KayitliRolOdiSoru> model);
        Task<KayitliRolOdiSoruAciklama> YeniKayitliRolOdiSoruAciklama(KayitliRolOdiSoruAciklama model);
        Task<List<KayitliRolOdiSoruAciklama>> YeniKayitliRolOdiSoruAciklama(List<KayitliRolOdiSoruAciklama> model);
        Task<KayitliRolOdiSoruCevapSecenek> YeniKayitliRolOdiSoruCevapSecenek(KayitliRolOdiSoruCevapSecenek model);
        Task<KayitliRolOdiVideo> YeniKayitliRolOdiVideo(KayitliRolOdiVideo model);
        Task<List<KayitliRolOdiVideo>> YeniKayitliRolOdiVideo(List<KayitliRolOdiVideo> model);
        Task<KayitliRolOdiVideoDetay> YeniKayitliRolOdiVideoDetay(KayitliRolOdiVideoDetay model);
        Task<KayitliRolOdiVideoOrnekOyun> YeniKayitliRolOdiVideoOrnekOyun(KayitliRolOdiVideoOrnekOyun model);
        Task<List<KayitliRolOdiVideoOrnekOyun>> YeniKayitliRolOdiVideoOrnekOyun(List<KayitliRolOdiVideoOrnekOyun> model);
        Task<KayitliRolOdiVideoSenaryo> YeniKayitliRolOdiVideoSenaryo(KayitliRolOdiVideoSenaryo model);
        Task<List<KayitliRolOdiVideoSenaryo>> YeniKayitliRolOdiVideoSenaryo(List<KayitliRolOdiVideoSenaryo> model);
        Task<KayitliRolOdiVideoYonetmenNotu> YeniKayitliRolOdiVideoYonetmenNotu(KayitliRolOdiVideoYonetmenNotu model);
        Task<List<KayitliRolOdiVideoYonetmenNotu>> YeniKayitliRolOdiVideoYonetmenNotu(List<KayitliRolOdiVideoYonetmenNotu> model);
        Task<KayitliRol> YeniKayitliRol(KayitliRol model);
        Task<KayitliRolAnketSorusu> YeniKayitliRolAnketSorusu(KayitliRolAnketSorusu model);
        Task<KayitliRolOzellik> YeniKayitliRolOzellik(KayitliRolOzellik model);
        Task<KayitliRolOdi> YeniKayitliRolOdi(KayitliRolOdi model);
    }
}