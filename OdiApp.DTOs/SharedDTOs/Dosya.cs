namespace OdiApp.DTOs.SharedDTOs
{
    public class Dosya
    {
        public int DosyaTipi { get; set; }
        public string? KullaniciId { get; set; }
        public string? AlbumAdi { get; set; }
        public int ProjeId { get; set; }

        //dosya adı uniqe olmalı ve dosya uzantısını da içermeli
        public string? DosyaAdi { get; set; }
    }
}