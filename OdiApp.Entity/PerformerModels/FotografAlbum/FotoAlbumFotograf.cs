using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.PerformerModels.FotografAlbum;

[Table("FotoAlbumFotograflar")]
public class FotoAlbumFotograf : BaseModel
{
    public int AlbumId { get; set; }
    public string FotografAdi { get; set; }
    public string DosyaYolu { get; set; }
    public int Sira { get; set; }
    public bool Yatay { get; set; }
    public bool Aktif { get; set; }
}