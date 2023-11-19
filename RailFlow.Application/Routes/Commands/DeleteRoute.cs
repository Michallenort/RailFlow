using MediatR;

namespace RailFlow.Application.Routes.Commands;

public record DeleteRoute(Guid Id) : IRequest;