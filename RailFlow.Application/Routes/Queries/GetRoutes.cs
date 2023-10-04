using MediatR;
using RailFlow.Application.Routes.DTO;

namespace RailFlow.Application.Routes.Queries;

public record GetRoutes() : IRequest<IEnumerable<RouteDto>>;