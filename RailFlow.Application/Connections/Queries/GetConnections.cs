using MediatR;
using RailFlow.Application.Connections.DTO;

namespace RailFlow.Application.Connections.Queries;

public record GetConnections(string StartStation, string EndStation, DateOnly Date) : IRequest<IEnumerable<ConnectionDto>>;