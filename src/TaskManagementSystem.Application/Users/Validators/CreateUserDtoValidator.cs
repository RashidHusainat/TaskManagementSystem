

namespace TaskManagementSystem.Application.Users.Validators;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("The Email field is required.")
            .MaximumLength(25).WithMessage("User Name should be less than 25 characters.")
            .MinimumLength(6).WithMessage("User Name should be greater than 6 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("The Email field is required.")
            .EmailAddress().WithMessage("Please enter valid email address.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("The password field is required.")
            .MaximumLength(25).WithMessage("Password should be less than 25 characters.")
            .MinimumLength(8).WithMessage("Password should be greater than 8 characters.");

        RuleFor(x => x.PasswordConfirmation)
            .NotEmpty().WithMessage("The password confirmation field is required.")
            .Equal(p=>p.Password).WithMessage("Password and Password Confirmation don't match.");
    }
}
