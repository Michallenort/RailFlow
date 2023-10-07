using MediatR;
using RailFlow.Application.Routes.DTO;

namespace RailFlow.Application.Routes.Commands;

public record UpdateRoute(Guid Id, UpdateRouteDto Route) : IRequest;