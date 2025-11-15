namespace TaskManagementSystem.Infrastructure.Persistence.EntityConfigurations;

internal class TaskItemEntityTypeConfigurations : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(255);

        builder.Property(x => x.DueDate)
            .HasColumnType("date")
            .IsRequired();

        builder.HasIndex(x => x.AssignedUserId);

        builder.Property(x => x.Status)
            .HasDefaultValue(TaskState.InProgress)
            .IsRequired()
              .HasConversion(s => s.ToString(),
              dbstatus => (TaskState)Enum.Parse(typeof(TaskState), dbstatus));

        builder.Property(x => x.Priority)
            .IsRequired()
            .HasConversion(s => s.ToString(),
            dbstatus => (Priority)Enum.Parse(typeof(Priority), dbstatus));

    }
}

