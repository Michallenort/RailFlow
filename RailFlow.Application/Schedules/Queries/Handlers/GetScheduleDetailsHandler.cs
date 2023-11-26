using MediatR;
using RailFlow.Application.Exceptions;
using RailFlow.Application.Schedules.DTO;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Schedules.Queries.Handlers;

internal sealed class GetScheduleDetailsHandler : IRequestHandler<GetScheduleDetails, ScheduleDetailsDto>
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IScheduleMapper _scheduleMapper;
    
    public GetScheduleDetailsHandler(IScheduleRepository scheduleRepository, IScheduleMapper scheduleMapper)
    {
        _scheduleRepository = scheduleRepository;
        _scheduleMapper = scheduleMapper;
    }
    
    public async Task<ScheduleDetailsDto> Handle(GetScheduleDetails request, CancellationToken cancellationToken)
    {
        var schedule = await _scheduleRepository.GetByIdAsync(request.Id);

        if (schedule is null)
        {
            throw new ScheduleNotFoundException(request.Id);
        }
            
        
        return _scheduleMapper.MapScheduleDetailsDto(schedule);
    }
}