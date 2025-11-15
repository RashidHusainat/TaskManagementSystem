namespace RMS.Conference.Application.Core.Mappings;

public static class MappingProfiles
{
    public static void RegisterMappings()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        // Automatically scan this assembly for IRegister implementations
        config.Scan(Assembly.GetExecutingAssembly());
    }
}

