using MediatR;
using RailFlow.Application.Connections.DTO;
using Railflow.Core.Repositories;
using Railflow.Core.ValueObjects;

namespace RailFlow.Application.Connections.Queries.Handlers;

internal class GetConnectionsHandler : IRequestHandler<GetConnections, IEnumerable<ConnectionDto>>
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IConnectionMapper _connectionMapper;
    
    public GetConnectionsHandler(IScheduleRepository scheduleRepository, IConnectionMapper connectionMapper)
    {
        _scheduleRepository = scheduleRepository;
        _connectionMapper = connectionMapper;
    }
    
    public async Task<IEnumerable<ConnectionDto>> Handle(GetConnections request, CancellationToken cancellationToken)
    {
        var schedulesWithTransfer = new List<Connection>();
        
        var schedules = await _scheduleRepository.GetByDateAsync(request.Date);

        var schedulesWithStartAndEndStation = schedules
            .Where(schedule => schedule.Route.Stops.Any(stop => stop.Station.Name == request.StartStation) &&
                               schedule.Route.Stops.Any(stop => stop.Station.Name == request.EndStation) &&
                               schedule.Route.Stops.FirstOrDefault(stop => stop.Station.Name == request.StartStation)!
                                   .DepartureHour <
                               schedule.Route.Stops.FirstOrDefault(stop => stop.Station.Name == request.EndStation)!
                                   .ArrivalHour).ToList();

        if (schedulesWithStartAndEndStation.Any())
        {
            schedulesWithTransfer.AddRange(schedulesWithStartAndEndStation.Select(schedule => new Connection(
                new List<SubConnection>()
                {
                    new SubConnection(schedule, schedule.Route.Stops.Where(x => x.ArrivalHour > schedule.Route.Stops
                            .FirstOrDefault(stop => stop.Station.Name == request.StartStation)!.DepartureHour && 
                        x.DepartureHour < schedule.Route.Stops.FirstOrDefault(stop => stop.Station.Name == request.EndStation)!
                            .ArrivalHour)),
                },request.StartStation, request.EndStation, 
                schedule.Route.Stops.FirstOrDefault(x => x.Station.Name == request.StartStation).ArrivalHour,
                schedule.Route.Stops.FirstOrDefault(x => x.Station.Name == request.EndStation).DepartureHour,
                schedule.Route.Stops.Count(x => x.ArrivalHour > schedule.Route.Stops
                                                       .FirstOrDefault(stop => stop.Station.Name == request.StartStation)!.DepartureHour && 
                                                   x.DepartureHour < schedule.Route.Stops.FirstOrDefault(stop => stop.Station.Name == request.EndStation)!
                                                       .ArrivalHour) * 2)));
        }

        var schedulesWithStartStation = schedules
            .Where(schedule => schedule.Route.Stops.Any(stop => stop.Station.Name == request.StartStation)
            && schedule.Route.Stops.All(stop => stop.Station.Name != request.EndStation))
            .ToList();

        var schedulesWithEndStation = schedules
            .Where(schedule => schedule.Route.Stops.Any(stop => stop.Station.Name == request.EndStation)
            && schedule.Route.Stops.All(stop => stop.Station.Name != request.StartStation))
            .ToList();

        foreach (var scheduleWithStartStation in schedulesWithStartStation)
        {
            var startStop = scheduleWithStartStation.Route.Stops.
                FirstOrDefault(stop => stop.Station.Name == request.StartStation);
            
            foreach (var stopStartStation in scheduleWithStartStation.Route.Stops.
                         Where(stop => stop.ArrivalHour > startStop!.DepartureHour).OrderBy(x => x.ArrivalHour).ToList())
            {
                var transferedSchedules = schedulesWithEndStation
                    .Where(x => x.Route.Stops.Any(stop => stop.Station.Name == stopStartStation.Station.Name)).ToList();

                foreach (var transferedSchedule in transferedSchedules)
                {
                    var transferStop = transferedSchedule.Route.Stops.
                        FirstOrDefault(stop => stop.Station.Name == stopStartStation.Station.Name);

                    if (transferStop != null && transferStop.DepartureHour > stopStartStation.ArrivalHour)
                    {
                        var startConnection = new SubConnection(scheduleWithStartStation,
                            scheduleWithStartStation.Route.Stops.Where(stop =>
                                stop.ArrivalHour >= startStop!.ArrivalHour && 
                                stop.ArrivalHour <= stopStartStation.ArrivalHour).OrderBy(x => x.ArrivalHour));
                        
                        var transferConnection = new SubConnection(transferedSchedule,
                            transferedSchedule.Route.Stops.Where(stop =>
                                stop.ArrivalHour >= transferStop!.ArrivalHour &&
                                transferedSchedule.Route.Stops.FirstOrDefault(stop1 => stop1.Station.Name == request.EndStation)!
                                    .ArrivalHour >= stop.ArrivalHour).OrderBy(x => x.ArrivalHour));
                        
                        schedulesWithTransfer.Add(new Connection(
                            new List<SubConnection>() {startConnection, transferConnection},
                            request.StartStation, request.EndStation, 
                            startConnection.Stops.FirstOrDefault(x => x.Station.Name == request.StartStation).ArrivalHour,
                            transferConnection.Stops.FirstOrDefault(x => x.Station.Name == request.EndStation).DepartureHour,
                            (startConnection.Stops.Count() + transferConnection.Stops.Count()) * 2));
                    }
                }
            }
        }

        return _connectionMapper.MapConnectionDtos(schedulesWithTransfer);
    }
}