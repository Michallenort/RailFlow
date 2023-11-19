using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailFlow.Application.Routes.Commands;
using RailFlow.Application.Routes.DTO;
using RailFlow.Application.Routes.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace RailFlow.API.Controllers;

[ApiController]
[Route("[controller]")]
public class RouteController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public RouteController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [Authorize(Roles = "Supervisor")]
    [HttpPost]
    [SwaggerOperation("Create route")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> CreateRoute(CreateRoute command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    
    [Authorize(Roles = "Supervisor")]
    [HttpGet]
    [SwaggerOperation("Get routes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<RouteDto>>> GetRoutes([FromQuery] string? searchTerm, 
        [FromQuery] int page, [FromQuery] int pageSize)
    {
        var routes = await _mediator.Send(new GetRoutes(searchTerm, page, pageSize));
        return Ok(routes);
    }

    [Authorize(Roles = "Supervisor")]
    [HttpGet("{routeId:guid}")]
    [SwaggerOperation("Get route details")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RouteDetailsDto>> GetRouteDetails(Guid routeId)
    {
        var route = await _mediator.Send(new GetRouteDetails(routeId));
        return Ok(route);
    }
    
    [Authorize(Roles = "Supervisor")]
    [HttpPut("{routeId:guid}")]
    [SwaggerOperation("Update route's active status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateRoute(Guid routeId, UpdateRouteDto route)
    {
        await _mediator.Send(new UpdateRoute(routeId, route));
        return Ok();
    }
    
    [Authorize(Roles = "Supervisor")]
    [HttpPut("update-active/{routeId:guid}")]
    [SwaggerOperation("Update route's active status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateActive(Guid routeId)
    {
        await _mediator.Send(new UpdateActive(routeId));
        return Ok();
    }
    
    [Authorize(Roles = "Supervisor")]
    [HttpDelete("{routeId:guid}")]
    [SwaggerOperation("Delete route")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteRoute(Guid routeId)
    {
        await _mediator.Send(new DeleteRoute(routeId));
        return NoContent();
    }
}