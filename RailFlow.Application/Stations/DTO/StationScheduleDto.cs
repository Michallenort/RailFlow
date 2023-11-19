using RailFlow.Application.Stops.DTO;

namespace RailFlow.Application.Stations.DTO;

public record StationScheduleDto(Guid Id, string Name, IEnumerable<StopScheduleDto> StopSchedules);