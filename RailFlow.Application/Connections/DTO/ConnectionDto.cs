namespace RailFlow.Application.Connections.DTO;

public record ConnectionDto(IEnumerable<SubConnectionDto> SubConnections, Guid StartStopId, string StartStationName,
    Guid EndStopId, string EndStationName, TimeOnly StartHour, TimeOnly EndHour, float Price);