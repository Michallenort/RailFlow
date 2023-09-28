using Railflow.Core.Entities;

namespace Railflow.Core.Repositories;

public interface IRouteRepository
{
    Task<Route?> GetByNameAsync(string name);
    Task AddAsync(Route route);
}