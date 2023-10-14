using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RailFlow.Application.Schedules.Commands;
using Railflow.Core.Services;

namespace RailFlow.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly IRecurringJobManager _manager;
    private readonly IScheduleService _scheduleService;
    private readonly IMediator _mediator;
    
    public ScheduleController(IRecurringJobManager manager, IScheduleService scheduleService, 
        IMediator mediator)
    {
        _manager = manager;
        _scheduleService = scheduleService;
        _mediator = mediator;
    }
    
    [HttpPost("register-schedule-service")]
    public ActionResult RegisterScheduleService()
    {
        _manager.AddOrUpdate<IScheduleService>("update-schedule",
            x => x.Run(), Cron.Daily());
        return Ok("added");
    }

    [HttpPost]
    public async Task<ActionResult> AddSchedulesForDay(AddSchedulesForDay command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpDelete]
    public async Task<ActionResult> DeleteSchedulesForDay(DeleteSchedulesForDay command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}