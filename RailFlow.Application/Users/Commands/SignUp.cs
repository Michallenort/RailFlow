using MediatR;

namespace RailFlow.Application.Users.Commands;

public record SignUp(string Email, string FirstName, string LastName, DateTime? DateOfBirth,
    string Password, string ConfirmPassword, string Nationality) : IRequest;