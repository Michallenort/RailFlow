using MediatR;
using RailFlow.Application.Reservations.DTO;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Reservations.Queries.Handlers;

internal sealed class GetReservationsForUserHandler : IRequestHandler<GetReservationsForUser, IEnumerable<ReservationDto>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IReservationMapper _reservationMapper;
    
    public GetReservationsForUserHandler(IReservationRepository reservationRepository, IReservationMapper reservationMapper)
    {
        _reservationRepository = reservationRepository;
        _reservationMapper = reservationMapper;
    }
    
    public async Task<IEnumerable<ReservationDto>> Handle(GetReservationsForUser request, CancellationToken cancellationToken)
    {
        var reservations = await _reservationRepository.GetByUserIdAsync(request.UserId);
        return _reservationMapper.MapReservationDtos(reservations);
    }
}