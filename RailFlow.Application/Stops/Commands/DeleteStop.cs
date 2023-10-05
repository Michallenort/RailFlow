using MediatR;

namespace RailFlow.Application.Stops.Commands;

public record DeleteStop(Guid Id) : IRequest;