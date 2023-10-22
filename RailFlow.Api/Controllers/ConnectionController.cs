using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Railflow.Core.Services;
using Railflow.Core.ValueObjects;

namespace RailFlow.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ConnectionController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IConnectionService _connectionService;
    
    public ConnectionController(IMediator mediator, IConnectionService connectionService)
    {
        _mediator = mediator;
        _connectionService = connectionService;
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Connection>>> GetConnections([FromQuery] string startStation, [FromQuery] string endStation, [FromQuery] DateOnly date)
    {
        var connections = await _connectionService.FindConnectionAsync(startStation, endStation, date);
        return Ok(connections);
    }
}