using Microsoft.EntityFrameworkCore;
using Railflow.Core.Entities;
using Railflow.Core.Repositories;
using Railflow.Core.Services;
using RailFlow.Infrastructure.DAL;

namespace RailFlow.Infrastructure.Services;

internal sealed class ScheduleService : IScheduleService
{
    private readonly TrainDbContext _dbContext;
    
    public ScheduleService(TrainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Run()
    {
        var now = DateOnly.FromDateTime(DateTime.Today);
        var routes = await _dbContext.Routes
            .AsNoTracking()
            .ToListAsync();

        var newSchedules = routes.Select(x => new Schedule(Guid.NewGuid(), now.AddDays(30), x.Id));
        var oldSchedules = await _dbContext.Schedules
            .Where(schedule => schedule.Date == now.AddDays(-30))
            .ToListAsync();

        await _dbContext.Schedules.AddRangeAsync(newSchedules);
        await Task.Run(() => _dbContext.Schedules.RemoveRange(oldSchedules));
        await _dbContext.SaveChangesAsync();
    }
}