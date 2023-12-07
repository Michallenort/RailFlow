using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RailFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ReservationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    
}