using MediatR;
using RailFlow.Application.Stations.DTO;
using Railflow.Core.Pagination;

namespace RailFlow.Application.Stations.Queries;

public record GetStations(string? SearchTerm, int Page, int PageSize) : IRequest<PagedList<StationDetailsDto>>;