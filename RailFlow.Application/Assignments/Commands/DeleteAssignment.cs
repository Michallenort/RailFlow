using MediatR;

namespace RailFlow.Application.Assignments.Commands;

public record DeleteAssignment(Guid Id) : IRequest;