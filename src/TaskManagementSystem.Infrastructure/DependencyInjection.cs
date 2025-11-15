
using Microsoft.AspNetCore.Authorization;

namespace TaskManagementSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<IUserAccessor, UserAccessor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetService<ISaveChangesInterceptor>()!);
            options.UseInMemoryDatabase("TasksDB");
        });

        

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddTransient(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        services.AddTransient<IAuthorizationHandler, TaskStatusRequirementHandler>();


        return services;
    }
}

