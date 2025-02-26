using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.PerformerModels.ProfilOnayModels;

[Table("ProfilOnaylari")]
public class ProfilOnay : StringBaseModel
{
    public string PerformerId { get; set; }
    public string YetenekTemsilcisiId { get; set; }
    public DateTime OnayGonderimTarihi { get; set; }
    public bool Onay { get; set; }
    public DateTime? OnaylanmaTarihi { get; set; }
    public string? OnaylayanId { get; set; }
    public bool Red { get; set; }
    public string? RedSebebiMetni { get; set; }
    public DateTime? RedTarihi { get; set; }
    public string? ReddedenId { get; set; }
    public bool Incelemede { get; set; }
    public string? InceleyenId { get; set; }
    public DateTime? IncelemeTarihi { get; set; }
    public bool Aktif { get; set; }
    public DateTime? SonuclamaTarihi { get; set; }
}