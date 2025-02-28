namespace OdiApp.DTOs.ProjelerDTOs.OdiFotograf
{
    public class RolOdiFotoOrnekFotografUpdateListDTO
    {
        public string ProjeRolOdiId { get; set; }
        public List<RolOdiFotoOrnekFotografUpdateDTO> OrnekFotograflar { get; set; }
        public List<RolOdiFotoOrnekFotografCreateDTO> YeniOrnekFotograflar { get; set; }
    }
}
