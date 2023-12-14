using MediatR;

namespace RailFlow.Application.Reservations.Commands;

public record CancelReservation(Guid Id) : IRequest;