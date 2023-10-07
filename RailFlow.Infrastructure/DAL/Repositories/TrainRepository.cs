using Microsoft.EntityFrameworkCore;
using Railflow.Core.Entities;
using Railflow.Core.Repositories;

namespace RailFlow.Infrastructure.DAL.Repositories;

internal sealed class TrainRepository : ITrainRepository
{
    private readonly DbSet<Train> _trains;
    
    public TrainRepository(TrainDbContext dbContext)
    {
        _trains = dbContext.Trains;
    }

    public async Task<IEnumerable<Train>> GetAllAsync()
        => await _trains.ToListAsync();

    public async Task<Train?> GetByIdAsync(Guid id)
        => await _trains
            .Include(x => x.AssignedRoute)
            .SingleOrDefaultAsync(train => train.Id == id);

    public async Task<Train?> GetByNumberAsync(int number)
        => await _trains.SingleOrDefaultAsync(train => train.Number == number);

    public async Task AddAsync(Train train)
        => await _trains.AddAsync(train);

    public async Task DeleteAsync(Train train)
        => await Task.Run(() => _trains.Remove(train));
}