using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Railflow.Core.Services;

namespace RailFlow.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly IRecurringJobManager _manager;
    private readonly IScheduleService _scheduleService;
    
    public ScheduleController(IRecurringJobManager manager, IScheduleService scheduleService)
    {
        _manager = manager;
        _scheduleService = scheduleService;
    }
    
    [HttpPost("register-schedule-service")]
    public ActionResult RegisterScheduleService()
    {
        _manager.AddOrUpdate<IScheduleService>("update-schedule",
            x => x.Run(), Cron.Daily());
        return Ok("added");
    }
}