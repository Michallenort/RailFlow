using Railflow.Core.Entities;

namespace Railflow.Core.Repositories;

public interface ITrainRepository
{
    Task<IEnumerable<Train>> GetAllAsync();
    Task<IEnumerable<Train>> GetBySearchTermAsync(string searchTerm);
    Task<Train?> GetByIdAsync(Guid id);
    Task<Train?> GetByNumberAsync(int number);
    Task AddAsync(Train train);
    Task DeleteAsync(Train train);
}