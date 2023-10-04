using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class StopExistsException : CustomException
{
    public Guid RouteId { get; set; }
    public StopExistsException(Guid routeId) : base($"This station exists in route: '{routeId}'.")
    {
        RouteId = routeId;
    }
}