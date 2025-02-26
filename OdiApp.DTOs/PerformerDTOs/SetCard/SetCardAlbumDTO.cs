namespace OdiApp.DTOs.PerformerDTOs.SetCard;

public class SetCardAlbumDTO
{
    public int AlbumId { get; set; }
    public string AlbumAdi { get; set; }
    public int AlbumTipiId { get; set; }
    public string AlbumTipiLabel { get; set; }
    public List<SetCardAlbumFotograf> FotografList { get; set; }
}