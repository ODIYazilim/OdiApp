using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.PerformerModels.VideoAlbumm;

[Table("VideoAlbumTipiLabels")]
public class VideoAlbumTipiLabel : BaseModel
{
    public int DilId { get; set; }
    public int AlbumTipId { get; set; }
    public string Label { get; set; }
    public int Sira { get; set; }
    public int Aktif { get; set; }
}