using MediatR;

namespace RailFlow.Application.Users.Commands;

public record SignIn(string Email, string Password) : IRequest;