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
        
        RuleFor(x => x.StartStationId)
            .NotEmpty()
            .WithMessage("Start station should not be empty.")
            .NotEqual(x => x.EndStationId)
            .WithMessage("Start station should not be equal to end station.");
            
        
        RuleFor(x => x.EndStationId)
            .NotEmpty()
            .WithMessage("End station should not be empty.");
        
        RuleFor(x => x.TrainId)
            .NotEmpty()
            .WithMessage("Train Id should not be empty.");
    }
}