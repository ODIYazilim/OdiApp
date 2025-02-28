namespace OdiApp.DTOs.ProjelerDTOs.OdiSoru
{
    public class RolOdiSoruOutputDTO
    {
        public string RolOdiSoruId { get; set; }
        public string ProjeRolOdiId { get; set; }
        public string Soru { get; set; }
        public bool CokluSecimSorusu { get; set; }
        public bool CokluCevapIzni { get; set; }
        public List<RolOdiSoruCevapSecenekOutputDTO> CevapSecenekleri { get; set; }
    }
}
