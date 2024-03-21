namespace DotnetFoundation.Application.Models.Common;
public class PagingRequest
{
    private const int MaxPageSize = 50;
    public int PageNumber { get; set; }
    private int _pageSize;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value > MaxPageSize ? MaxPageSize : value) < 1 ? 1 : value;
        }
    }
}
