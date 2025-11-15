
namespace TaskManagementSystem.Application.Core.Mappings;

public class TasksListMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TaskItem, TaskDto>()
             .Map(dest => dest.Id, src => src.Id)
             .Map(dest => dest.Title, src => src.Title)
             .Map(dest => dest.Description, src => src.Description)
             .Map(dest => dest.DueDate, src => src.DueDate)
             .Map(dest => dest.CompletedAt, src => src.CompletedAt)
             .Map(dest => dest.CreatedAt, src => src.CreatedAt)
             .Map(dest => dest.Priority, src => src.Priority)
             .Map(dest => dest.CompletedAt, src => src.CompletedAt)
             .Map(dest => dest.AssignedToUser, src => src.AssignedUser!.Email);
    }
}

