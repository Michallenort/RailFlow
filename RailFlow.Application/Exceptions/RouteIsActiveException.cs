using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class RouteIsActiveException : CustomException
{
    public Guid Id { get; set; }
    
    public RouteIsActiveException(Guid id) : base($"Route with id: '{id}' is active.")
    {
        Id = id;
    }
}