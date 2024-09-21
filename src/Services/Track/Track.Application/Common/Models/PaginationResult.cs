namespace Track.Application.Common.Models;

public class PaginationResult<T> where T : class
{
    private PaginationResult(int pageIndex, int pageSize, int count, List<T> data)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
        Data = data;
    }

    public static PaginationResult<T> CreatePaginationResult(int pageIndex, int pageSize, int count, List<T> data)
    {
        return new PaginationResult<T>(pageIndex, pageSize, count, data);
    }

    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Count { get; set; }
    public List<T> Data { get; set; }
}