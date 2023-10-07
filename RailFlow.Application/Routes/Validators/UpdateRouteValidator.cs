using FluentValidation;
using RailFlow.Application.Routes.Commands;

namespace RailFlow.Application.Routes.Validators;

internal sealed class UpdateRouteValidator : AbstractValidator<UpdateRoute>
{
    public UpdateRouteValidator()
    {
        RuleFor(x => x.Route.Name)
            .MaximumLength(25)
            .WithMessage("Name should have less than 25 chars.");
        
        RuleFor(x => x.Route.StartStationId)
            .Custom((value, context) =>
            {
                var instance = context.InstanceToValidate;
                if (value is not null && instance.Route.EndStationId is not null && 
                    value == instance.Route.EndStationId)
                {
                    context.AddFailure("StartStationId", "Start station should not be equal to end station.");
                }
            });
    }
}