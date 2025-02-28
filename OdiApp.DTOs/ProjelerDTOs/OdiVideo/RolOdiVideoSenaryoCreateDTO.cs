using System.Security.Principal;

namespace OdiApp.DTOs.ProjelerDTOs.OdiVideo
{
    public class RolOdiVideoSenaryoCreateDTO
    {
        public string ProjeRolOdiId { get; set; }
        public string SenaryoAdi { get; set; }
        public string Senaryo { get; set; } //html formatında
        public bool Dosyami { get; set; }
    }
}
