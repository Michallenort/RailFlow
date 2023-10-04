using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class RouteNotFoundException : CustomException
{
    public Guid Id { get; set; }
    public RouteNotFoundException(Guid id) : base($"Route with id: '{id}' does not exists.")
    {
        Id = id;
    }
}