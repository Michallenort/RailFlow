using RailFlow.Application.Connections.DTO;
using RailFlow.Application.Routes.DTO;
using RailFlow.Application.Schedules.DTO;
using RailFlow.Application.Stops.DTO;
using Railflow.Core.ValueObjects;

namespace RailFlow.Application.Connections;

public interface IConnectionMapper
{
    IEnumerable<ConnectionDto> MapConnectionDtos(IEnumerable<Connection> connections);
}

internal class ConnectionMapper : IConnectionMapper
{
    public IEnumerable<ConnectionDto> MapConnectionDtos(IEnumerable<Connection> connections)
    {
        return connections.Select(x => new ConnectionDto(
            x.SubConnections.Select(y => new SubConnectionDto(
                new ScheduleDto(y.Schedule.Id, y.Schedule.Date, new RouteDto(
                    y.Schedule.Route.Id, y.Schedule.Route.Name, y.Schedule.Route.StartStation!.Name, 
                    y.Schedule.Route.EndStation!.Name, y.Schedule.Route.Train!.Number, y.Schedule.Route.IsActive)),
                y.Stops.Select(z => new StopDto(z.Id, z.ArrivalHour, z.DepartureHour, z.Station.Name))
            )), x.StartStopId, x.StartStationName, x.EndStopId, x.EndStationName, x.StartHour, x.EndHour, x.Price
        ));
    }
}