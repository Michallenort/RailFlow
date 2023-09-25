using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class TrainNotFoundException : NotFoundException
{
    public Guid Id { get; set; }
    
    public TrainNotFoundException(Guid id) : base($"Train with id: '{id}' does not exists.")
    {
        Id = id;
    }
}