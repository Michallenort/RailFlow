using MediatR;
using RailFlow.Application.Reservations.DTO;

namespace RailFlow.Application.Reservations.Queries;

public record GetReservationsForUser(Guid UserId) : IRequest<IEnumerable<ReservationDto>>;