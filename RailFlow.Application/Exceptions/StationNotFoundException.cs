using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class StationNotFoundException : NotFoundException
{
    public Guid Id { get; set; }
    
    public StationNotFoundException(Guid id) : base($"Station with id: '{id}' does not exists.")
    {
        Id = id;
    }
}