using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailFlow.Application.Routes.Commands;
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
    
    [AllowAnonymous]
    [HttpPost]
    [SwaggerOperation("Create route")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> CreateRoute(CreateRoute command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    
}