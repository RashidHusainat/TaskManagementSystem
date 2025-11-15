
namespace TaskManagementSystem.Application.Tasks.Validators;

public class ChangeTaskStatusDtoValidator : AbstractValidator<ChangeTaskStatusDto>
{
    public ChangeTaskStatusDtoValidator()
    {
        RuleFor(x => x.TaskStatus)
      .IsInEnum()
      .WithMessage("The Task Status field should be a valid enum value.")
      .NotEmpty()
      .WithMessage("The Task Status field is required.");
    }
}

