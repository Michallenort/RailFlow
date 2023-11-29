using MediatR;
using RailFlow.Application.Schedules.DTO;
using Railflow.Core.Pagination;

namespace RailFlow.Application.Schedules.Queries;

public record GetSchedules(string? SearchTerm, int Page, int PageSize) : IRequest<PagedList<ScheduleDto>>;