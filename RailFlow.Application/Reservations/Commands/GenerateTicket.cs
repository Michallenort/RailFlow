using MediatR;
using RailFlow.Application.Reservations.DTO;

namespace RailFlow.Application.Reservations.Commands;

public record GenerateTicket(Guid ReservationId) : IRequest<string>;