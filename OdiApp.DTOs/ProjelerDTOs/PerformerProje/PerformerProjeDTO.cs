using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeYetkilileriDTOs;

namespace OdiApp.DTOs.ProjelerDTOs.PerformerProje
{
    public class PerformerProjeDTO
    {
        public string ProjeId { get; set; }
        public string ProjeAdi { get; set; }
        public string ProjeKapakFotografi { get; set; }
        public string ProjeTuru { get; set; }
        public string Platform { get; set; }
        public int Butce { get; set; }
        public int KalanGun { get; set; }
        public bool Gizli { get; set; }
        public bool Premium { get; set; }
        public bool Acil { get; set; }
        public bool CallbackTalep { get; set; }
        public bool OpsiyonTalep { get; set; }
        public bool OdiTalep { get; set; }

        public List<ProjeYetkiliOutputDTO>? Yetkililer { get; set; }
        public List<ProjeRolWithOdiBilgisiOutput>? Roller { get; set; }
    }
}