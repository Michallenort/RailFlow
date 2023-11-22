using Microsoft.EntityFrameworkCore;
using Railflow.Core.Entities;
using Railflow.Core.Repositories;

namespace RailFlow.Infrastructure.DAL.Repositories;

internal sealed class ScheduleRepository : IScheduleRepository
{
    private readonly DbSet<Schedule> _schedules;

    public ScheduleRepository(TrainDbContext dbContext)
    {
        _schedules = dbContext.Schedules;
    }

    public async Task<IEnumerable<Schedule>> GetByDateAsync(DateOnly date)
        => await _schedules
            .Where(schedule => schedule.Date == date)
            .Include(x => x.Route)
            .ThenInclude(x => x.Stops)
            .ThenInclude(x =>  x.Station)
            .Include(x => x.Route)
            .ThenInclude(x => x.Train)
            .ToListAsync();

    public async Task AddRangeAsync(IEnumerable<Schedule> schedules)
        => await _schedules.AddRangeAsync(schedules);

    public async Task DeleteRangeAsync(IEnumerable<Schedule> schedules)
        => await Task.Run(() => _schedules.RemoveRange(schedules));
}