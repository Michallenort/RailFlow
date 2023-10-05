using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class StopNotFoundException : NotFoundException
{
    public Guid Id { get; set; }
    
    public StopNotFoundException(Guid id) : base($"Stop with id: {id} was not found.")
    {
        Id = id;
    }
}