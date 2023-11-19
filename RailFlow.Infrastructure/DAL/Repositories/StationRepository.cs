using Microsoft.EntityFrameworkCore;
using Railflow.Core.Entities;
using Railflow.Core.Repositories;

namespace RailFlow.Infrastructure.DAL.Repositories;

internal sealed class StationRepository : IStationRepository
{
    private readonly DbSet<Station> _stations;

    public StationRepository(TrainDbContext dbContext)
    {
        _stations = dbContext.Stations;
    }

    public async Task<IEnumerable<Station>> GetAllAsync()
        => await _stations
            .AsNoTracking()
            .ToListAsync();

    public async Task<IEnumerable<Station>> GetBySearchTermAsync(string searchTerm)
        => await _stations
            .AsNoTracking()
            .Where(station => station.Name.ToLower().Contains(searchTerm.ToLower()) || 
                              station.Address.City.ToLower().Contains(searchTerm.ToLower()))
            .ToListAsync();

    public async Task<Station?> GetByIdAsync(Guid id)
        => await _stations
            .Include(x => x.Stops)
                .ThenInclude(x=> x.Route)
                    .ThenInclude(x=> x.StartStation)
            .Include(x => x.Stops)
                .ThenInclude(x=> x.Route)
                    .ThenInclude(x=> x.EndStation)
            .Include(x => x.Stops)
                .ThenInclude(x=> x.Route)
                    .ThenInclude(x=> x.Train)
            .SingleOrDefaultAsync(station => station.Id == id);

    public async Task<Station?> GetByNameAsync(string name)
        => await _stations.SingleOrDefaultAsync(station => station.Name == name);

    public async Task AddAsync(Station station)
        => await _stations.AddAsync(station);

    public async Task AddRangeAsync(List<Station> stations)
        => await _stations.AddRangeAsync(stations);

    public async Task DeleteAsync(Station station)
        => await Task.Run(() => _stations.Remove(station));
}