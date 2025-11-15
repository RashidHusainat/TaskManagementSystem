using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.Authorization;
using TaskManagementSystem.Infrastructure.Services.Security;


namespace TaskManagementSystem.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddIdentityApiEndpoints<User>(options
                 => options.User.RequireUniqueEmail = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddControllers(options =>
        {
            var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();

            options.Filters.Add(new AuthorizeFilter(policy));
        });

        //services.AddOpenApi();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
           c.EnableAnnotations();
        });

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddTransient<LoggingMiddleware>();

        services.AddProblemDetails();
        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddCors();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("IsTaskOwner", policy =>
            {
                policy.Requirements.Add(new TaskStatusRequirement());
                policy.RequireRole(roles: ["User"]);
            });
        });

        return services;
    }


    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseExceptionHandler(options => { });

        app.UseMiddleware<LoggingMiddleware>();

        app.UseCors();

        app.UseHealthChecks("/api/health",
           new HealthCheckOptions
           {
               ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
           });

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.MapGroup("api").MapIdentityApi<User>();

        return app;
    }
}

