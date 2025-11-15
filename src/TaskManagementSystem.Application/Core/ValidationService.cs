
namespace TaskManagementSystem.Application.Core;

public class ValidationService(IServiceProvider serviceProvider) : IValidationService
{
    public async Task ValidateRequest<T>(T request)
    {
        var validator = serviceProvider.GetService<IValidator<T>>();

        if (validator is null) return;

        var result =await validator.ValidateAsync(request);

        if (!result.IsValid)
            throw new ValidationException(result.Errors);

    }
}

