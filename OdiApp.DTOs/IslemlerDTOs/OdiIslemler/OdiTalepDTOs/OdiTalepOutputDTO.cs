namespace OdiApp.DTOs.IslemlerDTOs.OdiIslemler.OdiTalepDTOs
{
    public class OdiTalepOutputDTO
    {
        public string OdiTalepId { get; set; }
        public string ProjeId { get; set; }
        public string ProjeAdi { get; set; }

        public string ProjeRolId { get; set; }
        public string ProjeRolAdi { get; set; }

        public string TalepGonderenId { get; set; }
        public string TalepGonderenAdSoyad { get; set; }
        public string TalepGonderenProfilResmi { get; set; }

        public string TalepGonderilenPerformerId { get; set; }
        public string TalepGonderilenPerformerAdSoyad { get; set; }
        public string TalepGonderilenPerformerProfilResmi { get; set; }

        public string TalepGonderilenMenajerId { get; set; }
        public string TalepGonderilenMenajerAdSoyad { get; set; }
        public string TalepGonderilenMenajerProfilResmi { get; set; }

        //
        public DateTime? OdiTalepTarihi { get; set; }
        //
        public bool MenajerGordu { get; set; }
        public DateTime? MenajerGormeTarihi { get; set; }
        public bool PerformeraIletildi { get; set; }
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

        public bool YapimciOdiIzledi { get; set; }
        public DateTime YapimciOdiIzlediTarihi { get; set; }
    }
}