using FluentValidation;
using RailFlow.Application.Reservations.Commands;

namespace RailFlow.Application.Reservations.Validators;

internal sealed class AddReservationValidator : AbstractValidator<AddReservation>
{
    public AddReservationValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Date should not be empty.")
            .Custom((value, context) =>
            {
                if (value < DateOnly.FromDateTime(DateTime.Today))
                {
                    context.AddFailure("Date", 
                        "It is not possible to make a reservation for a past date.");
                }
            });
        
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User id should not be empty.");
        
        RuleFor(x => x.FirstScheduleId)
            .NotEmpty()
            .WithMessage("First schedule id should not be empty.");
        
        RuleFor(x => x.StartStopId)
            .NotEmpty()
            .WithMessage("Start stop id should not be empty.");
        
        RuleFor(x => x.EndStopId)
            .NotEmpty()
            .WithMessage("End stop id should not be empty.");
        
        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage("Price should not be empty.")
            .GreaterThan(0)
            .WithMessage("Price should be greater than 0.");
    }
}