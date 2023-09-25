using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public Guid? Id { get; set; }
    
    public UserNotFoundException(Guid? id) : base($"User with id: '{id!.Value}' does not exists.")
    {
        Id = id;
    }
}