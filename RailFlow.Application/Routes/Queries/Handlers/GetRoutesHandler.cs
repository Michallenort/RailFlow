using MediatR;
using RailFlow.Application.Routes.DTO;
using Railflow.Core.Entities;
using Railflow.Core.Pagination;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Routes.Queries.Handlers;

internal sealed class GetRoutesHandler : IRequestHandler<GetRoutes, PagedList<RouteDto>>
{
    private readonly IRouteRepository _routeRepository;
    private readonly IRouteMapper _routeMapper;
    
    public GetRoutesHandler(IRouteRepository routeRepository, IRouteMapper routeMapper)
    {
        _routeRepository = routeRepository;
        _routeMapper = routeMapper;
    }
    
    public async Task<PagedList<RouteDto>> Handle(GetRoutes request, CancellationToken cancellationToken)
    {
        IEnumerable<Route> routes;
        
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            routes = await _routeRepository.GetBySearchTermAsync(request.SearchTerm);
        }
        else
        {
            routes = await _routeRepository.GetAllAsync();
        }
        
        var pagedRoutes = PagedList<RouteDto>
            .Create(_routeMapper.MapRouteDtos(routes), request.Page, request.PageSize);
        
        return pagedRoutes;
    }
}