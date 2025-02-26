using System.Collections.Generic;

namespace OdiApp.DTOs.SharedDTOs.PerformerDTOs.CVDTOs
{
    public class FiltrelenenPerformerlarInputDTO
    {
        public List<RangeFilter>? RangeFilters { get; set; } // Min-Max aralıklı filtreler için
        public List<MultiSelectFilter>? MultiSelectFilters { get; set; } // Çoktan seçmeli filtreler için
        public List<string>? DeneyimKoduList { get; set; } // Deneyim filtreleri için
        public List<int>? EgitimTipiIdList { get; set; } // Eğitim filtreleri için
        public List<YetenekFilter>? YetenekFilters { get; set; } // Yetenek filtreleri için
        public List<string>? PerformerEtiketKoduList { get; set; } // Performer etiketleri için
        public List<string>? KayitTuruKoduList { get; set; } // Kayıt türü kodları için
        public PuanFilter? PuanFilter { get; set; } // Puan filtrelemeleri için
        public YasakliKelimelerFilter? YasakliKelimelerFilter { get; set; } // Yasaklı kelimeler filtresi için
        public YasFilter? YasFilter { get; set; } // Yasaklı kelimeler filtresi için
        public List<ProfilTipiFilterType>? ProfilTipiFilter { get; set; } //Profil tipine göre filtreleme için
        public List<IcerigeGoreFilterType> IcerigeGoreFilter { get; set; } //Örn, Tanıtım videosu olanlar, mimik videosu olanlar
        public List<YineDeGosterFilterType> YineDeGosterFilter { get; set; } //Örn, müsait olmayanlar, yasaklı kelimeye takılanlar
        public List<string>? YetenekTemsilcisiFilter { get; set; } //Menajer idsine göre filtreleme
        public string? ProjeId { get; set; } // Yine de Göster filtreleri için proje rol'e ihtiyaç olduğundan gerekli parametre
        public int Page { get; set; } = 1; // Sayfa numarası
        public int Limit { get; set; } = 10; // Sayfa başına kayıt limiti
    }

    public class RangeFilter
    {
        public string AlanKodu { get; set; } = string.Empty; // Alan kodu örn. BOYU, KILO
        public int? Min { get; set; } // Minimum değer
        public int? Max { get; set; } // Maksimum değer
    }

    public class YasFilter
    {
        public int? MinYas { get; set; }
        public int? MaxYas { get; set; }
        public bool CastYasinaGoreUygula { get; set; }
        //public DateTime? MinDogumTarihi { get; set; } // Minimum doğum tarihi değeri
        //public DateTime? MaxDogumTarihi { get; set; } // Maksimum doğum tarihi değeri
    }

    public class YasakliKelimelerFilter
    {
        public bool ReklamMi { get; set; }
        public List<string>? YasakliKelimelerList { get; set; }
    }

    public class PuanFilter
    {
        public int? GenelPuanMin { get; set; }
        public int? GenelPuanMax { get; set; }
        public int? İlgiCekicilikPuanMin { get; set; }
        public int? İlgiCekicilikPuanMax { get; set; }
        public int? YetenekPuanMin { get; set; }
        public int? YetenekPuanMax { get; set; }
        public int? BasariPuanMin { get; set; }
        public int? BasariPuanMax { get; set; }
    }

    public class MultiSelectFilter
    {
        public string AlanKodu { get; set; } = string.Empty; // Alan kodu örn. YSHR
        public List<string> Values { get; set; } = new List<string>(); // Seçilen değerler
    }

    public class YetenekFilter
    {
        public string YetenekKodu { get; set; } = string.Empty; // Yetenek kodu örn. ALMC
        public int MinDerece { get; set; } = 0; // Minimum derece
    }

    public enum ProfilTipiFilterType
    {
        None = 0,
        EksikProfil = 1,
        OnayliProfil = 2,
        PremiumProfil = 3
    }

    public enum IcerigeGoreFilterType
    {
        None = 0,
        TanitimVideosuOlanlar = 1,
        MimikVideosuOlanlar = 2,
        ShowreeliOlanlar = 3,
        PortreKolajiOlanlar = 4,
    }

    public enum YineDeGosterFilterType
    {
        None = 0,
        MusaitOlmayanlar = 1,
        YasakliKelimeyeTakilanlar = 2,
        ButceUstuOlanlar = 3,
        KatilimSehrineUymayanlar = 4,
    }
}