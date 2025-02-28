namespace OdiApp.DTOs.ProjelerDTOs.OdiSoru
{
    public class RolOdiSorumatikDTO
    {
        public string ProjRolOdiId { get; set; }
        public List<RolOdiSoruOutputDTO> SoruList { get; set; }
        public RolOdiSoruAciklamaOutputDTO Aciklama { get; set; }
    }
}
