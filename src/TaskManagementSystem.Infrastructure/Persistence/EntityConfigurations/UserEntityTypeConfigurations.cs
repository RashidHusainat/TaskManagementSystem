namespace TaskManagementSystem.Infrastructure.Persistence.EntityConfigurations;

internal class UserEntityTypeConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(x => x.Tasks)
            .WithOne(a => a.AssignedUser)
            .HasForeignKey(a => a.AssignedUserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}

