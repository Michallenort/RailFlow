using Railflow.Core.Entities;

namespace Railflow.Core.Repositories;

public interface IReservationRepository
{
    Task<IEnumerable<Reservation>> GetByUserIdAsync(Guid userId);
    Task<Reservation?> GetByIdAsync(Guid id);
    Task AddAsync(Reservation reservation);
    Task RemoveAsync(Reservation reservation);
}