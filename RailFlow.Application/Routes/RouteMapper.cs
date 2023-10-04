using RailFlow.Application.Routes.DTO;
using Railflow.Core.Entities;

namespace RailFlow.Application.Routes;

internal interface IRouteMapper
{
    IEnumerable<RouteDto> MapRouteDtos(IEnumerable<Route> routes);
}

internal sealed class RouteMapper : IRouteMapper
{
    public IEnumerable<RouteDto> MapRouteDtos(IEnumerable<Route> routes)
        => routes.Select(x => new RouteDto(x.Id, x.Name, x.StartStation.Name, 
            x.EndStation.Name, x.Train.Number));
}