using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class RouteExistsException : CustomException
{
    public string RouteName { get; set; }
    
    public RouteExistsException(string routeRouteName) : base($"Station: '{routeRouteName}' already exists.")
    {
        RouteName = routeRouteName;
    }
}