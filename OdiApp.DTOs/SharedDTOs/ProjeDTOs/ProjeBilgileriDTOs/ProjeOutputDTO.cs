using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeYetkilileriDTOs;
using System;
using System.Collections.Generic;

namespace OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeBilgileriDTOs
{
    public class ProjeOutputDTO
    {
        public string ProjeId { get; set; }
        public string Adi { get; set; }
        public string FotografAdi { get; set; }
        public string Fotograf { get; set; }
        public string ProjeTurKodu { get; set; }
        public string ProjeTuru { get; set; }
        public string ProjeAciklama { get; set; }
        public DateTime? CekimBaslangicTarihi { get; set; }
        public DateTime? CekimBitisTarihi { get; set; }
        public int Butce { get; set; }
        public DateTime? PrePpmTarihi { get; set; }
        public DateTime? PpmTarihi { get; set; }
        public string Sehirler { get; set; }
        public string YasakliKelimeler { get; set; }

        public string YapimciFirmaKodu { get; set; }
        public string YapimciFirmaAdi { get; set; }

        public bool? Gizli { get; set; }
        public bool? Premium { get; set; }
        public bool? Acil { get; set; }
        public bool? Durum { get; set; }

        public DateTime? SonOdilemeTarihi { get; set; } = DateTime.Now; // eğer ilk defa odi ekleniyorsa bu tarih hem rolodi tablosuna hem de proje  tablosuna eklenir
        public List<ProjeYetkiliOutputDTO> Yetkililer { get; set; }
        //public List<ProjeRolOutputDTO>? Roller { get; set; }
    }
}