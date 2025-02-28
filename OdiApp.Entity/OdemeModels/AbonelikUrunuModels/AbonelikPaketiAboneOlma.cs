using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels
{
    public class AbonelikPaketiAboneOlma : StringBaseModel
    {
        public string KullaniciId { get; set; }
        public string KullaniciAbonelikUrunuId { get; set; }
        public int AbonelikTipi { get; set; }
        public string KullaniciAbonelikId { get; set; } //abonelik türüne gör PerformerAbonelikId, YetenektemsilcisiAbonelikId yada YapımAbonelikId
        public string AbonelikReferanceCode { get; set; } // iyzicodan Dönen değerde referenceCode
        public string PlanReferanceCode { get; set; } //iyzicodan Dönen değerde pricingPlanReferenceCode
        public string KullaniciReferanceCode { get; set; } //iyzicodan Dönen değerde customerReferenceCode
        public decimal Fiyat { get; set; }
        public int OdemePeriodu { get; set; }
        public string AbonelikKartlariId { get; set; }
    }
}