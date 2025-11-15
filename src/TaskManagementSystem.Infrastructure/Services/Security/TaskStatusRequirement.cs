
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace TaskManagementSystem.Infrastructure.Services.Security;

public class TaskStatusRequirement : IAuthorizationRequirement
{
}

public class TaskStatusRequirementHandler(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    : AuthorizationHandler<TaskStatusRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TaskStatusRequirement requirement)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return;

        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext?.GetRouteValue("id") is not string taskId) return;

        var task = await dbContext.Tasks
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == taskId && x.AssignedUserId == userId);

        if (task is null) return;

        context.Succeed(requirement);
    }
}

