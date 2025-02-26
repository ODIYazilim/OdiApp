namespace OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.MenajerPerformerGuncellenenAlanlarDTOs;

public class MenajerPerformerGuncellenenAlaniOutputDTO
{
    public string MenajerPerformerGuncellenenAlaniId { get; set; }
    public string MenajerId { get; set; }
    public string PerformerId { get; set; }
    public string PerformerAdSoyad { get; set; }
    public string GuncellemeTipi { get; set; }
    public string GuncellenenAlan { get; set; }
    public DateTime GuncellenmeTarihi { get; set; }
    public bool MenajerGordu { get; set; }
    public DateTime MenajerGorduTarihi { get; set; }
}