using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;

namespace TaskManagementSystem.Tests.PresentationLayer;

public class TaskControllerUnitTests
{
    private readonly Mock<ITaskService> mockTaskService;
    public TaskControllerUnitTests()
    {
        mockTaskService = new Mock<ITaskService>();
    }


    [Fact]
    public async Task GetTasks_PaginatedRequest_ReturnsPaginatedTasksResult()
    {
        // Arrange
        var PaginatedRequest = new PaginationRequest { PageIndex = 0, PageSize = 10 };
        var PaginatedTasksList = new Result<PaginatedResult<TaskDto>>
        {
            IsSuccess = true,
            Value = new PaginatedResult<TaskDto>
            {
                PageIndex = 0,
                PageSize = 10,
                Count = 2,
                Items = new List<TaskDto>
               {
                   new TaskDto
                   {
                       Id = "1",
                       Title = "Task 1",
                       Description = "Description 1",
                       DueDate = DateTime.UtcNow.AddDays(5),
                       Priority = Priority.Medium,
                       Status= TaskState.InProgress,
                       AssignedToUser = "user1"
                   },
                   new TaskDto
                   {
                       Id = "2",
                       Title = "Task 2",
                       Description = "Description 2",
                       DueDate = DateTime.UtcNow.AddDays(10),
                       Priority = Priority.High,
                        Status= TaskState.InProgress,
                       AssignedToUser = "user2"
                   }
               }
            }
        };
        mockTaskService
            .Setup(x => x.GetTasksAsync(PaginatedRequest, default))
            .ReturnsAsync(PaginatedTasksList);
        var taskController = new TaskController(mockTaskService.Object);


        // Act
        var result = await taskController.GetTasks(PaginatedRequest, default);

        // Assert
        Assert.IsType<ActionResult<PaginatedResult<TaskDto>>>(result);
        var taskResult = (result.Result as OkObjectResult)?.Value as PaginatedResult<TaskDto>;
        Assert.NotNull(taskResult);
        Assert.Equal(2, taskResult.Count);
        Assert.Equal(0, taskResult.PageIndex);
        Assert.Equal(10, taskResult.PageSize);
        Assert.True(taskResult.Items.Equals(PaginatedTasksList.Value.Items));

    }
}

