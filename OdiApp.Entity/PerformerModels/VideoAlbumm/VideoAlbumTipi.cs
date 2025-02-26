using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.PerformerModels.VideoAlbumm;

[Table("VideoAlbumTipleri")]
public class VideoAlbumTipi : BaseModel
{
    public string AlbumTipi { get; set; }
    public string AlbumTipiKodu { get; set; }
    public int DilId { get; set; }
    public int Sira { get; set; }
    public int Aktif { get; set; }
}