namespace OdiApp.DTOs.IslemlerDTOs.OpsiyonIslemler
{
    public class OpsiyonOutputDTO
    {
        public string OpsiyonId { get; set; }
        public string OpsiyonListesiId { get; set; }
        public string OpsiyonGonderenId { get; set; }
        public string ProjeId { get; set; }
        public string ProjeAdi { get; set; }
        public string ProjeRolId { get; set; }
        public string ProjeRolAdi { get; set; }
        public string MenajerId { get; set; }
        public string MenajerAdi { get; set; }
        public string PerformerId { get; set; }
        public string PerformerAdi { get; set; }
        public string PerformerProfilFotografi { get; set; }
        public DateTime OpsiyonGonderimTarihi { get; set; }


        public DateTime CekimBaslagicTarihi { get; set; }
        public DateTime CekimBitisTarihi { get; set; }
        public bool TumGunlerMusaitim { get; set; } = false;
        public bool MusaitOlmadigimGunlerVar { get; set; } = false;
        public List<DateTime>? MusaitOlmadigimGunler { get; set; }

        public int ProjeButcesi { get; set; }
        public int PerformerProjeButcesi { get; set; }
        public bool ProjeButcesiDegistirildi { get; set; }

        public int OdemeSuresi { get; set; }
        public int PerformerOdemeSuresi { get; set; }
        public bool OdemeSuresiDegistirildi { get; set; }

        public bool MenajerInceledi { get; set; }
        public bool PerformeraIletildi { get; set; }
        public DateTime PerformeraIletimTarihi { get; set; }
        public bool PerformerInceledi { get; set; }

        public bool GeriCevrildi { get; set; }
        public bool Onaylandi { get; set; }

        public bool YanitlayanMenajer { get; set; }
        public bool YanitlayanPerformer { get; set; }
        public bool Tamamlandi { get; set; }
        public DateTime TamamlanmaTarihi { get; set; }

        public List<OpsiyonAnketSorulariOutputDTO> AnketSorulari { get; set; }
    }
}
