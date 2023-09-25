using FluentValidation;
using RailFlow.Application.Stations.Commands;

namespace RailFlow.Application.Stations.Validators;

public class CreateStationValidator : AbstractValidator<CreateStation>
{
    public CreateStationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Station name must not be empty.")
            .MaximumLength(50)
            .WithMessage("Station name must be less than 50 characters long.");

        RuleFor(x => x.Country)
            .NotEmpty()
            .WithMessage("Country must not be empty.")
            .MaximumLength(25)
            .WithMessage("Country must be less than 25 characters long.");

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City must not be empty.")
            .MaximumLength(25)
            .WithMessage("City must be less than 25 characters long.");

        RuleFor(x => x.Street)
            .NotEmpty()
            .WithMessage("Street must not be empty.")
            .MaximumLength(50)
            .WithMessage("Street must be less than 50 characters long.");
    }
}