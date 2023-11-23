using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class AssignmentNotFoundException : CustomException
{
    public Guid Id { get; set; }
    
    public AssignmentNotFoundException(Guid id) : base($"Assignment with id: '{id}' does not exists.")
    {
        Id = id;
    }
}