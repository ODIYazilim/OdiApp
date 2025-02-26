using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.PerformerModels.VideoAlbumm;

[Table("VideoAlbumleri")]
public class VideoAlbum : BaseModel
{
    public int VideoAlbumTipiId { get; set; }
    public string KullaniciId { get; set; }
    public string AlbumAdi { get; set; }

    public List<VideoAlbumVideo> Videolar { get; set; }
    public VideoAlbumTipi AlbumTipi { get; set; }
}