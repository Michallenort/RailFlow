using Railflow.Core.Entities;

namespace Railflow.Core.Repositories;

public interface IStationRepository
{
    Task<IEnumerable<Station>> GetAllAsync();
    Task<IEnumerable<Station>> GetBySearchTermAsync(string searchTerm);
    Task<Station?> GetByIdAsync(Guid id);
    Task<Station?> GetByNameAsync(string name);
    Task AddAsync(Station station);
    Task AddRangeAsync(List<Station> stations);
    Task DeleteAsync(Station station);
    
}