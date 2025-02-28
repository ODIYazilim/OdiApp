namespace OdiApp.DTOs.ProjelerDTOs.OdiVideo
{
    public class RolOdiVideomatikDTO
    {
        public RolOdiVideoOutputDTO Video { get; set; }
        public List<RolOdiVideoOrnekOyunOutputDTO> OrnekOyun { get; set; }
        public RolOdiVideoSenaryoOutputDTO Senaryo { get; set; }
        public RolOdiVideoYonetmenNotuOutputDTO YonetmenNotu { get; set; }
    }
}
