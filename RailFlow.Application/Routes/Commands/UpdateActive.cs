using MediatR;

namespace RailFlow.Application.Routes.Commands;

public record UpdateActive(Guid Id) : IRequest;