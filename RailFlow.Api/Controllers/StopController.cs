using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailFlow.Application.Stops.Commands;
using RailFlow.Application.Stops.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace RailFlow.API.Controllers;

[ApiController]
[Route("[controller]")]
public class StopController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public StopController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [Authorize(Roles = "Supervisor")]
    [HttpPost]
    [SwaggerOperation("Create stop")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> CreateStop(CreateStop command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    
    [Authorize(Roles = "Supervisor")]
    [HttpPost("create-stops")]
    [SwaggerOperation("Create set of stops")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> CreateStops(CreateStops command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    
    [AllowAnonymous]
    [HttpGet("{routeId:guid}")]
    [SwaggerOperation("Get stops by route id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetStopsByRouteId(Guid routeId)
    {
        var stops = await _mediator.Send(new GetStops(routeId));
        return Ok(stops);
    }
    
}