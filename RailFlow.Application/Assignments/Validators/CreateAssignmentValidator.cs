using FluentValidation;
using RailFlow.Application.Assignments.Commands;

namespace RailFlow.Application.Assignments.Validators;

internal sealed class CreateAssignmentValidator : AbstractValidator<CreateAssignment>
{
    public CreateAssignmentValidator()
    {
        RuleFor(x => x.UserEmail)
            .NotEmpty()
            .WithMessage("User email should not be empty.")
            .EmailAddress()
            .WithMessage("User email should be a valid email address.");
        
        RuleFor(x => x.RouteName)
            .NotEmpty()
            .WithMessage("Route name should not be empty.");
        
        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Date should not be empty.");
        
        RuleFor(x => x.StartHour)
            .NotEmpty()
            .WithMessage("Start hour should not be empty.");
        
        RuleFor(x => x.EndHour)
            .NotEmpty()
            .WithMessage("End hour should not be empty.")
            .GreaterThan(x => x.StartHour)
            .WithMessage("End hour should be greater than start hour.");
    }
}