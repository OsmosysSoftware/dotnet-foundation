using Microsoft.EntityFrameworkCore;

namespace DotnetFoundation.Application.Models.Common;
public class PagedList<T>
{
    public List<T> Items { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public bool hasPrevious => PageNumber > 1;
    public bool hasNext => PageNumber * PageSize < TotalCount;

    public PagedList(List<T> items, int pageNumber, int pageSize, int totalCount)
    {
        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
    public static async Task<PagedList<T>> ToPagedListAsync(IQueryable<T> query, int pageNumber, int pageSize)
    {
        if(pageNumber <= 1) { pageNumber = 1; }
        if(pageSize <= 1) {  pageSize = 1; }
        int totalCount = await query.CountAsync().ConfigureAwait(false);
        List<T> items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync().ConfigureAwait(false);

        return new PagedList<T>(items, pageNumber, pageSize, totalCount);
    }
}
