using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

namespace TaskManagementSystem.Infrastructure.Persistence.Extensions;

public static class DataBaseExtentions
{
    public static async Task InitialMigration(this WebApplication web)
    {

        using var scope = web.Services.CreateScope();

        var AppDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        await SeedRolesAsync(roleManager);
        await SeedUsersAsync(userManager);
        await SeedTasksAsync(AppDbContext);
    }

    private static async Task SeedTasksAsync(ApplicationDbContext context)
    {
        if (!await context.Tasks.AnyAsync())
        {
            context.Tasks.AddRange(InitialData.GetTasks());
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedUsersAsync(UserManager<User> userManager)
    {
        if (!await userManager.Users.AnyAsync())
        {
            var users = InitialData.GetUsers();

            foreach (var user in users)
            {
                await userManager.CreateAsync(user,"P@ssw0rd@2025");
                await userManager.AddToRoleAsync(user, "User");
            }

            var admines = InitialData.GetAdmins();

            foreach (var admin in admines)
            {
                await userManager.CreateAsync(admin, "P@ssw0rd@2025");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        var roles = InitialData.GetRoles();

        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}

