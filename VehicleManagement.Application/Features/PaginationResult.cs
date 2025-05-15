namespace VehicleManagement.Application.Features;

public class PaginationResult<T>(int pageSize, int pageNumber, int totalCount, List<T> data)
{
    public int PageSize { get; set; } = pageSize;
    public int PageNumber { get; set; } = pageNumber;
    public int TotalCount { get; set; } = totalCount;
    public List<T> Data { get; set; } = data;

    public static PaginationResult<T> Create(int pageSize, int pageNumber, int totalCount, List<T> data)
    {
        return new(pageSize, pageNumber, totalCount, data);
    }
}
