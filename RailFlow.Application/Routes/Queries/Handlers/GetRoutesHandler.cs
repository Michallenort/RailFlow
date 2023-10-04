using MediatR;
using RailFlow.Application.Routes.DTO;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Routes.Queries.Handlers;

internal sealed class GetRoutesHandler : IRequestHandler<GetRoutes, IEnumerable<RouteDto>>
{
    private readonly IRouteRepository _routeRepository;
    private readonly IRouteMapper _routeMapper;
    
    public GetRoutesHandler(IRouteRepository routeRepository, IRouteMapper routeMapper)
    {
        _routeRepository = routeRepository;
        _routeMapper = routeMapper;
    }
    
    public async Task<IEnumerable<RouteDto>> Handle(GetRoutes request, CancellationToken cancellationToken)
    {
        var routes = await _routeRepository.GetAllAsync();
        return _routeMapper.MapRouteDtos(routes);
    }
}