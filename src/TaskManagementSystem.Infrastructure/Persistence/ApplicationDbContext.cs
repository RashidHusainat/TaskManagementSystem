using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TaskManagementSystem.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<User>(options)
{
    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserEntityTypeConfigurations());
        builder.ApplyConfiguration(new TaskItemEntityTypeConfigurations());

        base.OnModelCreating(builder);
    }

    
}

