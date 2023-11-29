using RailFlow.Application.Assignments.DTO;
using RailFlow.Application.Routes.DTO;

namespace RailFlow.Application.Schedules.DTO;

public record ScheduleDetailsDto(Guid Id, DateOnly Date, RouteDto Route, 
    IEnumerable<AssignmentDto> Assignments);