
namespace TaskManagementSystem.Domain.Models;

public class TaskItem : Entity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = null!;
    public string? Description { get; set; } 
    public DateTime? DueDate { get; set; }
    public DateTime? CompletedAt { get; set; }

    // Enums
    public TaskState Status { get; set; } = TaskState.InProgress;
    public Priority Priority { get; set; }

    // Navigations
    public string AssignedUserId { get; set; } = null!;
    public User? AssignedUser { get; set; }
   
}

