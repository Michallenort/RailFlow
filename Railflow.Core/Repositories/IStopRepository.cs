using Railflow.Core.Entities;

namespace Railflow.Core.Repositories;

public interface IStopRepository
{
    Task<IEnumerable<Stop>> GetAllAsync();
    Task<Stop?> GetByIdAsync(Guid id);
    Task<IEnumerable<Stop>> GetByRouteIdAsync(Guid routeId);
    Task<IEnumerable<Stop>> GetByStationIdAsync(Guid stationId);
    Task<Stop?> GetByRouteIdAndStationIdAsync(Guid routeId, Guid stationId);
    Task AddAsync(Stop stop);
    Task AddRangeAsync(List<Stop> stops);
    Task UpdateAsync(Stop stop);
    Task DeleteAsync(Stop stop);
}