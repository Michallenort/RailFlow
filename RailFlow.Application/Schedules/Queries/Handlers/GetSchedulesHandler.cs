using MediatR;
using RailFlow.Application.Schedules.DTO;
using Railflow.Core.Entities;
using Railflow.Core.Pagination;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Schedules.Queries.Handlers;

internal sealed class GetSchedulesHandler : IRequestHandler<GetSchedules, PagedList<ScheduleDto>>
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IScheduleMapper _scheduleMapper;
    
    public GetSchedulesHandler(IScheduleRepository scheduleRepository, IScheduleMapper scheduleMapper)
    {
        _scheduleRepository = scheduleRepository;
        _scheduleMapper = scheduleMapper;
    }
    
    public async Task<PagedList<ScheduleDto>> Handle(GetSchedules request, CancellationToken cancellationToken)
    {
        IEnumerable<Schedule> schedules;
        
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            schedules = await _scheduleRepository.GetBySearchTermAsync(request.SearchTerm);
        }
        else
        {
            schedules = await _scheduleRepository.GetAllAsync();
        }

        var pagedStations = PagedList<ScheduleDto>
            .Create(_scheduleMapper.MapScheduleDtos(schedules).ToList().OrderBy(x => x.Date), 
                request.Page, request.PageSize);
        return pagedStations;
    }
}