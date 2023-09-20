using Railflow.Core.Entities;

namespace Railflow.Core.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid? id);
    Task<User?> GetByEmailAsync(string? email);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}