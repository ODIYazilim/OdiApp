using System.Collections.Generic;

namespace OdiApp.DTOs.SharedDTOs
{
    public class PagedData<T> : PagedDataInfo
    {
        public List<T> DataList { get; set; }
    }
}