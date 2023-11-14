using MediatR;
using RailFlow.Application.Users.DTO;
using Railflow.Core.Pagination;

namespace RailFlow.Application.Users.Queries;

public record GetUsers(string? SearchTerm, int Page, int PageSize) : IRequest<PagedList<UserDetailsDto>>;