using Railflow.Core.Entities;

namespace Railflow.Core.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string? email);
    Task AddAsync(User user);
}