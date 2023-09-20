using MediatR;

namespace RailFlow.Application.Users.Commands;

public record UpdateAccount(string? Email, string? FirstName, string? LastName, DateTime? DateOfBirth) : IRequest;