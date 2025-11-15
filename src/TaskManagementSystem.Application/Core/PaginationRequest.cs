

namespace TaskManagementSystem.Application.Core;

public class PaginationRequest
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; } = 10;
}

