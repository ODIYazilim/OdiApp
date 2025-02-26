using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.VideoTipiModels;

public class VideoTag : StringBaseModel
{
    public string Tag { get; set; }
    public string TagKodu { get; set; }
    public int DilId { get; set; }
    public int Sira { get; set; }
    public bool Aktif { get; set; }
    public bool OdiOnerisi { get; set; }
}