namespace OdiApp.DTOs.IslemlerDTOs.CallbackIslemler
{
    public class CallbackGonderilecekPerformerOutput
    {
        public string OpsiyonId { get; set; }
        public string ProjeId { get; set; }
        public string ProjeAdi { get; set; }
        public string ProjeRolId { get; set; }
        public string ProjeRolAdi { get; set; }
        public string PerformerId { get; set; }
        public string PerformerAdi { get; set; }
        public string PerformerProfilFotografi { get; set; }

        public string MenajerId { get; set; }
        public string MenajerAdi { get; set; }

        public bool ProjeButcesiDegistirildi { get; set; }
        public bool OdemeSuresiDegistirildi { get; set; }
        public bool Onaylandi { get; set; }
        public bool Tamamlandi { get; set; }
        public bool CallbackGonderildi { get; set; }
    }
}
