using FluentValidation;
using RailFlow.Application.Stops.Commands;

namespace RailFlow.Application.Stops.Validators;

internal sealed class CreateStopValidator : AbstractValidator<CreateStop>
{
    public CreateStopValidator()
    {
        RuleFor(x => x.ArrivalTime)
            .NotEmpty()
            .WithMessage("Arrival hour should not be empty.")
            .LessThanOrEqualTo(x => x.DepartureTime)
            .WithMessage("Arrival hour should be less than or equal departure hour.");
        
        RuleFor(x => x.DepartureTime)
            .NotEmpty()
            .WithMessage("Departure hour should not be empty.");
        
        RuleFor(x => x.StationName)
            .NotEmpty()
            .WithMessage("Station Name should not be empty.");
        
        RuleFor(x => x.RouteId)
            .NotEmpty()
            .WithMessage("Route Id should not be empty.");
    }
}