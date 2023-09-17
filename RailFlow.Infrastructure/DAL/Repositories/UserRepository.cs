using Microsoft.EntityFrameworkCore;
using Railflow.Core.Entities;
using Railflow.Core.Repositories;

namespace RailFlow.Infrastructure.DAL.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly DbSet<User> _users;
    
    public UserRepository(TrainDbContext dbContext)
    {
        _users = dbContext.Users;
    }

    public async Task<User?> GetByEmailAsync(string? email)
        => await _users.SingleOrDefaultAsync(x => x.Email == email); 

    public async Task AddAsync(User user)
        => await _users.AddAsync(user);
}