using MediatR;
using RailFlow.Application.Exceptions;
using RailFlow.Application.Routes.DTO;
using RailFlow.Application.Stops;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Routes.Queries.Handlers;

internal sealed class GetRouteDetailsHandler : IRequestHandler<GetRouteDetails, RouteDetailsDto>
{
    private readonly IRouteRepository _routeRepository;
    private readonly IStopRepository _stopRepository;
    private readonly IRouteMapper _routeMapper;
    
    public GetRouteDetailsHandler(IRouteRepository routeRepository, IStopRepository stopRepository, IRouteMapper routeMapper)
    {
        _routeRepository = routeRepository;
        _stopRepository = stopRepository;
        _routeMapper = routeMapper;
    }
    
    public async Task<RouteDetailsDto> Handle(GetRouteDetails request, CancellationToken cancellationToken)
    {
        var route = await _routeRepository.GetByIdAsync(request.Id);

        if (route is null)
        {
            throw new RouteNotFoundException(request.Id);
        }
        
        var stops = await _stopRepository.GetByRouteIdAsync(route.Id);
        stops = stops.OrderBy(x => x.ArrivalHour).ToList();

        return _routeMapper.MapRouteDetailsDto(route, stops);
    }
}