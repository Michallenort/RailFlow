using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailFlow.Application.Security;
using RailFlow.Application.Users.Commands;
using RailFlow.Application.Users.DTO;
using RailFlow.Application.Users.Queries;

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
    
    [Authorize(Roles = "Supervisor")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var users = await _mediator.Send(new GetUsers());
        return Ok(users);
    }
    
    [Authorize]
    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<UserDetailsDto>> GetUserDetails(Guid userId)
    {
        var user = await _mediator.Send(new GetUserDetails(userId));
        return Ok(user);
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