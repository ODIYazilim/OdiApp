namespace OdiApp.DTOs.PerformerDTOs.SetCard;

//Dbden çekerken tek işlemle almak için ayrı model oluşturuldu.
public class SetCardAlbumVeFotografDTO
{
    public int AlbumId { get; set; }
    public string AlbumAdi { get; set; }
    public int AlbumTipiId { get; set; }
    public string AlbumTipiLabel { get; set; }
    public int FotoId { get; set; }
    public string FotoAdi { get; set; }
    public string FotoDosyaYolu { get; set; }
    public string Sira { get; set; }
    public bool Yatay { get; set; }
}