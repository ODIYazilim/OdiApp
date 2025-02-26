namespace OdiApp.DTOs.SharedDTOs
{
    public class PagedDataInfo
    {
        public int PageNo { get; set; } //sayfa no
        public int PageCount { get; set; } //toplam sayfa sayısı
        public int Records { get; set; } //toplam veri sayısı
        public int RecordsPerPage { get; set; }//her sayfadaki veri sayısı
    }
}
