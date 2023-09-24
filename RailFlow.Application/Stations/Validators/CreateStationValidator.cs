using FluentValidation;
using RailFlow.Application.Stations.Commands;

namespace RailFlow.Application.Stations.Validators;

public class CreateStationValidator : AbstractValidator<CreateStation>
{
    public CreateStationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Country)
            .NotEmpty()
            .MaximumLength(25);

        RuleFor(x => x.City)
            .NotEmpty()
            .MaximumLength(25);

        RuleFor(x => x.Street)
            .NotEmpty()
            .MaximumLength(50);
    }
}