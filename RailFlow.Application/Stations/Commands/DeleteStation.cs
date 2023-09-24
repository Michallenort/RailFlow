using MediatR;

namespace RailFlow.Application.Stations.Commands;

public record DeleteStation(Guid Id) : IRequest;