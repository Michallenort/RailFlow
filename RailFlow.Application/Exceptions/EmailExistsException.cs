using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

public sealed class EmailExistsException : CustomException
{
    public string Email { get; set; }
    
    public EmailExistsException(string email) : base($"Email: '{email}' already exists.")
    {
        Email = email;
    }
}