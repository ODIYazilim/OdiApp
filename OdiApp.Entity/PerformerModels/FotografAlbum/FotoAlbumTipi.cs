using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.PerformerModels.FotografAlbum;

[Table("FotoAlbumTipleri")]
public class FotoAlbumTipi : BaseModel
{
    public string AlbumTipi { get; set; }
    public int Sira { get; set; }
    public bool Premium { get; set; }
    public bool Aktif { get; set; }
}