using RailFlow.Application.Schedules.DTO;
using RailFlow.Application.Stops.DTO;

namespace RailFlow.Application.Connections.DTO;

public record SubConnectionDto(ScheduleDto Schedule, IEnumerable<StopDto> Stops);