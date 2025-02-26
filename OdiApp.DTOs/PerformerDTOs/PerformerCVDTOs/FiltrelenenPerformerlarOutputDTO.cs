namespace OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs;

public class FiltrelenenPerformerlarOutputDTO
{
    public List<FiltrelenenPerformerlarFilterItem> Filtreler { get; set; }
}

public class FiltrelenenPerformerlarFilterItem
{
    public string FiltreBasligi { get; set; } //Filtrenin adı. Dile göre değişir.
    public PerformerFilterType FilterType { get; set; } //Filtrenin tipi. Filtreleme input'u doldurulurken dikkate alınacak.
    public string AlanKodu { get; set; }
    public List<MultiSelectFilterItem>? FilterItems { get; set; } //Filtre tipine göre filtreleme için kullanıclacak filtre seçenekleri. Örn Cinsiyet için Erkek, Kadın gibi
    public bool DetayliFiltre { get; set; } = false;
}

public class MultiSelectFilterItem
{
    public int? IntDeger { get; set; } //Değerin idsi veya enum değeri. Filtrelerken gönderilir.
    public string? StringDeger { get; set; } //Değerin kodu veya string IDsi. Filtrelerken gönderilir.
    public string Adi { get; set; } //Kullanıcıya gösterilecek ad. Örn; Cinsiyet için Erkek, Kadın gibi
    public bool GrupMu { get; set; } = false;//Gruplanmış filtre seçeneği olup olmadığını belirler.
    public List<MultiSelectFilterItem>? AltItemlar { get; set; } //Altındaki itemleri temsil eder. Örn; Dans altında Salsa, Tango gibi
}

public enum PerformerFilterType
{
    NotSet = 0,
    RangeFilter = 1,
    MultiSelectFilter = 2,
    DeneyimFilter = 3,
    EgitimFilter = 4,
    YetenekFilter = 5,
    PerformerEtiketFilter = 6,
    KayitTuruFilter = 7,
    PuanFilter = 8,
    YasakliKelimelerFilter = 9,
    YasFilter = 10,
    ProfilTipiFilter = 11,
    IcerigeGoreFilter = 12,
    YetenekTemsilcisiFilter = 13,
    YineDeGosterFilter = 14,
}