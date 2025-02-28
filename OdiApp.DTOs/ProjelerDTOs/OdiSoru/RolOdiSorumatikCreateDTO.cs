namespace OdiApp.DTOs.ProjelerDTOs.OdiSoru
{
    public class RolOdiSorumatikCreateDTO
    {
        public string ProjeRolOdiId { get; set; }
        public List<RolOdiSoruCreateDTO> SoruList { get; set; }
        public RolOdiSoruAciklamaCreateDTO Aciklama { get; set; }
    }
}
