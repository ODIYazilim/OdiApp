using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.BildirimModels.Mesajlasma
{
    public class MesajDetay : StringBaseModel
    {
        public string MesajId { get; set; }
        public string GonderenKullaniciId { get; set; }
        public string GonderilenKullaniciId { get; set; }
        public DateTime MesajGonderimTarihi { get; set; }
        public string TextMesaj { get; set; } = "";
        public string DosyaMesaj { get; set; } = "";
        public bool MesajDosyami { get; set; } = false;
        public string DosyaTipi { get; set; } = "";
        public bool Okundu { get; set; } = false;
    }
}