using RailFlow.Application.Routes.DTO;
using RailFlow.Application.Stops.DTO;
using Railflow.Core.Entities;

namespace RailFlow.Application.Routes;

internal interface IRouteMapper
{
    IEnumerable<RouteDto> MapRouteDtos(IEnumerable<Route> routes);
    RouteDetailsDto MapRouteDetailsDto(Route route, IEnumerable<Stop> stops);
}

internal sealed class RouteMapper : IRouteMapper
{
    public IEnumerable<RouteDto> MapRouteDtos(IEnumerable<Route> routes)
        => routes.Select(x => new RouteDto(x.Id, x.Name, x.StartStation!.Name, 
            x.EndStation!.Name, x.Train!.Number));
    
    public RouteDetailsDto MapRouteDetailsDto(Route route, IEnumerable<Stop> stops)
        => new RouteDetailsDto(route.Id, route.Name, route.StartStation!.Name,
            route.EndStation!.Name, route.Train!.Number,
            stops.Select(x => 
                new StopDto(x.Id, x.ArrivalHour, x.DepartureHour, x.Station.Name )
            )
        ); 
}