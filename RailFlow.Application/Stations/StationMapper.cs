using RailFlow.Application.Stations.DTO;
using Railflow.Core.Entities;
using Railflow.Core.ValueObjects;

namespace RailFlow.Application.Stations;

public interface IStationMapper
{
    IEnumerable<StationDto> MapStationDtos(IEnumerable<Station> station);
    StationDetailsDto MapStationDetailsDto(Station station);
    Station MapStation(CreateStationDto stationDto);
    IEnumerable<Station> MapStations(IEnumerable<CreateStationDto> stationDtos);
}

internal sealed class StationMapper : IStationMapper
{
    public IEnumerable<StationDto> MapStationDtos(IEnumerable<Station> station)
        => station.Select(x => new StationDto(x.Id, x.Name));

    public StationDetailsDto MapStationDetailsDto(Station station)
        => new(station.Id, station.Name, station.Address.Country, station.Address.City, station.Address.Street);

    public Station MapStation(CreateStationDto stationDto)
        => new(Guid.NewGuid(), stationDto.Name, new Address(stationDto.Country, stationDto.City, stationDto.Street));

    public IEnumerable<Station> MapStations(IEnumerable<CreateStationDto> stationDtos)
        => stationDtos.Select(x => new Station(Guid.NewGuid(), x.Name, new Address(x.Country, x.City, x.Street)));
}