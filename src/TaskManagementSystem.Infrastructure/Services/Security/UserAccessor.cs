using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TaskManagementSystem.Application.Core.Interfaces;

namespace TaskManagementSystem.Infrastructure.Services.Security;

public class UserAccessor(IHttpContextAccessor httpContextAccessor)
    : IUserAccessor
{
    //public async Task<User> GetUserAsync()
    //{
    //    return await dbContext.Users.FindAsync(GetUserId())
    //        ?? throw new UnauthorizedAccessException("No user is logged in");
    //}

    public string GetUserId()
        => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException("No logged-in user.");

    public string GetUserName()
       => httpContextAccessor.HttpContext?.User.Identity?.Name
        ?? throw new UnauthorizedAccessException("No logged-in user.");



}
