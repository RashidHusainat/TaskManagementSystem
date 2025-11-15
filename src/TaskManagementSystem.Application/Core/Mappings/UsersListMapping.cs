namespace TaskManagementSystem.Application.Core.Mappings;

public class UsersListMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.UserName, src => src.UserName)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Tasks, src => src.Tasks.ToList());
    }
}

