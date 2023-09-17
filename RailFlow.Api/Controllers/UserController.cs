using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailFlow.Application.Security;
using RailFlow.Application.Users.Commands;
using RailFlow.Application.Users.DTO;

namespace RailFlow.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITokenStorage _tokenStorage;

    public UserController(IMediator mediator, ITokenStorage tokenStorage)
    {
        _mediator = mediator;
        _tokenStorage = tokenStorage;
    }

    [AllowAnonymous]
    [HttpPost("sign-up")]
    public async Task<ActionResult> SignUp(SignUp command)
    {
        await _mediator.Send(command);
        return NoContent();        
    }
    
    [AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<ActionResult<JwtDto>> SignIn(SignIn command)
    {
        await _mediator.Send(command);
        var jwt = _tokenStorage.Get();
        return Ok(jwt);
    }
}