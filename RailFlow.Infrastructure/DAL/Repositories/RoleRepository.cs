using Microsoft.EntityFrameworkCore;
using Railflow.Core.Entities;
using Railflow.Core.Repositories;

namespace RailFlow.Infrastructure.DAL.Repositories;

internal sealed class RoleRepository : IRoleRepository
{
    private readonly DbSet<Role> _roles;

    public RoleRepository(TrainDbContext dbContext)
    {
        _roles = dbContext.Roles;
    }

    public async Task<Role?> GetByIdAsync(int id)
        => await _roles.SingleOrDefaultAsync(x => x.Id == id);

}