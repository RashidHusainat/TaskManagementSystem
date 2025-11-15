
namespace TaskManagementSystem.Application.Core.Interfaces;

public interface IValidationService
{
    Task ValidateRequest<T>(T request);
}

