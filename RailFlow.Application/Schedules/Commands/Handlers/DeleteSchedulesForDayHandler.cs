using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Entities;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Schedules.Commands.Handlers;

internal sealed class DeleteSchedulesForDayHandler : IRequestHandler<DeleteSchedulesForDay>
{
    private readonly IScheduleRepository _scheduleRepository;
    
    public DeleteSchedulesForDayHandler(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }
    
    public async Task Handle(DeleteSchedulesForDay request, CancellationToken cancellationToken)
    {
        var schedules = await _scheduleRepository.GetByDateAsync(request.Date);
        schedules = schedules.ToList();
        
        if (!schedules.Any())
        {
            throw new NullException(nameof(Schedule), Guid.Empty);
        }
        
        await _scheduleRepository.DeleteRangeAsync(schedules);
    }
}