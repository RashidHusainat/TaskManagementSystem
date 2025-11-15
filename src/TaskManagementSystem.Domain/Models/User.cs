using Microsoft.AspNetCore.Identity;

namespace TaskManagementSystem.Domain.Models;

public class User : IdentityUser
{
    // Navigations
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}

