namespace OdiApp.DTOs.IslemlerDTOs.OpsiyonIslemler
{
    public class OpsiyonYanitlaDetayDTO
    {
        public string OpsiyonId { get; set; }

        public bool TumGunlerMusaitim { get; set; } = false;
        public bool MusaitOlmadigimGunlerVar { get; set; } = false;
        public List<DateTime>? MusaitOlmadigimGunler { get; set; }
        public int PerformerProjeButcesi { get; set; }
        public bool ProjeButcesiDegistirildi { get; set; }

        public int PerformerOdemeSuresi { get; set; }
        public bool OdemeSuresiDegistirildi { get; set; }

        public bool YanitlayanMenajer { get; set; }
        public bool YanitlayanPerformer { get; set; }
    }
}
