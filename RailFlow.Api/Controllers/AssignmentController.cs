using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailFlow.Application.Assignments.Commands;
using RailFlow.Application.Assignments.DTO;
using RailFlow.Application.Assignments.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace RailFlow.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AssignmentController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public AssignmentController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [AllowAnonymous]
    [HttpGet("{scheduleId:guid}")]
    [SwaggerOperation("Get assignments by schedule id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<AssignmentDto>>> GetAssignments([FromRoute] Guid scheduleId)
    {
        var assignments = await _mediator.Send(new GetAssignments(scheduleId));
        return Ok(assignments);
    }
    
    [AllowAnonymous]
    [HttpPost]
    [SwaggerOperation("Create assignment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> CreateAssignment(CreateAssignment command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    
    [AllowAnonymous]
    [HttpDelete("{id:guid}")]
    [SwaggerOperation("Delete assignment")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteAssignment([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteAssignment(id));
        return NoContent();
    }
    
    [AllowAnonymous]
    [HttpGet("employee/{employeeId:guid}")]
    [SwaggerOperation("Get assignments by employee id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<AssignmentsForEmployeeDto>>> GetAssignmentsByEmployeeId([FromRoute] Guid employeeId)
    {
        var assignments = await _mediator.Send(new GetAssignmentsForEmployee(employeeId));
        return Ok(assignments);
    }
    
}