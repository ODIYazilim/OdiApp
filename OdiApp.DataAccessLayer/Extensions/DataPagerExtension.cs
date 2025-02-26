using Microsoft.EntityFrameworkCore;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.DataAccessLayer.Extensions
{
    public static class DataPagerExtension
    {
        public static async Task<PagedData<T>> PaginateAsync<T>(
        this IQueryable<T> query,
        int page,
        int limit)
        where T : class
        {
            var paged = new PagedData<T>();

            page = page <= 0 ? 1 : page;

            if (limit > 200)
            {
                limit = 200;
            }
            if (limit <= 0)
            {
                limit = 10;
            }

            paged.PageNo = page;
            paged.RecordsPerPage = limit;

            int totalItems = await query.CountAsync();

            var startRow = (page - 1) * limit;
            paged.DataList = await query
                       .Skip(startRow)
                       .Take(limit)
                       .ToListAsync();

            paged.Records = totalItems;
            paged.PageCount = (int)Math.Ceiling(paged.Records / (double)limit);

            return paged;
        }

        public static async Task<PagedData<T>> PaginateAsync<T>(
         this List<T> query,
         int page,
         int limit)
         where T : class
        {
            var paged = new PagedData<T>();

            page = page < 0 ? 1 : page;

            paged.PageNo = page;
            paged.RecordsPerPage = limit;

            int totalItems = query.Count();

            var startRow = (page - 1) * limit;
            paged.DataList = query
                       .Skip(startRow)
                       .Take(limit)
                       .ToList();

            paged.Records = totalItems;
            paged.PageCount = (int)Math.Ceiling(paged.Records / (double)limit);

            return paged;
        }
    }
}
