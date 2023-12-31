using Railflow.Core.Entities;

namespace Railflow.Core.Repositories;

public interface IRouteRepository
{
    Task<IEnumerable<Route>> GetAllAsync();
    Task<IEnumerable<Route>> GetBySearchTermAsync(string searchTerm);
    Task<Route?> GetByIdAsync(Guid id);
    Task<Route?> GetByNameAsync(string name);
    Task AddAsync(Route route);
    Task UpdateAsync(Route route);
    Task DeleteAsync(Route route);
}