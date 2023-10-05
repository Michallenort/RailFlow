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

    public async Task<Stop?> GetByIdAsync(Guid id)
        => await _stops.SingleOrDefaultAsync(stop => stop.Id == id);

    public async Task<IEnumerable<Stop>> GetByRouteIdAsync(Guid routeId)
        => await _stops.Where(s => s.RouteId == routeId)
            .Include(x => x.Station)
            .ToListAsync();

    public async Task<Stop?> GetByRouteIdAndStationIdAsync(Guid routeId, Guid stationId)
        => await _stops.SingleOrDefaultAsync(s => s.RouteId == routeId && s.StationId == stationId);

    public async Task AddAsync(Stop stop)
        => await _stops.AddAsync(stop);

    public async Task AddRangeAsync(List<Stop> stops)
        => await _stops.AddRangeAsync(stops);

    public async Task UpdateAsync(Stop stop)
        => await Task.Run(() => _stops.Update(stop));

    public async Task DeleteAsync(Stop stop)
        => await Task.Run(() => _stops.Remove(stop));
}