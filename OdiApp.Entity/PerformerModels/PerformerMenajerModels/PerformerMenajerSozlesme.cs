using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.PerformerMenajerModels;

public class PerformerMenajerSozlesme : StringBaseModel
{
    public string MenajerId { get; set; }
    public string PerformerId { get; set; }
    public DateTime SozleşmeImzaTarihi { get; set; }
    public int SozlesmeSuresi { get; set; }
    public DateTime SozlesmeBitisTarihi { get; set; }
    public string SozlesmeDosyasi { get; set; }
    public string SozlesmeyiEkleyenId { get; set; }
}