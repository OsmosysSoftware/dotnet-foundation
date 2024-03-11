namespace DotnetFoundation.Application.Models.Common;
public class PagingRequest
{
    private const int MaxPageSize = 50;
    public int PageNumber { get; set; }
    private int pageSize;
    public int PageSize
    {
        get
        {
            return pageSize;
        }
        set
        {
            pageSize = (value > MaxPageSize ? MaxPageSize : value) < 1 ? 1 : value;
        }
    }
}
