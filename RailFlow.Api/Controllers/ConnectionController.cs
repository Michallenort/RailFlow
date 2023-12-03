using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailFlow.Application.Connections.DTO;
using RailFlow.Application.Connections.Queries;
using Railflow.Core.Services;
using Railflow.Core.ValueObjects;

namespace RailFlow.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ConnectionController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ConnectionController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ConnectionDto>>> GetConnections([FromQuery] string startStation, [FromQuery] string endStation, [FromQuery] DateOnly date)
    {
        var connections = await _mediator.Send(new GetConnections(startStation, endStation, date));
        return Ok(connections);
    }
}