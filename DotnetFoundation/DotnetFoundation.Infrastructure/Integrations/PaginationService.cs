using DotnetFoundation.Application.Interfaces.Integrations;
using DotnetFoundation.Application.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace DotnetFoundation.Infrastructure.Integrations;
public class PaginationService<T> : IPaginationService<T>
{
    public async Task<PagedList<T>> ToPagedListAsync(IQueryable<T> query, int pageNumber, int pageSize)
    {
        // To fetch atleast one value, if exists, when no parameters are passed
        if (pageNumber <= 0) { pageNumber = 1; }
        if (pageSize <= 0) { pageSize = 10; }

        int totalCount = await query.CountAsync().ConfigureAwait(false);
        List<T> items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync().ConfigureAwait(false);

        return new PagedList<T>(items, pageNumber, pageSize, totalCount);
    }
}
