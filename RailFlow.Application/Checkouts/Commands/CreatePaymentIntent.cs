using MediatR;
using RailFlow.Application.Checkouts.DTO;

namespace RailFlow.Application.Checkouts.Commands;

public record CreatePaymentIntent(long Price) : IRequest<ClientSecretDto>;