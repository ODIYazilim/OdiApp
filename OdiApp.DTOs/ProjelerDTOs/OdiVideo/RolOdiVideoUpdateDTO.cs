namespace OdiApp.DTOs.ProjelerDTOs.OdiVideo
{
    public class RolOdiVideoUpdateDTO
    {
        public string RolOdiVideoId { get; set; }
        public string ProjeRolOdiId { get; set; }
        public string Baslik { get; set; }

        public List<RolOdiVideoDetayUpdateDTO> DetayList { get; set; }
    }
}
