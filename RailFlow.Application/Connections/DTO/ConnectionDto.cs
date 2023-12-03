namespace RailFlow.Application.Connections.DTO;

public record ConnectionDto(IEnumerable<SubConnectionDto> SubConnections, float Price);