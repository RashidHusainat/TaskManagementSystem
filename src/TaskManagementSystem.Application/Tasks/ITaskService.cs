
namespace TaskManagementSystem.Application.Tasks;

public interface ITaskService
{
    Task<Result<string>> CreateAsync(CreateTaskDto dto, CancellationToken cancellationToken);

    Task<Result<List<AssignedTaskDto>>> GetAssignedTasksAsync(CancellationToken cancellationToken);

    Task<Result<Unit>> UpdateAsync(string id, EditTaskDto dto, CancellationToken cancellationToken);

    Task<Result<Unit>> CompleteTaskAsync(string id, CancellationToken cancellationToken);
    Task<Result<Unit>> DeleteAsync(string id, CancellationToken cancellationToken);

    Task<Result<PaginatedResult<TaskDto>>> GetTasksAsync(PaginationRequest paginationRequest,CancellationToken cancellationToken);
}

