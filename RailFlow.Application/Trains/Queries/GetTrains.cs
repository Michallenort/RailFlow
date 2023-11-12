using MediatR;
using RailFlow.Application.Trains.DTO;
using Railflow.Core.Pagination;

namespace RailFlow.Application.Trains.Queries;

public record GetTrains(string? SearchTerm, int Page, int PageSize) : IRequest<PagedList<TrainDto>>;