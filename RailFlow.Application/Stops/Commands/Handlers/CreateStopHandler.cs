using FluentValidation;
using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Entities;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Stops.Commands.Handlers;

internal sealed class CreateStopHandler : IRequestHandler<CreateStop>
{
    private readonly IValidator<CreateStop> _validator;
    private readonly IStopRepository _stopRepository;
    private readonly IStationRepository _stationRepository;
    private readonly IRouteRepository _routeRepository;
    
    public CreateStopHandler(IValidator<CreateStop> validator, IStopRepository stopRepository, 
        IStationRepository stationRepository, IRouteRepository routeRepository)
    {
        _validator = validator;
        _stopRepository = stopRepository;
        _stationRepository = stationRepository;
        _routeRepository = routeRepository;
    }
    
    public async Task Handle(CreateStop request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        var station = await _stationRepository.GetByNameAsync(request.StationName);
        
        if (station is null)
        {
            throw new StationNotFoundException(request.StationName);
        }
        
        if (await _stopRepository.GetByRouteIdAndStationIdAsync(request.RouteId, station.Id) is not null)
        {
            throw new StopExistsException(request.RouteId);
        }
        
        if (await _routeRepository.GetByIdAsync(request.RouteId) is null)
        {
            throw new RouteNotFoundException(request.RouteId);
        }
        
        var stops = await _stopRepository.GetByRouteIdAsync(request.RouteId);
        
        if (stops.Any(stop => 
                (stop.ArrivalHour > request.ArrivalTime && stop.DepartureHour < request.DepartureTime) ||
                (stop.ArrivalHour < request.ArrivalTime && stop.DepartureHour > request.DepartureTime) ||
                (stop.ArrivalHour > request.ArrivalTime && stop.ArrivalHour < request.DepartureTime) ||
                (stop.DepartureHour > request.ArrivalTime && stop.DepartureHour < request.DepartureTime))
            )
        {
            throw new StopTimeConflictException();
        }
        
        var stop = new Stop(Guid.NewGuid(), request.ArrivalTime, request.DepartureTime, station.Id, request.RouteId);
        
        await _stopRepository.AddAsync(stop);
    }
}