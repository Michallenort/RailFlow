using MediatR;

namespace RailFlow.Application.Users.Commands;

public record CreateUser(string Email, string FirstName, string LastName, DateTime? DateOfBirth,
    string Password, string Nationality, int RoleId) : IRequest;