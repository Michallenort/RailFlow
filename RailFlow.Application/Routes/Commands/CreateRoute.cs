using MediatR;

namespace RailFlow.Application.Routes.Commands;

public record CreateRoute(string Name, Guid StartStationId, Guid EndStationId, Guid TrainId) : IRequest;