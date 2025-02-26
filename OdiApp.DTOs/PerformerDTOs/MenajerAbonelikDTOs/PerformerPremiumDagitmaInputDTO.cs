using OdiApp.DTOs.SharedDTOs.OrtakDTOs;

namespace OdiApp.DTOs.PerformerDTOs.MenajerAbonelikDTOs;

public class PerformerPremiumDagitmaInputDTO
{
    public string YetenekTemsilcisiAbonelikId { get; set; }
    public string YetenekTemsilcisiId { get; set; }
    public List<PerformerIdDTO> PerformerListesi { get; set; }
}
