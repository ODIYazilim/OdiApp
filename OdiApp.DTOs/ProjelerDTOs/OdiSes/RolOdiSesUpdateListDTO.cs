namespace OdiApp.DTOs.ProjelerDTOs.OdiSes
{
    public class RolOdiSesUpdateListDTO
    {
        public string ProjeRolOdiId { get; set; }
        public List<RolOdiSesUpdateDTO> sesList { get; set; }
        public List<RolOdiSesCreateDTO> yeniSesList { get; set; }
    }
}
