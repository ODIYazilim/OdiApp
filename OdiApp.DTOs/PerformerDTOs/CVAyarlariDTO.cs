using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.EgitimDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.YetenekTipiDTOs;

namespace OdiApp.DTOs.PerformerDTOs;

public class CVAyarlariDTO
{
    public List<CVFormAlanlariDTO> CVFormAlanlari { get; set; }
    public List<EgitimTipiDTO> EgitimListesi { get; set; }
    public List<YetenekTipiDTO> YetenekListesi { get; set; }
}