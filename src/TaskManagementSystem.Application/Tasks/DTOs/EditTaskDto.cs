
namespace TaskManagementSystem.Application.Tasks.DTOs;

public class EditTaskDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }

    // Enums
    public Priority Priority { get; set; }

    // Navigations
    public string AssignedUserId { get; set; } = null!;
}

