using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.YetenekTemsilcisiModels;

/// <summary>
/// Performer'ın yetenek temsilcisi(menajer) ilişkisini tutar.
/// </summary>
public class PerformerYetenekTemsilcisi : StringBaseModel
{
    public string PerformerId { get; set; }
    public string MenajerId { get; set; }
    public bool Aktif { get; set; }
    public DateTime AtamaTarihi { get; set; }
    public DateTime? AtamaIptalTarihi { get; set; }
}