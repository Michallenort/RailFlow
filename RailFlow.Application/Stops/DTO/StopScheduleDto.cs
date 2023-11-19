using RailFlow.Application.Routes.DTO;

namespace RailFlow.Application.Stops.DTO;

public record StopScheduleDto(Guid Id, RouteDto Route, TimeOnly ArrivalTime, TimeOnly DepartureTime);