using FluentValidation;
using FluentValidation.Results;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;

namespace TaskManagementSystem.Tests.ServiceLayer.Tasks;

public class TaskServiceUnitTests
{
    private async Task<(ApplicationDbContext dbContext, ITaskService ITaskService)> BuildTestContextAndService<T>()
    {
        var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase($"TaskServiceTestDb_{Guid.NewGuid()}")
            .ConfigureWarnings(cfg => cfg.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;
        var dbContext = new ApplicationDbContext(dbOptions);

        dbContext.Roles.AddRange(
            new IdentityRole {Id="user-role", Name = "User" },
            new IdentityRole {Id = "admin-role",  Name = "Admin" }
            );

        dbContext.Users.AddRange(
            new User
            {
                Id = "user-1",
                UserName = "testuser1",
                Email = "testuser1@test.com",
                Tasks = new List<TaskItem>()
                {
                    new TaskItem
                    {
                        Title = "User1 Task 1",
                        Description = "User1 Description 1",
                        CreatedAt=DateTime.UtcNow,
                        Priority=Priority.Medium,
                        DueDate=DateTime.UtcNow.AddDays(1)
                    },

                }
            },
            new User
            {
                Id = "user-2",
                UserName = "testuser2",
                Email = "testuser2@test.com",
                Tasks = new List<TaskItem>()
                {
                    new TaskItem
                    {
                        Title = "User2 Task 2",
                        Description = "User2 Description 2",
                        CreatedAt=DateTime.UtcNow,
                        Priority=Priority.High,
                        DueDate=DateTime.UtcNow.AddDays(2)
                    },
                }
            }
            );

        dbContext.UserRoles.AddRange(
            new IdentityUserRole<string>
            {
                UserId = "user-1",
                RoleId = "user-role"
            },
            new IdentityUserRole<string>
            {
                UserId = "user-2",
                RoleId = "user-role"
            }
            );

        await dbContext.SaveChangesAsync();


        var unitOfWorkMock = new Mock<IUnitOfWork>();

        unitOfWorkMock
            .Setup(u => u.Tasks)
            .Returns(new Repository<TaskItem>(dbContext));

        unitOfWorkMock
     .Setup(u => u.SaveChangesAsync(default))
     .ReturnsAsync(true);

        var mapper = new Mapper();

        var userAccessorMock = new Mock<IUserAccessor>();
        userAccessorMock.Setup(x => x.GetUserId()).Returns("user-1");


        var serviceProviderMock = new Mock<IServiceProvider>();

        var validatorMock = new Mock<IValidator<T>>();

        validatorMock
            .Setup(v => v.ValidateAsync(It.IsAny<T>(), default))
            .ReturnsAsync(new ValidationResult());

        serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IValidator<T>)))
            .Returns(validatorMock.Object);

        var validationService = new ValidationService(serviceProviderMock.Object);



        var service = new TaskService(
            unitOfWorkMock.Object,
            mapper,
            userAccessorMock.Object,
           validationService
        );


        return (dbContext, service);
    }

    [Fact]
    public async Task CreateTaskAsync_ValidData_ReturnsSuccessResult()
    {
        // Arrange
        var (dbContext, taskService) = await BuildTestContextAndService<CreateTaskDto>();
        var createTaskDto = new CreateTaskDto
        {
            Title = "New Task Title",
            Description = "New Task Description",
            DueDate = DateTime.UtcNow.AddDays(5),
            Priority = Priority.High,
            AssignedUserId = "user-1"
        };


        // Act
        var result = await taskService.CreateAsync(createTaskDto, default);


        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        var createdTask = await dbContext.Tasks.FindAsync(result.Value);
        Assert.NotNull(createdTask);
        Assert.Equal(createTaskDto.Title, createdTask.Title);
        Assert.Equal(createTaskDto.Description, createdTask.Description);
        Assert.Equal(createTaskDto.DueDate, createdTask.DueDate);
        Assert.Equal(createTaskDto.Priority, createdTask.Priority);
        Assert.Equal(TaskState.InProgress, createdTask.Status);
    }


}

