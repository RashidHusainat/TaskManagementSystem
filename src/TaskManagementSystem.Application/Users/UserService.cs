

using Microsoft.EntityFrameworkCore;

namespace TaskManagementSystem.Application.Users;

public class UserService(UserManager<User> userManager, IValidationService validationService) : IUserService
{
    public async Task<Result<string>> CreateAsync(CreateUserDto dto)
    {
        await validationService.ValidateRequest(dto);

        var user = dto.Adapt<User>();

        var result = await userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "User");
            return Result<string>.Success(user.Id);
        }

        return Result<string>.Failure(result.Errors.FirstOrDefault()!.Description, 400);

    }

    public async Task<Result<UserDto>> GetUserDetailAsync(string id)
    {
        var user = await userManager
            .Users
            .AsNoTracking()
            .ProjectToType<UserDto>()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (user is null)
            return Result<UserDto>.Failure("User not found.", 404);

        return Result<UserDto>.Success(user);
    }

    public async Task<Result<Unit>> UpdateAsync(string id, EditUserDto dto)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user is null)
            return Result<Unit>.Failure("User not found.", 404);

        user.UserName = dto.UserName;
        user.Email = dto.Email;

        await userManager.UpdateAsync(user);

        return Result<Unit>.Success(Unit.Value);

    }

    public async Task<Result<Unit>> DeleteAsync(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user is null)
            return Result<Unit>.Failure("User not found.", 404);

        await userManager.DeleteAsync(user);

        return Result<Unit>.Success(Unit.Value);
    }

    public async Task<Result<PaginatedResult<UserDto>>> GetAllAsync(PaginationRequest paginationRequest)
    {
        var pageIndex = paginationRequest.PageIndex;
        var pageSize = paginationRequest.PageSize;

        var totalCount = await userManager.Users.LongCountAsync();

        var users = await userManager
            .Users
            .AsNoTracking()
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ProjectToType<UserDto>()
            .ToListAsync();

        return Result<PaginatedResult<UserDto>>.Success(
            new PaginatedResult<UserDto>
            {
                Count = totalCount,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = users
            });
    }
}

