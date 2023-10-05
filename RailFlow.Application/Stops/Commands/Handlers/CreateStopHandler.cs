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
        
        if (await _stopRepository.GetByRouteIdAndStationIdAsync(request.RouteId, request.StationId) is not null)
        {
            throw new StopExistsException(request.RouteId);
        }
        
        if (await _stationRepository.GetByIdAsync(request.StationId) is null)
        {
            throw new StationNotFoundException(request.StationId);
        }
        
        if (await _routeRepository.GetByIdAsync(request.RouteId) is null)
        {
            throw new RouteNotFoundException(request.RouteId);
        }
        
        var stops = await _stopRepository.GetByRouteIdAsync(request.RouteId);
        
        if (stops.Any(stop => 
                (stop.ArrivalHour > request.ArrivalHour && stop.DepartureHour < request.DepartureHour) ||
                (stop.ArrivalHour < request.ArrivalHour && stop.DepartureHour > request.DepartureHour) ||
                (stop.ArrivalHour > request.ArrivalHour && stop.ArrivalHour < request.DepartureHour) ||
                (stop.DepartureHour > request.ArrivalHour && stop.DepartureHour < request.DepartureHour))
            )
        {
            throw new StopTimeConflictException();
        }
        
        var stop = new Stop(Guid.NewGuid(), request.ArrivalHour, request.DepartureHour, request.StationId, request.RouteId);
        
        await _stopRepository.AddAsync(stop);
    }
}