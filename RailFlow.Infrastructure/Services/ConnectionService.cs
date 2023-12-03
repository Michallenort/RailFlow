using Microsoft.EntityFrameworkCore;
using Railflow.Core.Services;
using Railflow.Core.ValueObjects;
using RailFlow.Infrastructure.DAL;

namespace RailFlow.Infrastructure.Services;

internal sealed class ConnectionService : IConnectionService
{
    private readonly TrainDbContext _dbContext;
    
    public ConnectionService(TrainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Connection>> FindConnectionAsync(string startStation, string endStation,
        DateOnly date)
    {
        var schedules = await _dbContext.Schedules
            .Where(schedule => schedule.Date == date)
            .Include(x => x.Route)
            .ThenInclude(x => x.Stops)
            .ThenInclude(x =>  x.Station)
            .ToListAsync();

        var schedulesWithStartAndEndStation = schedules
            .Where(schedule => schedule.Route.Stops.Any(stop => stop.Station.Name == startStation) &&
                               schedule.Route.Stops.Any(stop => stop.Station.Name == endStation) &&
                               schedule.Route.Stops.FirstOrDefault(stop => stop.Station.Name == startStation)!
                                   .DepartureHour <
                               schedule.Route.Stops.FirstOrDefault(stop => stop.Station.Name == endStation)!
                                   .ArrivalHour);

        var schedulesWithTransfer = new List<Connection>();

        var schedulesWithStartStation = schedules
            .Where(schedule => schedule.Route.Stops.Any(stop => stop.Station.Name == startStation)
            && schedule.Route.Stops.All(stop => stop.Station.Name != endStation))
            .ToList();

        var schedulesWithEndStation = schedules
            .Where(schedule => schedule.Route.Stops.Any(stop => stop.Station.Name == endStation)
            && schedule.Route.Stops.All(stop => stop.Station.Name != startStation))
            .ToList();

        foreach (var scheduleWithStartStation in schedulesWithStartStation)
        {
            var startStop = scheduleWithStartStation.Route.Stops.
                FirstOrDefault(stop => stop.Station.Name == startStation);
            
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
                                stop.ArrivalHour > startStop!.DepartureHour));
                        
                        var transferConnection = new SubConnection(transferedSchedule,
                            transferedSchedule.Route.Stops.Where(stop =>
                                stop.ArrivalHour > transferStop!.DepartureHour));
                        
                        schedulesWithTransfer.Add(new Connection(
                            new List<SubConnection>() {startConnection, transferConnection}, 0.0f));
                    }
                }
            }
        }

        return schedulesWithTransfer;
    }
}