namespace OdiApp.DTOs.ProjelerDTOs.OdiFotograf
{
    public class RolOdiFotoPozUpdateListDTO
    {
        public string ProjeRolOdiId { get; set; }
        public List<RolOdiFotoPozUpdateDTO> pozListesi { get; set; }

        public List<RolOdiFotoPozCreateDTO> yeniPozListesi { get; set; }
    }
}
