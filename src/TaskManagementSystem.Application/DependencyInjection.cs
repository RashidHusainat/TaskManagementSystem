namespace TaskManagementSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Mapster Mapper
        services.AddMapster();
        MappingProfiles.RegisterMappings();
        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddScoped<IMapper, ServiceMapper>();

        services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITaskService, TaskService>();

        services.AddScoped<IValidationService, ValidationService>();

        return services;
    }
}

