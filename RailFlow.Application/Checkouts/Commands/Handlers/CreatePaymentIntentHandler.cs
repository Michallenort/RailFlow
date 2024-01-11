using MediatR;
using RailFlow.Application.Checkouts.DTO;
using Stripe;

namespace RailFlow.Application.Checkouts.Commands.Handlers;

internal sealed class CreatePaymentInentHandler : IRequestHandler<CreatePaymentIntent, ClientSecretDto>
{
    public async Task<ClientSecretDto> Handle(CreatePaymentIntent request, CancellationToken cancellationToken)
    {
        var paymentIntentService = new PaymentIntentService();
        var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
        {
            Amount = request.Price,
            Currency = "pln",
            AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
            {
                Enabled = true
            }
        });
        
        return new ClientSecretDto(paymentIntent.ClientSecret);
    }
}