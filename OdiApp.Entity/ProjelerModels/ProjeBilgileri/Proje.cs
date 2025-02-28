using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.ProjelerModels.ProjeBilgileri
{
    [Table("Projeler")]
    public class Proje : StringBaseModel
    {
        public string? Adi { get; set; }
        public int DilId { get; set; }
        public string? FotografAdi { get; set; }
        public string? Fotograf { get; set; }
        public string? ProjeTurKodu { get; set; }
        public string? ProjeAciklama { get; set; }
        public DateTime? CekimBaslangicTarihi { get; set; }
        public DateTime? CekimBitisTarihi { get; set; }
        public int Butce { get; set; }
        public DateTime? PrePpmTarihi { get; set; }
        public DateTime? PpmTarihi { get; set; }
        public string? Sehirler { get; set; }
        public string? YasakliKelimeler { get; set; }

        public bool? Gizli { get; set; }
        public bool? Premium { get; set; }
        public bool? Acil { get; set; }
        public bool? Durum { get; set; }

        public string? YapimciFirmaKodu { get; set; }
        public string? YapimciFirmaAdi { get; set; }

        public DateTime? SonOdilemeTarihi { get; set; } = DateTime.Now;

        [NotMapped]
        public ProjeTuru ProjeTuru { get; set; }
        public List<ProjeYetkili>? Yetkililer { get; set; }
        //public List<RolBilgisi> Roller { get; set; }
    }
}