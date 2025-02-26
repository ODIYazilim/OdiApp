using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.PerformerModels.FotografAlbum;

[Table("FotoAlbumleri")]
public class FotoAlbum : BaseModel
{
    public int FotoAlbumTipiId { get; set; }
    public string KullaniciId { get; set; }
    public string AlbumAdi { get; set; }
    public List<FotoAlbumFotograf> Fotograflar { get; set; }
    public FotoAlbumTipi AlbumTipi { get; set; }
}