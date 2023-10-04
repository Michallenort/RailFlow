using RailFlow.Application.Stops.DTO;
using Railflow.Core.Entities;

namespace RailFlow.Application.Stops;

internal interface IStopMapper
{ 
    IEnumerable<StopDto> MapStopDto(IEnumerable<Stop> stop);
    IEnumerable<Stop> MapStops(IEnumerable<CreateStopDto> stops);
}

internal sealed class StopMapper : IStopMapper
{
    public IEnumerable<StopDto> MapStopDto(IEnumerable<Stop> stop)
        => stop.Select(x => new StopDto(x.Id, x.ArrivalHour, x.DepartureHour, x.Station.Name));

    public IEnumerable<Stop> MapStops(IEnumerable<CreateStopDto> stops)
        => stops.Select(x => new Stop(Guid.NewGuid(), x.ArrivalHour, x.DepartureHour, x.StationId, x.RouteId));
}