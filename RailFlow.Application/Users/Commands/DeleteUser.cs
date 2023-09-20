using MediatR;

namespace RailFlow.Application.Users.Commands;

public record DeleteUser(Guid? Id) : IRequest;