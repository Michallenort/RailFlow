namespace RailFlow.Application.Stops.DTO;

public record StopDto(Guid Id, TimeOnly ArrivalTime, TimeOnly DepartureTime, string StationName);