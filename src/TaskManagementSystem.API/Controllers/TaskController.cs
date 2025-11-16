
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace TaskManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class TaskController(ITaskService taskService) : BaseApiController
    {
        [HttpPost]
        [Authorize(Roles ="Admin")]
        [SwaggerOperation(Summary = "Create new task (Admin only)")]
        public async Task<ActionResult<string>> CreateTask(CreateTaskDto taskDto, CancellationToken cancellationToken)
        {
            return HandleResult(await taskService.CreateAsync(taskDto, cancellationToken));
        }


        [HttpGet("AssignedTasks")]
        [SwaggerOperation(Summary = "Retrieve assigned tasks (Admin/User can only view tasks assigned to them)")]
        public async Task<ActionResult<List<AssignedTaskDto>>> GetAssignedTasks(CancellationToken cancellationToken)
        {
            return HandleResult(await taskService.GetAssignedTasksAsync(cancellationToken));
        }


        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Update task details (Admin only)")]
        public async Task<ActionResult> EditTask(string id, EditTaskDto taskDto, CancellationToken cancellationToken)
        {
            return HandleResult(await taskService.UpdateAsync(id, taskDto, cancellationToken));
        }


        [HttpPut("{id}/Complete")]
        [Authorize(Policy = "IsTaskOwner")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "User can change there task status or complete there owned task (Task Owner only)")]
        public async Task<ActionResult> CompleteTask(string id, CancellationToken cancellationToken)
        {
            return HandleResult(await taskService.CompleteTaskAsync(id, cancellationToken));
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Delete task (Admin only)")]
        public async Task<ActionResult> DeleteTask(string id, CancellationToken cancellationToken)
        {
            return HandleResult(await taskService.DeleteAsync(id, cancellationToken));
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "List all tasks (Admin only)")]
        public async Task<ActionResult<PaginatedResult<TaskDto>>> GetTasks([FromQuery]PaginationRequest paginationRequest, CancellationToken cancellationToken)
        {
            return HandleResult(await taskService.GetTasksAsync(paginationRequest, cancellationToken));
        }

    }
}
