
namespace TaskManagementSystem.Application.Tasks.DTOs;

public class TaskDto
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public TaskState Status { get; set; } 
    public Priority Priority { get; set; }
    public string AssignedToUser { get; set; } = null!;
    public string? CreatedBy { get; set; }
}

