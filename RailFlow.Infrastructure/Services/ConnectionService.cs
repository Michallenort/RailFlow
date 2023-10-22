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
                         Where(stop => stop.ArrivalHour > startStop!.DepartureHour).ToList())
            {
                var middleStop = schedulesWithEndStation
                    .Where(x => x.Route.Stops.Any(stop => stop.Station.Name == stopStartStation.Station.Name));
            }
        }

        return schedulesWithTransfer;

        // foreach (var schedule in schedulesWithStartStation)
        // {
        //     var route = schedule.Route;
        //     var startStationStop = route.Stops.FirstOrDefault(stop => stop.Station.Name == startStation);
        //     var endStationStop = route.Stops.FirstOrDefault(stop => stop.Station.Name == endStation);
        //     var transferStations = route.Stops
        //         .Where(stop => stop.ArrivalHour > startStationStop!.DepartureHour &&
        //                        stop.DepartureHour < endStationStop!.ArrivalHour &&
        //                        stop.Station.Name != startStation &&
        //                        stop.Station.Name != endStation)
        //         .ToList();
        //
        //     foreach (var transferStation in transferStations)
        //     {
        //         var transferStationStop =
        //             route.Stops.FirstOrDefault(stop => stop.Station.Name == transferStation.Station.Name);
        //         var transferStationSchedule = schedulesWithEndStation
        //             .FirstOrDefault(x => x.Route.Stops.Any(stop => stop.Station.Name == transferStation.Station.Name));
        //         if (transferStationSchedule is null)
        //         {
        //             continue;
        //         }
        //
        //         var transferStationEndStationStop = transferStationSchedule.Route.Stops
        //             .FirstOrDefault(stop => stop.Station.Name == endStation);
        //         if (transferStationEndStationStop is null)
        //         {
        //             continue;
        //         }
        //
        //         if (transferStationStop is null)
        //         {
        //             continue;
        //         }
        //
        //         if (transferStationStop.ArrivalHour > transferStationEndStationStop.ArrivalHour)
        //         {
        //             continue;
        //         }
        //
        //         schedulesWithTransfer.Add(new Connection(schedule.Route, startStationStop, transferStationStop,
        //             transferStationEndStationStop));
        //     }
        // }
    }
}