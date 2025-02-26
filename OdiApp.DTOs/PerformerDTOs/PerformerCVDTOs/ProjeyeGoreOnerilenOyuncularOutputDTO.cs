using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.CVDTOs;

namespace OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs;

public class ProjeyeGoreOnerilenOyuncularOutputDTO
{
    public PagedData<KullaniciBilgileriDTO> KullanicilarPagedData { get; set; }
    public FiltrelenenPerformerlarInputDTO UygulananFiltreler { get; set; }
}