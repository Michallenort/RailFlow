namespace RailFlow.Application.Stops.DTO;

public record UpdateStopDto(TimeOnly? ArrivalHour, TimeOnly? DepartureHour, Guid? StationId);