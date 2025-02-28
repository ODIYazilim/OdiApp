using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliRolBilgisi
{
    public class KayitliRol : StringBaseModel
    {
        public string KullaniciId { get; set; } //?
        public string RolAdi { get; set; } //
        public string Cinsiyet { get; set; }//
        public string RolTuruKodu { get; set; }//
        public int YasBaslagic { get; set; }//
        public int YasBitis { get; set; }//
        public int Butce60Gun { get; set; }//
        public int PesinButce { get; set; }//
        public int AlternatifButce { get; set; }//
        public bool? CastYasinaGore { get; set; }
        public string? ProjeBilgileri { get; set; }
        public KayitliRolOzellik? RolOzellik { get; set; }
        public List<KayitliRolAnketSorusu>? AnketSorulari { get; set; }
        public string? OdemeSuresi { get; set; }
        public string? RolAgirligi { get; set; }
        public DateTime? CekimBaslangicTarihi { get; set; }
        public DateTime? CekimBitisTarihi { get; set; }
    }
}