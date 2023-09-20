using MediatR;

namespace RailFlow.Application.Users.Commands;

public record DeleteAccount() : IRequest;