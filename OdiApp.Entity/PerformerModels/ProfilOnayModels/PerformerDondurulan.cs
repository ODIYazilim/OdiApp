using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.ProfilOnayModels;

public class PerformerDondurulan : StringBaseModel
{
    public string DondurulanPerformerId { get; set; }
    public string DonduranId { get; set; }
    public DateTime DondurulmaTarihi { get; set; }
}