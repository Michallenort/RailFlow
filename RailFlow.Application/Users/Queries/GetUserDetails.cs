using MediatR;
using RailFlow.Application.Users.DTO;

namespace RailFlow.Application.Users.Queries;

public record GetUserDetails(Guid Id) : IRequest<UserDetailsDto>;