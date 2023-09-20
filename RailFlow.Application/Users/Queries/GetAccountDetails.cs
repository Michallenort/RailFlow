using MediatR;
using RailFlow.Application.Users.DTO;

namespace RailFlow.Application.Users.Queries;

public record GetAccountDetails() : IRequest<UserDetailsDto>;