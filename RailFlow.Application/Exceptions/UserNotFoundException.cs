using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class UserNotFoundException : NotFoundException
{
    public Guid? Id { get; set; }
    public string Email { get; set; }
    
    public UserNotFoundException(Guid? id) : base($"User with id: '{id!.Value}' does not exists.")
    {
        Id = id;
    }
    
    public UserNotFoundException(string email) : base($"User with email: '{email}' does not exists.")
    {
        Email = email;
    }
}