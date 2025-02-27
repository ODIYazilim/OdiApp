namespace OdiApp.DTOs.IslemlerDTOs.OdiIslemler.PerformerOdiDTO
{
    public class PerformerOdiUpdateDTO
    {
        public string PerformerOdiId { get; set; }
        public string OdiTalepId { get; set; }
        public bool OdiVideo { get; set; } = false;
        public bool OdiSoru { get; set; } = false;
        public bool OdiSes { get; set; } = false;
        public bool OdiFotograf { get; set; } = false;

        public List<PerformerOdiSoruUpdateDTO> PerformerOdiSorular { get; set; }
        public List<PerformerOdiFotografUpdateDTO> PerformerOdiFotograflar { get; set; }
        public PerformerOdiSesUpdateDTO PerformerOdiSes { get; set; }
        public PerformerOdiVideoUpdateDTO PerformerOdiVideo { get; set; }
    }
}