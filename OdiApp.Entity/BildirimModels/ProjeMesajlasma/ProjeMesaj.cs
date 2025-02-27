using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.BildirimModels.ProjeMesajlasma
{
    public class ProjeMesaj : StringBaseModel
    {
        public string Kullanici1Id { get; set; }
        public string Kullanici2Id { get; set; }
        public string PerformerId { get; set; }
        public string ProjeId { get; set; }
        public string ProjeAdi { get; set; }
        public string ProjeResmi { get; set; }
        public string ProjeRolId { get; set; }
        public string ProjeRolAdi { get; set; }
        public List<ProjeMesajDetay>? ProjeMesajDetaylar { get; set; }
    }
}