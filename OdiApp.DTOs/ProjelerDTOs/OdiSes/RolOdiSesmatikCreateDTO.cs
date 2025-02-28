namespace OdiApp.DTOs.ProjelerDTOs.OdiSes
{
    public class RolOdiSesmatikCreateDTO
    {
        public string ProjeRolOdiId { get; set; }
        public List<RolOdiSesCreateDTO> SesList { get; set; }
        public RolOdiSesSenaryoCreateDTO Senaryo { get; set; }
        public RolOdiSesYonetmenNotuCreateDTO YonetmenNotu { get; set; }
    }
}
