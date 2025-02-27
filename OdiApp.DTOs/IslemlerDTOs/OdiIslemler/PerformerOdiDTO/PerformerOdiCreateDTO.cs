namespace OdiApp.DTOs.IslemlerDTOs.OdiIslemler.PerformerOdiDTO
{
    public class PerformerOdiCreateDTO
    {
        public string OdiTalepId { get; set; }
        public bool OdiVideo { get; set; } = false;
        public bool OdiSoru { get; set; } = false;
        public bool OdiSes { get; set; } = false;
        public bool OdiFotograf { get; set; } = false;

        public List<PerformerOdiSoruCreateDTO> PerformerOdiSorular { get; set; }
        public List<PerformerOdiFotografCreateDTO> PerformerOdiFotograflar { get; set; }
        public PerformerOdiSesCreateDTO PerformerOdiSes { get; set; }
        public PerformerOdiVideoCreateDTO PerformerOdiVideo { get; set; }
    }
}