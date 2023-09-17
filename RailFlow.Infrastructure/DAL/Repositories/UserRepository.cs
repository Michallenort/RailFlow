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

    public Task<User?> GetByEmailAsync(string? email)
        => _users.SingleOrDefaultAsync(x => x.Email == email); 

    public async Task AddAsync(User user)
        => _users.AddAsync(user);
}