namespace OdiApp.DTOs.IslemlerDTOs.OdiIslemler.PerformerOdiDTO
{
    public class PerformerOdiOutputDTO
    {
        public string PerformerOdiId { get; set; }
        public string OdiTalepId { get; set; }
        public bool OdiVideo { get; set; } = false;
        public bool OdiSoru { get; set; } = false;
        public bool OdiSes { get; set; } = false;
        public bool OdiFotograf { get; set; } = false;

        public List<PerformerOdiSoruOutputDTO> PerformerOdiSorular { get; set; }
        public List<PerformerOdiFotografOutputDTO> PerformerOdiFotograflar { get; set; }
        public PerformerOdiSesOutputDTO PerformerOdiSes { get; set; }
        public PerformerOdiVideoOutputDTO PerformerOdiVideo { get; set; }


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

        public List<PerformerOdiTekrarCekOneriOutput>? TekrarCekOneriListesi { get; set; }

        public bool MenajerAktif { get; set; }
        public bool MenajerGizle { get; set; }
        public bool YapimAktif { get; set; }
        public bool YapimGizle { get; set; }
    }
}
