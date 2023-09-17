using Railflow.Core.Entities;

namespace Railflow.Core.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(int id);
}