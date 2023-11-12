using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailFlow.Application.Trains.Commands;
using RailFlow.Application.Trains.DTO;
using RailFlow.Application.Trains.Queries;
using Railflow.Core.Pagination;
using Swashbuckle.AspNetCore.Annotations;

namespace RailFlow.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TrainController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public TrainController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [Authorize(Roles = "Supervisor")]
    [HttpGet]
    [SwaggerOperation("Get all trains")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<PagedList<TrainDto>>> GetAllTrains([FromQuery] string? searchTerm, 
        [FromQuery] int page, [FromQuery] int pageSize)
    {
        var trains = await _mediator.Send(new GetTrains(searchTerm, page, pageSize));
        return Ok(trains);
    }
    
    [Authorize(Roles = "Supervisor")]
    [HttpPost]
    [SwaggerOperation("Create train")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> CreateTrain(CreateTrain command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    
    [Authorize(Roles = "Supervisor")]
    [HttpDelete("{trainId:guid}")]
    [SwaggerOperation("Delete train")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteTrain(Guid trainId)
    {
        await _mediator.Send(new DeleteTrain(trainId));
        return NoContent();
    }
}