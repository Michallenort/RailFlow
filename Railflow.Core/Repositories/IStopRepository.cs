using Railflow.Core.Entities;

namespace Railflow.Core.Repositories;

public interface IStopRepository
{
    Task<IEnumerable<Stop>> GetAllAsync();
    Task<IEnumerable<Stop>> GetByRouteIdAsync(Guid routeId);
    Task<Stop?> GetByRouteIdAndStationIdAsync(Guid routeId, Guid stationId);
    Task AddAsync(Stop stop);
    Task AddRangeAsync(List<Stop> stops);
}