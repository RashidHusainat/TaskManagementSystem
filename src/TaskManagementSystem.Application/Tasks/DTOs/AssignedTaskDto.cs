
using System.Text.Json.Serialization;

namespace TaskManagementSystem.Application.Tasks.DTOs;

public class AssignedTaskDto
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public TaskState Status { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? CreatedBy { get; set; }

}

