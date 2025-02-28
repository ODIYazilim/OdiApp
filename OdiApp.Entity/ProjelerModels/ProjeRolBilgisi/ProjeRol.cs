using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.ProjelerModels.ProjeRolBilgisi
{
    [Table("ProjeRolleri")]
    public class ProjeRol : StringBaseModel
    {
        public string ProjeId { get; set; }
        public string RolAdi { get; set; }
        public string Cinsiyet { get; set; }
        public string RolTuruKodu { get; set; }
        public int YasBaslangic { get; set; }
        public int YasBitis { get; set; }
        public int? Butce60Gun { get; set; }
        public int PesinButce { get; set; }
        public int? AlternatifButce { get; set; }
        public bool? CastYasinaGore { get; set; }
        public string? ProjeBilgileri { get; set; }
        public ProjeRolOzellik? RolOzellik { get; set; }
        public List<ProjeRolAnketSorusu>? AnketSorulari { get; set; }
        public string? OdemeSuresi { get; set; }
        public string? RolAgirligi { get; set; }
        public string? RolAgirlikKodu { get; set; }
        public DateTime? CekimBaslangicTarihi { get; set; }
        public DateTime? CekimBitisTarihi { get; set; }
    }
}