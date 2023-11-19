namespace RailFlow.Application.Routes.DTO;

public record RouteDto(Guid Id, string Name, string StartStationName, string EndStationName, 
    int TrainNumber, bool IsActive);