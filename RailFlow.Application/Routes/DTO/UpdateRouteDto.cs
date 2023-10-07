namespace RailFlow.Application.Routes.DTO;

public record UpdateRouteDto(string? Name, Guid? StartStationId, Guid? EndStationId, Guid? TrainId);