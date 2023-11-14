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

    public async Task<IEnumerable<User>> GetAllAsync()
        => await _users
            .AsNoTracking()
            .Include(x => x.Role)
            .ToListAsync();

    public async Task<IEnumerable<User>> GetBySearchTermAsync(string searchTerm)
        => await _users
            .AsNoTracking()
            .Include(x => x.Role)
            .Where(user => user.FirstName.ToLower().Contains(searchTerm.ToLower()) || 
                           user.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                           user.Email.ToLower().Contains(searchTerm.ToLower()))
            .ToListAsync();

    public async Task<User?> GetByIdAsync(Guid? id)
        => await _users.Include(x => x.Role)
            .SingleOrDefaultAsync(x => x.Id == id);

    public async Task<User?> GetByEmailAsync(string? email)
        => await _users.SingleOrDefaultAsync(x => x.Email == email);

    public async Task AddAsync(User user)
        => await _users.AddAsync(user);

    public async Task UpdateAsync(User user)
        => await Task.Run(() => _users.Update(user));

    public async Task DeleteAsync(User user)
        => await Task.Run(() => _users.Remove(user));
}