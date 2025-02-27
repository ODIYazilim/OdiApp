namespace OdiApp.DTOs.IslemlerDTOs.PerformerIslemler
{
    public class PerformerIslemDTO
    {
        public string PerformerId { get; set; }
        public string ProjeId { get; set; }
        public List<RolOdiBilgisiDTO> RolOdiBilgileri { get; set; }
        public List<RolOpsiyonBilgisiDTO> RolOpsiyonBilgileri { get; set; }
        public bool OdiTalep { get; set; }

        public bool Opsiyon { get; set; }
        public bool Callback { get; set; }


    }
}
