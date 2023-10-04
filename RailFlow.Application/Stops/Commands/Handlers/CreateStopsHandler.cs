using MediatR;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Stops.Commands.Handlers;

internal sealed class CreateStopsHandler : IRequestHandler<CreateStops>
{
    private readonly IStopRepository _stopRepository;
    private readonly IStationRepository _stationRepository;
    private readonly IRouteRepository _routeRepository;
    private readonly IStopMapper _stopMapper;
    
    public CreateStopsHandler(IStopRepository stopRepository, IStationRepository stationRepository, 
        IRouteRepository routeRepository, IStopMapper stopMapper)
    {
        _stopRepository = stopRepository;
        _stationRepository = stationRepository;
        _routeRepository = routeRepository;
        _stopMapper = stopMapper;
    }
    
    public async Task Handle(CreateStops request, CancellationToken cancellationToken)
    {
        var stops = _stopMapper.MapStops(request.Stops).ToList();

        var allStations = await _stationRepository.GetAllAsync();
        var allStationsIds = allStations.Select(x => x.Id).ToList();
        var allRoutes = await _routeRepository.GetAllAsync();
        var allRoutesIds = allRoutes.Select(x => x.Id).ToList();

        stops = stops.Where(stop => allStationsIds.Contains(stop.StationId) && allRoutesIds.Contains(stop.RouteId)).ToList();
        //stops = stops.Where(stop => !(allStopsStationIds.Contains(stop.StationId) && allStopsRouteIds.Contains(stop.RouteId))).ToList();
        
        await _stopRepository.AddRangeAsync(stops);
    }
}