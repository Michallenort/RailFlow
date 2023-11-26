using MediatR;
using RailFlow.Application.Schedules.DTO;

namespace RailFlow.Application.Schedules.Queries;

public record GetScheduleDetails(Guid Id) : IRequest<ScheduleDetailsDto>;