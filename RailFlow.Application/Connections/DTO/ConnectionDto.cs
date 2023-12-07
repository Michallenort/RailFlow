namespace RailFlow.Application.Connections.DTO;

public record ConnectionDto(IEnumerable<SubConnectionDto> SubConnections, string StartStationName,
    string EndStationName, TimeOnly StartHour, TimeOnly EndHour, float Price);