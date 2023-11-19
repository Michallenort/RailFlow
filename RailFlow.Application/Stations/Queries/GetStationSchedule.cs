using MediatR;
using RailFlow.Application.Stations.DTO;

namespace RailFlow.Application.Stations.Queries;

public record GetStationSchedule(Guid Id) : IRequest<StationScheduleDto>;