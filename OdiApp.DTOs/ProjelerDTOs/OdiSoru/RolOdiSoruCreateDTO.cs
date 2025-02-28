namespace OdiApp.DTOs.ProjelerDTOs.OdiSoru
{
    public class RolOdiSoruCreateDTO
    {
        public string ProjeRolOdiId { get; set; }
        public string Soru { get; set; }
        public bool CokluSecimSorusu { get; set; }
        public bool CokluCevapIzni { get; set; }
        public List<RolOdiSoruCevapSecenekCreateDTO> CevapSecenekleri { get; set; }
    }
}
