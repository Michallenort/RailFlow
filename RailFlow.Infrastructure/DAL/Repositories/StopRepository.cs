using Microsoft.EntityFrameworkCore;
using Railflow.Core.Entities;
using Railflow.Core.Repositories;

namespace RailFlow.Infrastructure.DAL.Repositories;

internal sealed class StopRepository : IStopRepository
{
    private readonly DbSet<Stop> _stops;

    public StopRepository(TrainDbContext dbContext)
    {
        _stops = dbContext.Stops;
    }

    public async Task<IEnumerable<Stop>> GetAllAsync()
        => await _stops.ToListAsync();

    public async Task<IEnumerable<Stop>> GetByRouteIdAsync(Guid routeId)
        => await _stops.Where(s => s.RouteId == routeId).ToListAsync();

    public async Task<Stop?> GetByRouteIdAndStationIdAsync(Guid routeId, Guid stationId)
        => await _stops.SingleOrDefaultAsync(s => s.RouteId == routeId && s.StationId == stationId);

    public async Task AddAsync(Stop stop)
        => await _stops.AddAsync(stop);

    public async Task AddRangeAsync(List<Stop> stops)
        => await _stops.AddRangeAsync(stops);
}