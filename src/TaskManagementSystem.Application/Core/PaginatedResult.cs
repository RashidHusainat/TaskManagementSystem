
namespace TaskManagementSystem.Application.Core;

public class PaginatedResult<T>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public long Count { get; set; }
    public List<T> Items { get; set; } = [];
}

