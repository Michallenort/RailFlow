using MediatR;
using RailFlow.Application.Stops.DTO;

namespace RailFlow.Application.Stops.Queries;

public record GetStops(Guid RouteId) : IRequest<IEnumerable<StopDto>>;