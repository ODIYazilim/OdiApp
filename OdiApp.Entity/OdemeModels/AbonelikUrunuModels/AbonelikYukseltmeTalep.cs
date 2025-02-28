using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels
{
    public class AbonelikYukseltmeTalep : StringBaseModel
    {
        public string KullaniciId { get; set; }
        public int AbonelikTipi { get; set; }
        public string MevcutKullaniciAbonelikId { get; set; }
        public string MevcutKullaniciAbonelikUrunId { get; set; }
        public string MevcutKullaniciReferenceCode { get; set; }
        public string MevcutPlanReferenceCode { get; set; }
        public string MevcutAbonelikReferenceCode { get; set; }
        public DateTime MevcutAbonelikBitisTarihi { get; set; }
        public string YukseltilecekAbonelikUrunId { get; set; }
        public string YukseltilecekAbonelikReferansKodu { get; set; }
        public bool Aktif { get; set; }
    }
}