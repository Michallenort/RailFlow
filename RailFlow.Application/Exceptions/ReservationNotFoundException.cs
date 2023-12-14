using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class ReservationNotFoundException : NotFoundException
{
    public Guid Id { get; set; }
    
    public ReservationNotFoundException(Guid id) : base($"Reservation with id: {id} was not found.")
    {
        Id = id;
    }
}