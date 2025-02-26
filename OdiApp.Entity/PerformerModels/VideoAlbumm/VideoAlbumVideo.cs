using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.PerformerModels.VideoAlbumm;

[Table("VideoAlbumVideolar")]
public class VideoAlbumVideo : BaseModel
{
    public int AlbumId { get; set; }
    public string VideoAdi { get; set; }
    public string DosyaYolu { get; set; }
    public int Sira { get; set; }
    public bool Aktif { get; set; }
    public string VideoDili { get; set; }
    public string Tag { get; set; }
}