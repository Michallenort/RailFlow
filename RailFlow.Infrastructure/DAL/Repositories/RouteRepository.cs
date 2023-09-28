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
    
    public async Task<Route?> GetByNameAsync(string name)
        => await _routes.SingleOrDefaultAsync(route => route.Name == name);

    public async Task AddAsync(Route route)
        => await _routes.AddAsync(route);
}