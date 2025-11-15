using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TaskManagementSystem.Application.Tasks;

public class TaskService(IUnitOfWork unitOfWork,
    IMapper mapper,
    IUserAccessor userAccessor,
    IValidationService validationService)
    : ITaskService
{
    public async Task<Result<string>> CreateAsync(CreateTaskDto dto, CancellationToken cancellationToken)
    {
        await validationService.ValidateRequest(dto);

        var task = mapper.Map<TaskItem>(dto);

        unitOfWork.Tasks.Add(task);

        var result = await unitOfWork.SaveChangesAsync(cancellationToken);

        return result
               ? Result<string>.Success(task.Id)
               : Result<string>.Failure("Failed to create new task", 400);
    }

    public async Task<Result<List<AssignedTaskDto>>> GetAssignedTasksAsync(CancellationToken cancellationToken)
    {
        var assignedTasks = await unitOfWork
            .Tasks
            .Query()
            .AsNoTracking()
            .Where(a => a.AssignedUserId == userAccessor.GetUserId())
            .ProjectToType<AssignedTaskDto>()
            .ToListAsync(cancellationToken);

        return Result<List<AssignedTaskDto>>.Success(assignedTasks);
    }

    public async Task<Result<Unit>> UpdateAsync(string id, EditTaskDto dto, CancellationToken cancellationToken)
    {
        await validationService.ValidateRequest(dto);

        var task = await unitOfWork.Tasks.GetByIdAsync(id);
        if (task is null)
            return Result<Unit>.Failure("No task found.", 404);

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.AssignedUserId = dto.AssignedUserId;
        task.DueDate = dto.DueDate;
        task.Priority = dto.Priority;

        var result = await unitOfWork.SaveChangesAsync(cancellationToken);

        return result
             ? Result<Unit>.Success(Unit.Value)
             : Result<Unit>.Failure("Failed to edit new task", 400);
    }

    public async Task<Result<Unit>> CompleteTaskAsync(string id, CancellationToken cancellationToken)
    {
        var task = await unitOfWork.Tasks.GetByIdAsync(id);
        if (task is null)
            return Result<Unit>.Failure("No task found.", 404);

        task.Status = TaskState.Completed;
        task.CompletedAt = DateTime.UtcNow;

        var result = await unitOfWork.SaveChangesAsync(cancellationToken);

        return result
             ? Result<Unit>.Success(Unit.Value)
             : Result<Unit>.Failure("Failed to edit new task.", 400);
    }

    public async Task<Result<Unit>> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var task = await unitOfWork.Tasks.GetByIdAsync(id);
        if (task is null)
            return Result<Unit>.Failure("No task found.", 404);

        unitOfWork.Tasks.Delete(task);

        var result = await unitOfWork.SaveChangesAsync(cancellationToken);

        return result
             ? Result<Unit>.Success(Unit.Value)
             : Result<Unit>.Failure("Failed to delete task.", 400);
    }

    public async Task<Result<PaginatedResult<TaskDto>>> GetTasksAsync(PaginationRequest paginationRequest,CancellationToken cancellationToken)
    {
        var pageIndex = paginationRequest.PageIndex;
        var pageSize = paginationRequest.PageSize;

        var totalCount = await unitOfWork.Tasks.Query().LongCountAsync(cancellationToken);

        var tasks = await unitOfWork
            .Tasks
            .Query()
            .AsNoTracking()
            .OrderBy(x=>x.CreatedAt)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ProjectToType<TaskDto>()
            .ToListAsync(cancellationToken);

        return Result<PaginatedResult<TaskDto>>.Success(
            new PaginatedResult<TaskDto>
            {
                Count = totalCount,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = tasks
            });
    }
}

