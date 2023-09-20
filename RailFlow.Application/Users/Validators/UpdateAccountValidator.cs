using FluentValidation;
using RailFlow.Application.Users.Commands;

namespace RailFlow.Application.Users.Validators;

public class UpdateAccountValidator : AbstractValidator<UpdateAccount>
{
    public UpdateAccountValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email is not valid");
        
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