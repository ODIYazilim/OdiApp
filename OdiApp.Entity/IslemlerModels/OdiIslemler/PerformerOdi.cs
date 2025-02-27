using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.IslemlerModels.OdiIslemler
{
    [Table("PerformerOdi")]
    public class PerformerOdi : StringBaseModel
    {
        public string OdiTalepId { get; set; }
        public bool OdiVideo { get; set; } = false;
        public bool OdiSoru { get; set; } = false;
        public bool OdiSes { get; set; } = false;
        public bool OdiFotograf { get; set; } = false;

        public List<PerformerOdiSoru> PerformerOdiSorular { get; set; }
        public List<PerformerOdiFotograf> PerformerOdiFotograflar { get; set; }
        public PerformerOdiSes PerformerOdiSes { get; set; }
        public PerformerOdiVideo PerformerOdiVideo { get; set; }

        public bool MenajerOnayinaGonder { get; set; }
        public DateTime MenajerOnayinaGonderTarih { get; set; }
        public bool MenajerInceledi { get; set; }
        public DateTime? MenajerIncelediTarih { get; set; }
        public bool TekrarCekTalebi { get; set; }
        public string? SonTekrarCekTalebiMenajerOnerisi { get; set; }
        public DateTime? SonTekrarCekTalepTarihi { get; set; }
        public bool PerformerSonTekrarCekti { get; set; }
        public DateTime PerformerSonTekrarCektiTarihi { get; set; }
        public bool MenajerOnayi { get; set; }
        public DateTime? MenajerOnayTarihi { get; set; }
        public bool YapimInceledi { get; set; }
        public DateTime? YapimIncelediTarih { get; set; }

        public List<PerformerOdiTekrarCekOneri>? TekrarCekOneriListesi { get; set; }

        public bool MenajerAktif { get; set; }
        public bool MenajerGizle { get; set; }
        public bool YapimAktif { get; set; }
        public bool YapimGizle { get; set; }
    }
}