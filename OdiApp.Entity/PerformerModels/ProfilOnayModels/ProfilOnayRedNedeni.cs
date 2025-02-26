using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.ProfilOnayModels;

public class ProfilOnayRedNedeni : StringBaseModel
{
    public string ProfilOnayId { get; set; }
    public int ProfilOnayRedNedeniTanimiId { get; set; } //tanımlı red sebebi idsi
}