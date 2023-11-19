using MediatR;

namespace RailFlow.Application.Stops.Commands;

public record CreateStop(TimeOnly ArrivalTime, TimeOnly DepartureTime,
    string StationName, Guid RouteId) : IRequest;