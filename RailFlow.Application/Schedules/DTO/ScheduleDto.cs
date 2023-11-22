using RailFlow.Application.Routes.DTO;

namespace RailFlow.Application.Schedules.DTO;

public record ScheduleDto(Guid Id, DateOnly Date, RouteDto Route);