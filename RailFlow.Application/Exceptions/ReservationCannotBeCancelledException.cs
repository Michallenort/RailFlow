using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class ReservationCannotBeCancelledException : CustomException
{
    public Guid Id { get; set; }
    public ReservationCannotBeCancelledException(Guid id) : base($"Reservation with id: {id} cannot be cancelled.")
    {
        Id = id;
    }
}