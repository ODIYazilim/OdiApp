namespace OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs;

public class FiltreAyarlariInputDTO
{
    public bool DetayliFiltre { get; set; } //Filtrenin tüm özelliklerini getirmeye yarar.
    public bool MenajerFiltreAyarlari { get; set; } //Filtrenin sadece menajer için olan seçeneklerini getirir.
}