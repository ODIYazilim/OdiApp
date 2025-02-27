namespace OdiApp.DTOs.IslemlerDTOs.OdiIslemler.PerformerOdiDTO
{
    public class MenajerRolOdiListItemOutputDTO
    {
        public string ProjeRolId { get; set; }
        public string PerformerId { get; set; }
        public string PerformerAdSoyad { get; set; }
        public string PerformerProfilFotografi { get; set; }


        public string OdiListId { get; set; }

        public bool Pasif { get; set; } = false;
        public bool Gizle { get; set; } = false;
        public PerformerOdiOutputDTO PerformerOdi { get; set; }
    }
}
