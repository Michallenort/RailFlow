using Railflow.Core.Entities;
using Railflow.Core.Repositories;
using Railflow.Core.Services;
using RailFlow.Infrastructure.DAL;

namespace RailFlow.Infrastructure.Services;

internal sealed class ScheduleService : IScheduleService
{
    private readonly IRouteRepository _routeRepository;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly TrainDbContext _dbContext;
    
    public ScheduleService(IRouteRepository routeRepository, IScheduleRepository scheduleRepository, TrainDbContext dbContext)
    {
        _routeRepository = routeRepository;
        _scheduleRepository = scheduleRepository;
        _dbContext = dbContext;
    }

    public async Task Run()
    {
        var now = DateOnly.FromDateTime(DateTime.Today);
        var routes = await _routeRepository.GetAllAsync();

        var newSchedules = routes.Select(x => new Schedule(Guid.NewGuid(), now.AddDays(30), x.Id));
        var oldSchedules = await _scheduleRepository.GetByDateAsync(now.AddDays(-30));

        await _scheduleRepository.AddRangeAsync(newSchedules);
        await _scheduleRepository.DeleteRangeAsync(oldSchedules);
        await _dbContext.SaveChangesAsync();
    }
}