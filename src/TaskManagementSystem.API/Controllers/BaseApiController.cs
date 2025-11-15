namespace TaskManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess && result.Value != null) return Ok(result.Value);

        if (!result.IsSuccess && result.Code == 404) return NotFound();

        return BadRequest(result.Error);
    }
}

