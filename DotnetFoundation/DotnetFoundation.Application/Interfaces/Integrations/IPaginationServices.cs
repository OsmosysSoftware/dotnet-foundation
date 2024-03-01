using DotnetFoundation.Application.Models.Common;

namespace DotnetFoundation.Application.Interfaces.Integrations;
public interface IPagiationServices<T>
{
    public Task<PagedList<T>> ToPagedListAsync(IQueryable<T> query, int pageNumber, int pageSize);
}
