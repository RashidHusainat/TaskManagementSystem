namespace TaskManagementSystem.Infrastructure.Persistence.Extensions;

public static class InitialData
{
    public static IEnumerable<User> GetUsers() =>
        new List<User>
        {
            new User{Id="id-user-test1", UserName = "test1@test.com", Email = "test1@test.com"},
            new User{Id="id-user-test2", UserName = "test2@test.com", Email = "test2@test.com"},
            new User{Id="id-user-test3", UserName = "test3@test.com", Email = "test3@test.com"},
            new User{Id="id-user-test4", UserName = "test4@test.com", Email = "test4@test.com"},
            new User{Id="id-user-test5", UserName = "test5@test.com", Email = "test5@test.com"}
        };

    public static IEnumerable<User> GetAdmins() =>
        new List<User>
        {
            new User{ UserName = "admin1@test.com", Email = "admin1@test.com"},
            new User{ UserName = "admin2@test.com", Email = "admin2@test.com"},
        };

    public static IEnumerable<TaskItem> GetTasks() =>
        new List<TaskItem>
    {
        new TaskItem { AssignedUserId = "id-user-test1", Title = "Task1-User1", Description = "Desc", DueDate = DateTime.UtcNow.AddDays(1), Priority = Priority.Medium },
        new TaskItem { AssignedUserId = "id-user-test2", Title = "Task1-User2", Description = "Desc", DueDate = DateTime.UtcNow.AddDays(1), Priority = Priority.High },
        new TaskItem { AssignedUserId = "id-user-test2", Title = "Task2-User2", Description = "Desc", DueDate = DateTime.UtcNow.AddDays(2), Priority = Priority.Low },
        new TaskItem { AssignedUserId = "id-user-test3", Title = "Task1-User3", Description = "Desc", DueDate = DateTime.UtcNow.AddDays(1), Priority = Priority.Medium },
        new TaskItem { AssignedUserId = "id-user-test3", Title = "Task2-User3", Description = "Desc", DueDate = DateTime.UtcNow.AddDays(2), Priority = Priority.High },
        new TaskItem { AssignedUserId = "id-user-test3", Title = "Task3-User3", Description = "Desc", DueDate = DateTime.UtcNow.AddDays(3), Priority = Priority.Low },
    };

    public static string[] GetRoles() => new[] { "User", "Admin" };
}

