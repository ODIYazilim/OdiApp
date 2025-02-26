namespace OdiApp.DTOs.PerformerDTOs.SetCard;

public class SetCardOutputDTO
{
    public SetCardKisiselBilgilerDTO KisiselBilgiler { get; set; }
    public List<SetCardFizikselOzellikDTO> FizikselOzellikList { get; set; }
    public List<SetCardKisiselOzellikDTO> KisiselOzellikList { get; set; }
    public List<SetCardEgitimBilgileriDTO> EgitimBilgileriList { get; set; }
    public List<SetCardYetenekBilgileriDTO> YetenekBilgileriList { get; set; }
    public List<SetCardDeneyimBilgileriGroupedDTO> DeneyimBilgileriList { get; set; }
    public List<SetCardAlbumDTO> AlbumList { get; set; }
}