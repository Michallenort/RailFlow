using MediatR;
using RailFlow.Application.Users.DTO;

namespace RailFlow.Application.Users.Queries;

public record GetUsers() : IRequest<IEnumerable<UserDto>>;