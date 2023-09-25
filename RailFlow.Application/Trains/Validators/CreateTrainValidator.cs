using FluentValidation;
using RailFlow.Application.Trains.Commands;

namespace RailFlow.Application.Trains.Validators;

internal sealed class CreateTrainValidator : AbstractValidator<CreateTrain>
{
    public CreateTrainValidator()
    {
        RuleFor(train => train.Number)
            .NotEmpty()
            .WithMessage("Train number must not be empty.")
            .GreaterThan(99999)
            .WithMessage("Train number must be 6 digits long.")
            .LessThan(1000000)
            .WithMessage("Train number must be 6 digits long.");
        
        RuleFor(train => train.MaxSpeed)
            .GreaterThan(0)
            .WithMessage("Train max speed must be greater than 0.");
        
        RuleFor(train => train.Capacity)
            .NotEmpty()
            .WithMessage("Train capacity must not be empty.")
            .GreaterThan(0)
            .WithMessage("Train capacity must be greater than 0.");
    }
}