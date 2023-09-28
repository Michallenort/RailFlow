using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class TrainAssignedException : CustomException
{
    public Guid Id { get; set; }
    
    public TrainAssignedException(Guid id) : base($"Train with id: '{id}' is already assigned to a route.")
    {
        Id = id;
    }
}