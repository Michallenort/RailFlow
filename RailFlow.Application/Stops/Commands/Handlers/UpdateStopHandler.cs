using System.Windows.Markup;
using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Stops.Commands.Handlers;

internal sealed class UpdateStopHandler : IRequestHandler<UpdateStop>
{
    private readonly IStopRepository _stopRepository;

    public UpdateStopHandler(IStopRepository stopRepository)
    {
        _stopRepository = stopRepository;
    }

    public async Task Handle(UpdateStop request, CancellationToken cancellationToken)
    {
        var stop = await _stopRepository.GetByIdAsync(request.Id);
        
        if (stop is null)
        {
            throw new StopNotFoundException(request.Id);
        }
        
        var newArrivalHour = request.Stop.ArrivalHour ?? stop.ArrivalHour;
        var newDepartureHour = request.Stop.DepartureHour ?? stop.DepartureHour;
        var newStationId = request.Stop.StationId ?? stop.StationId;

        if (newArrivalHour > newDepartureHour)
        {
            throw new StopTimeConflictException();
        }
        
        var stops = await _stopRepository.GetByRouteIdAsync(stop.RouteId);
        stops = stops.Where(x => x.Id != request.Id).ToList();
        
        if (stops.Any(x => 
                (x.ArrivalHour > newArrivalHour && x.DepartureHour < newDepartureHour) ||
                (x.ArrivalHour < newArrivalHour && x.DepartureHour > newDepartureHour) ||
                (x.ArrivalHour > newArrivalHour && x.ArrivalHour < newDepartureHour) ||
                (x.DepartureHour > newArrivalHour && x.DepartureHour < newDepartureHour))
           )
        {
            throw new StopTimeConflictException();
        }
        
        stop.Update(newArrivalHour, newDepartureHour, newStationId);
        await _stopRepository.UpdateAsync(stop);
    }
}