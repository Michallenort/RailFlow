namespace RailFlow.Application.Stops.DTO;

public record CreateStopDto(TimeOnly ArrivalHour, TimeOnly DepartureHour,
    Guid StationId, Guid RouteId);