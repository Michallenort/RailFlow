using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Entities;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Schedules.Commands.Handlers;

internal sealed class AddSchedulesHandlerForDay : IRequestHandler<AddSchedulesForDay>
{
    private readonly IRouteRepository _routeRepository;
    private readonly IScheduleRepository _scheduleRepository;
    
    public AddSchedulesHandlerForDay(IRouteRepository routeRepository, IScheduleRepository scheduleRepository)
    {
        _routeRepository = routeRepository;
        _scheduleRepository = scheduleRepository;
    }
    
    public async Task Handle(AddSchedulesForDay request, CancellationToken cancellationToken)
    {
        var routes = await _routeRepository.GetAllAsync();
        routes = routes.ToList();
        
        if (!routes.Any())
        {
            throw new NullException(nameof(Route), Guid.Empty);
        }
        
        var schedules = routes.Select(route => 
            new Schedule(Guid.NewGuid(), request.Date, route.Id));
        
        await _scheduleRepository.AddRangeAsync(schedules);
    }
}