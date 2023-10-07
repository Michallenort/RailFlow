using Microsoft.EntityFrameworkCore;
using Railflow.Core.Entities;
using Railflow.Core.Repositories;

namespace RailFlow.Infrastructure.DAL.Repositories;

internal sealed class RouteRepository : IRouteRepository
{
    private readonly DbSet<Route> _routes;
    
    public RouteRepository(TrainDbContext dbContext)
    {
        _routes = dbContext.Routes;
    }

    public async Task<IEnumerable<Route>> GetAllAsync()
        => await _routes
            .Include(x => x.StartStation)
            .Include(x => x.EndStation)
            .Include(x => x.Train)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Route?> GetByIdAsync(Guid id)
        => await _routes
            .Include(x => x.StartStation)
            .Include(x => x.EndStation)
            .Include(x => x.Train)
            .Include(x => x.Stops)
            .SingleOrDefaultAsync(route => route.Id == id);

    public async Task<Route?> GetByNameAsync(string name)
        => await _routes.SingleOrDefaultAsync(route => route.Name == name);

    public async Task AddAsync(Route route)
        => await _routes.AddAsync(route);

    public async Task UpdateAsync(Route route)
        => await Task.Run(() => _routes.Update(route));
}