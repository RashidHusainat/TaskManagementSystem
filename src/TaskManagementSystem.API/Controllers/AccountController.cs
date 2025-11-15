
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace TaskManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
public class AccountController(IUserService userService) : BaseApiController
{
    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Create new user (Anonymous Access) also user can register by registration end point below /api/register")]
    public async Task<ActionResult<string>> CreateUser(CreateUserDto userDto)
    {
        return HandleResult(await userService.CreateAsync(userDto));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Retrieve a user by id (you should logged in before) below login endpoint /api/login")]
    public async Task<ActionResult<UserDto>> GetUserDetail(string id)
    {
        return HandleResult(await userService.GetUserDetailAsync(id));
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Update user details by id (you should logged in before) below login endpoint /api/login")]
    public async Task<ActionResult> EditUserDetail(string id,EditUserDto userDto)
    {
        return HandleResult(await userService.UpdateAsync(id, userDto));
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Delete user by id (you should logged in before) below login endpoint /api/login")]
    public async Task<ActionResult> DeleteUser(string id)
    {
        return HandleResult(await userService.DeleteAsync(id));
    }


    [HttpGet("Users")]
    [Authorize(Roles ="Admin")]
    [SwaggerOperation(Summary = "List all users paginated (you should logged in before) admin only")]
    public async Task<ActionResult<PaginatedResult<UserDto>>> GetUsers([FromQuery] PaginationRequest request)
    {
        return HandleResult(await userService.GetAllAsync(request));
    }

}

