namespace OdiApp.DTOs.ProjelerDTOs.OdiVideo
{
    public class RolOdiVideomatikCreateDTO
    {
        public string ProjeRolOdiId { get; set; }
        public RolOdiVideoCreateDTO Video { get; set; }
        public List<RolOdiVideoOrnekOyunCreateDTO> OrnekOyunList { get; set; }
        public RolOdiVideoSenaryoCreateDTO Senaryo { get; set; }
        public RolOdiVideoYonetmenNotuCreateDTO YonetmenNotu { get; set; }
    }
}
