using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliRolBilgisi
{
    public class KayitliRolOzellik : StringBaseModel
    {
        public string KayitliRolId { get; set; }
        public string? Sehirler { get; set; }
        public string? Uyruk { get; set; }
        public int MaxBoy { get; set; }
        public int MinBoy { get; set; }
        public int MaxKilo { get; set; }
        public int MinKilo { get; set; }
        /// <summary>
        /// Fiziksel Özellikler
        /// 2 olanlar olsada olur, diğerleri mutlaka olması gerekenler
        /// </summary>
        public string? EgitimTipleri { get; set; }
        public string? DeneyimTipleri { get; set; }
        public string? BedenTipleri { get; set; }
        public string? GozeRenkleri { get; set; }
        public string? SacRenkleri { get; set; }
        public string? SacSekilleri { get; set; }
        public string? SakaSekilleri { get; set; }
        public string? TenRenkleri { get; set; }

        //olsada olur
        public string? BedenTipleri2 { get; set; }
        public string? GozeRenkleri2 { get; set; }
        public string? SacRenkleri2 { get; set; }
        public string? SacSekilleri2 { get; set; }
        public string? SakaSekilleri2 { get; set; }
        public string? TenRenkleri2 { get; set; }
    }
}