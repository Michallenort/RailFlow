using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using RailFlow.Application.Checkouts.Commands;
using RailFlow.Application.Checkouts.DTO;
using RailFlow.Application.Reservations.DTO;
using Railflow.Core.Entities;
using Stripe;
using Stripe.Checkout;

namespace RailFlow.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CheckoutController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;

    public CheckoutController(IConfiguration configuration, IMediator mediator)
    {
        _configuration = configuration;
        _mediator = mediator;
    }

    [HttpGet("config")]
    public async Task<ActionResult<ConfigDto>> GetConfig()
    {
        var pubKey = _configuration["Stripe:PubKey"];
        return Ok(new ConfigDto(pubKey!));
    }

    [HttpPost("create-payment-intent")]
    public async Task<ActionResult<ClientSecretDto>> CreatePaymentIntent(CreatePaymentIntent request)
    {
        var clientSecretDto = await _mediator.Send(request);
        return Ok(clientSecretDto);
    }
}