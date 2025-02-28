using OdiApp.DTOs.ProjelerDTOs.ProjeRolBilgileri.ProjeRolDTO;

namespace OdiApp.DTOs.ProjelerDTOs.PerformerProje
{
    public class ProjeRolWithOdiBilgisiOutput
    {
        public RolOdiBilgisiDTO RolOdiBilgisi { get; set; }
        public ProjeRolOutputDTO Rol { get; set; }
        public string OpsiyonId { get; set; } = "";

    }
}
