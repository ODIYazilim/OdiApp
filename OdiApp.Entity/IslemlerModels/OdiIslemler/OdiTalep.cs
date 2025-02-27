using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.IslemlerModels.OdiIslemler
{
    public class OdiTalep : StringBaseModel
    {
        public string ProjeId { get; set; }
        public string ProjeAdi { get; set; }
        public string ProjeRolId { get; set; }
        public string ProjeRolAdi { get; set; }
        public string TalepGonderenId { get; set; }
        public string TalepGonderilenPerformerId { get; set; }
        public string TalepGonderilenMenajerId { get; set; }

        //
        public DateTime? OdiTalepTarihi { get; set; }
        //
        public bool MenajerGordu { get; set; }
        public DateTime? MenajerGormeTarihi { get; set; }
        public bool PerformeraIletildi { get; set; }
        public string? MenajerOnerisi { get; set; }
        public DateTime? PerformeraIletildiTarihi { get; set; }
        public bool PerformerGordu { get; set; }
        public DateTime? PerformerGorduTarihi { get; set; }
        public bool OdiYuklendi { get; set; }
        public DateTime? OdiYuklendiTarihi { get; set; }
        public bool MenajerOdiOnayi { get; set; }
        public DateTime? MenajerOdiOnayTarihi { get; set; }

        //
        public bool MenajerTalepRed { get; set; }
        public string? MenajerTalepRedSebebi { get; set; }
        public DateTime? MenajerTalepRedTarihi { get; set; }

        //
        public bool PerformerTalepRed { get; set; }
        public string? PerformerTalepRedSebebi { get; set; }
        public DateTime? PerformerTalepRedTarihi { get; set; }

        public bool MenajerRedOnayi { get; set; }
        public DateTime? MenajerRedOnayiTarihi { get; set; }
        public bool TalepKapadi { get; set; }
    }
}