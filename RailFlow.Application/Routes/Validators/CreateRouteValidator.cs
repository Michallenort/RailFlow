using FluentValidation;
using RailFlow.Application.Routes.Commands;

namespace RailFlow.Application.Routes.Validators;

internal sealed class CreateRouteValidator : AbstractValidator<CreateRoute>
{
    public CreateRouteValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name should not be empty.")
            .MaximumLength(25)
            .WithMessage("Name should have less than 25 chars.");
        
        RuleFor(x => x.StartStationName)
            .NotEmpty()
            .WithMessage("Start station should not be empty.")
            .NotEqual(x => x.EndStationName)
            .WithMessage("Start station should not be equal to end station.");
            
        
        RuleFor(x => x.EndStationName)
            .NotEmpty()
            .WithMessage("End station should not be empty.");
        
        RuleFor(x => x.TrainNumber)
            .NotEmpty()
            .WithMessage("Train number should not be empty.");
    }
}