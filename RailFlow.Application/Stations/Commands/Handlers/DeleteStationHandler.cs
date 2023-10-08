using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Stations.Commands.Handlers;

internal sealed class DeleteStationHandler : IRequestHandler<DeleteStation>
{
    private readonly IStationRepository _stationRepository;
    private readonly IRouteRepository _routeRepository;
    
    public DeleteStationHandler(IStationRepository stationRepository, IRouteRepository routeRepository)
    {
        _stationRepository = stationRepository;
        _routeRepository = routeRepository;
    }
    
    public async Task Handle(DeleteStation request, CancellationToken cancellationToken)
    {
        var station = await _stationRepository.GetByIdAsync(request.Id);
        
        if (station is null)
        {
            throw new StationNotFoundException(request.Id);
        }

        var routes = await _routeRepository.GetAllAsync();
        
        var routesWithStation = routes.Where(r => r.StartStationId == station.Id 
                                                  || r.EndStationId == station.Id);


        foreach (var route in routesWithStation)
        {
            if (route.IsActive)
            {
                throw new RouteIsActiveException(route.Id);
            }        
        }
        
        await _stationRepository.DeleteAsync(station);
    }
}