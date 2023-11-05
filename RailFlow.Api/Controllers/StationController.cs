using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailFlow.Application.Stations.Commands;
using RailFlow.Application.Stations.DTO;
using RailFlow.Application.Stations.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace RailFlow.API.Controllers;

[ApiController]
[Route("[controller]")]
public class StationController : ControllerBase
{
    private readonly IMediator _mediator;

    public StationController(IMediator mediator)
    {
        _mediator = mediator;
    }
      
    [AllowAnonymous]
    [HttpGet]
    [SwaggerOperation("Get all stations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<StationDetailsDto>>> GetAllStations()
    {
        var stations = await _mediator.Send(new GetStations());
        return Ok(stations);
    }
    
    [AllowAnonymous]
    [HttpGet("{stationId:guid}")]
    [SwaggerOperation("Get station details")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StationDetailsDto>> GetStationDetails(Guid stationId)
    {
        var station = await _mediator.Send(new GetStationDetails(stationId));
        return Ok(station);
    }
  
    [Authorize(Roles = "Supervisor")]
    [HttpPost]
    [SwaggerOperation("Create station")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> CreateStation(CreateStation command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    
    [Authorize(Roles = "Supervisor")]
    [HttpPost("create-stations")]
    [SwaggerOperation("Create set of stations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> CreateStations(CreateStations command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    
    [Authorize(Roles = "Supervisor")]
    [HttpDelete("{stationId:guid}")]
    [SwaggerOperation("Delete station")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteStation(Guid stationId)
    {
        await _mediator.Send(new DeleteStation(stationId));
        return NoContent();
    }
}