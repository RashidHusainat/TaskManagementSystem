namespace TaskManagementSystem.Application.Tasks.Validators;

public class EditTaskDtoValidator : AbstractValidator<EditTaskDto>
{
    public EditTaskDtoValidator()
    {
        RuleFor(x => x.Title)
       .NotEmpty().WithMessage("The Title field is required.")
       .MaximumLength(50).WithMessage("Title should be less than 50 characters.")
       .MinimumLength(10).WithMessage("Title should be greater than 10 characters.");

        RuleFor(x => x.DueDate)
            .NotEmpty()
            .WithMessage("The due date field is required.");

        RuleFor(x => x.AssignedUserId)
            .NotEmpty()
            .WithMessage("The user id is required.");

        RuleFor(x => x.Priority)
            .IsInEnum()
            .WithMessage("The Priority field should be a valid enum value.")
            .NotEmpty()
            .WithMessage("The Priority field is required.");
    }
}

