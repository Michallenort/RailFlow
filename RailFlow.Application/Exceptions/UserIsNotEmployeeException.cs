using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class UserIsNotEmployeeException : CustomException
{
    string Email { get; set; }
    public UserIsNotEmployeeException(string email) : base($"User with id: '{email}' is not an employee.")
    {
        Email = email;
    }
}