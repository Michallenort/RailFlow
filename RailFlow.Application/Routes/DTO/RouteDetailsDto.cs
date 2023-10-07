using RailFlow.Application.Stops.DTO;

namespace RailFlow.Application.Routes.DTO;

public record RouteDetailsDto(Guid Id, string Name, string StartStationName, string EndStationName, 
    int TrainNumber, IEnumerable<StopDto> StopDtos);