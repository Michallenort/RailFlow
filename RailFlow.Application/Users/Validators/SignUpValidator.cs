using FluentValidation;
using RailFlow.Application.Users.Commands;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Users.Validators;

internal sealed class SignUpValidator : AbstractValidator<SignUp>
{
    public SignUpValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name should not be empty.")
            .MinimumLength(2)
            .WithMessage("First name should have more than 1 char.")
            .MaximumLength(25)
            .WithMessage("First name should have less than 25 chars.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name should not be empty.")
            .MinimumLength(2)
            .WithMessage("Last name should have more than 1 char.")
            .MaximumLength(25)
            .WithMessage("Last name should have less than 25 chars.");
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email should not be empty.")
            .EmailAddress()
            .WithMessage("Email is not valid");

        RuleFor(x => x.Password)
            .MinimumLength(8)
            .WithMessage("Password should have more than 8 chars.");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage("Passwords are different.");
        
        RuleFor(x => x.DateOfBirth)
            .Custom((value, context) =>
            {
                if (value >= DateTime.Today)
                {
                    context.AddFailure("DateOfBirth", "Date of birth should have place before today!");
                }
            });
    }
}