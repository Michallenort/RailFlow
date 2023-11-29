using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailFlow.Application.Schedules.Commands;
using RailFlow.Application.Schedules.DTO;
using RailFlow.Application.Schedules.Queries;
using Railflow.Core.Pagination;
using Railflow.Core.Services;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerOperation("Registers schedule service in Hangfire")]
    public ActionResult RegisterScheduleService()
    {
        _manager.AddOrUpdate<IScheduleService>("update-schedule",
            x => x.Run(), Cron.Daily());
        return Ok("added");
    }
    
    [Authorize(Roles = "Supervisor")]
    [HttpGet]
    [SwaggerOperation("Get all schedules")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<PagedList<ScheduleDto>>> GetAllSchedules([FromQuery] string? searchTerm, 
        [FromQuery] int page, [FromQuery] int pageSize)
    {
        var schedules = await _mediator.Send(
            new GetSchedules(searchTerm, page, pageSize));
        return Ok(schedules);
    }
    
    [Authorize(Roles = "Supervisor")]
    [HttpGet("{id:guid}")]
    [SwaggerOperation("Get all schedules")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScheduleDetailsDto>> GetScheduleDetails(Guid id)
    {
        var schedule = await _mediator.Send(
            new GetScheduleDetails(id));
        return Ok(schedule);
    }

    [Authorize(Roles = "Supervisor")]
    [HttpPost]
    [SwaggerOperation("Adds schedules for day")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddSchedulesForDay(AddSchedulesForDay command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    
    [Authorize(Roles = "Supervisor")]
    [HttpDelete]
    [SwaggerOperation("Deletes schedules for day")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteSchedulesForDay(DeleteSchedulesForDay command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}