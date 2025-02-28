namespace OdiApp.DTOs.ProjelerDTOs.OdiVideo
{
    public class RolOdiVideoOutputDTO
    {
        public string RolOdiVideoId { get; set; }
        public string ProjeRolOdiId { get; set; }
        public string Baslik { get; set; }

        public List<RolOdiVideoDetayOutputDTO> DetayList { get; set; }
    }
}
