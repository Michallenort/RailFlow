using Microsoft.EntityFrameworkCore;
using Railflow.Core.Entities;
using Railflow.Core.Repositories;

namespace RailFlow.Infrastructure.DAL.Repositories;

internal sealed class ReservationRepository : IReservationRepository
{
    private readonly DbSet<Reservation> _reservations;

    public ReservationRepository(TrainDbContext dbContext)
    {
        _reservations = dbContext.Reservations;
    }

    public async Task<IEnumerable<Reservation>> GetByUserIdAsync(Guid userId)
        => await _reservations
            .Include(x => x.FirstSchedule)
            .ThenInclude(x => x.Route)
            .Include(x => x.SecondSchedule)
            .Include(x => x.StartStop)
            .ThenInclude(x => x.Station)
            .Include(x => x.EndStop)
            .ThenInclude(x => x.Station)
            .Include(x => x.TransferStop)
            .Where(r => r.UserId == userId)
            .ToListAsync();

    public async Task<Reservation?> GetByIdAsync(Guid id)
        => await _reservations
            .Include(x => x.FirstSchedule)
            .ThenInclude(x => x.Route)
            .Include(x => x.SecondSchedule)
            .Include(x => x.StartStop)
            .ThenInclude(x => x.Station)
            .Include(x => x.EndStop)
            .ThenInclude(x => x.Station)
            .Include(x => x.TransferStop)
            .SingleOrDefaultAsync(r => r.Id == id);

    public async Task AddAsync(Reservation reservation)
        => await _reservations.AddAsync(reservation);

    public async Task RemoveAsync(Reservation reservation)
        => await Task.Run(() => _reservations.Remove(reservation));
}