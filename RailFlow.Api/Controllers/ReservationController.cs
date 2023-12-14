using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailFlow.Application.Reservations.Commands;
using RailFlow.Application.Reservations.DTO;
using RailFlow.Application.Reservations.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace RailFlow.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ReservationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [Authorize]
    [HttpGet("{userId:guid}")]
    [SwaggerOperation("Get reservations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> GetReservations([FromRoute] Guid userId)
    {
        var reservations = await _mediator.Send(new GetReservationsForUser(userId));
        return Ok(reservations);
    }
    
    [Authorize]
    [HttpPost]
    [SwaggerOperation("Add reservation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddReservation(AddReservation command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    
    [Authorize]
    [HttpDelete("{id:guid}")]
    [SwaggerOperation("Cancel reservation")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CancelReservation(Guid id)
    {
        await _mediator.Send(new CancelReservation(id));
        return NoContent();
    }
    
    [Authorize]
    [HttpPost("generate-pdf/{reservationId:guid}")]
    [SwaggerOperation("Generate ticket")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<string>> GenerateTicket(Guid reservationId)
    {
        var url = await _mediator.Send(new GenerateTicket(reservationId));
        return Ok(url);
    }
}