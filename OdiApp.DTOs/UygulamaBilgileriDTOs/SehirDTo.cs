namespace OdiApp.DTOs.UygulamaBilgileriDTOs
{
    public class SehirDTo
    {
        public int SehirId { get; set; }
        public string SehirAdi { get; set; }

        public List<IlceDTO> Ilceler { get; set; }
    }
}
