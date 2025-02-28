namespace OdiApp.DTOs.ProjelerDTOs.OdiVideo
{
    public class RolOdiVideoCreateDTO
    {
        public string ProjeRolOdiId { get; set; }
        public string Baslik { get; set; }

        public List<RolOdiVideoDetayCreateDTO> DetayList { get; set; }
    }
}