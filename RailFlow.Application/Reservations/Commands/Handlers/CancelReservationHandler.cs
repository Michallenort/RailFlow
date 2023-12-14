using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Reservations.Commands.Handlers;

internal sealed class CancelReservationHandler : IRequestHandler<CancelReservation>
{
    private readonly IReservationRepository _reservationRepository;
    
    public CancelReservationHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }
    
    public async Task Handle(CancelReservation request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.GetByIdAsync(request.Id);
        
        if (reservation is null)
        {
            throw new ReservationNotFoundException(request.Id);
        }

        if (reservation.Date <= DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ReservationCannotBeCancelledException(request.Id);
        }
        
        await _reservationRepository.RemoveAsync(reservation);
    }
}