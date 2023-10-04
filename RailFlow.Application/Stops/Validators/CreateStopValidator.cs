using FluentValidation;
using RailFlow.Application.Stops.Commands;

namespace RailFlow.Application.Stops.Validators;

internal sealed class CreateStopValidator : AbstractValidator<CreateStop>
{
    public CreateStopValidator()
    {
        RuleFor(x => x.ArrivalHour)
            .NotEmpty()
            .WithMessage("Arrival hour should not be empty.")
            .LessThanOrEqualTo(x => x.DepartureHour)
            .WithMessage("Arrival hour should be less than or equal departure hour.");
        
        RuleFor(x => x.DepartureHour)
            .NotEmpty()
            .WithMessage("Departure hour should not be empty.");
        
        RuleFor(x => x.StationId)
            .NotEmpty()
            .WithMessage("Station Id should not be empty.");
        
        RuleFor(x => x.RouteId)
            .NotEmpty()
            .WithMessage("Route Id should not be empty.");
    }
}