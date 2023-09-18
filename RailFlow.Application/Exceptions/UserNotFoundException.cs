using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

public class UserNotFoundException : CustomException
{
    public Guid Id { get; set; }
    
    public UserNotFoundException(Guid id) : base($"User with id: '{id}' does not exists.")
    {
        Id = id;
    }
}