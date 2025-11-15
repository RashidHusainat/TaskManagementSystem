
namespace TaskManagementSystem.Application.Users;

public interface IUserService
{
    Task<Result<string>> CreateAsync(CreateUserDto dto);

    Task<Result<UserDto>> GetUserDetailAsync(string id);

    Task<Result<Unit>> UpdateAsync(string id, EditUserDto dto);

    Task<Result<Unit>> DeleteAsync(string id);

    Task<Result<PaginatedResult<UserDto>>> GetAllAsync(PaginationRequest paginationRequest);
}

