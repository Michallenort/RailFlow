using MediatR;
using RailFlow.Application.Routes.DTO;
using Railflow.Core.Pagination;

namespace RailFlow.Application.Routes.Queries;

public record GetRoutes(string? SearchTerm, int Page, int PageSize) : IRequest<PagedList<RouteDto>>;