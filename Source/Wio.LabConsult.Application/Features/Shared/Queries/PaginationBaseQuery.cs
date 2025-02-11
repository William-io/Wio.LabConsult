namespace Wio.LabConsult.Application.Features.Shared.Queries;

public class PaginationBaseQuery
{
    public string? Search { get; set; }
    public string? Sort { get; set; }
    public int PageIndex { get; set; } = 1;
    private int _pageSize = 3;
    private int MaxPageSize = 50;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}
