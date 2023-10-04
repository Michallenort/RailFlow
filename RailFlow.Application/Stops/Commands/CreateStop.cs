using MediatR;

namespace RailFlow.Application.Stops.Commands;

public record CreateStop(TimeOnly ArrivalHour, TimeOnly DepartureHour,
    Guid StationId, Guid RouteId) : IRequest;