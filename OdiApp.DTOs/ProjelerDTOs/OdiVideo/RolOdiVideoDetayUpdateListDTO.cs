namespace OdiApp.DTOs.ProjelerDTOs.OdiVideo
{
    public class RolOdiVideoDetayUpdateListDTO
    {
        public string ProjeRolOdiId { get; set; }
        public string RolOdiVideoId { get; set; }
        public List<RolOdiVideoDetayUpdateDTO> VideoDetayList { get; set; }
        public List<RolOdiVideoDetayUpdateDTO> YeniVideoDetayList { get; set; }
    }
}
