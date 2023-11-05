using RailFlow.Application.Stations.DTO;
using Railflow.Core.Entities;
using Railflow.Core.ValueObjects;

namespace RailFlow.Application.Stations;

internal interface IStationMapper
{
    IEnumerable<StationDto> MapStationDtos(IEnumerable<Station> stations);
    IEnumerable<StationDetailsDto> MapStationDetailsDtos(IEnumerable<Station> stations);
    StationDetailsDto MapStationDetailsDto(Station station);
    Station MapStation(CreateStationDto stationDto);
    IEnumerable<Station> MapStations(IEnumerable<CreateStationDto> stationDtos);
}

internal sealed class StationMapper : IStationMapper
{
    public IEnumerable<StationDto> MapStationDtos(IEnumerable<Station> stations)
        => stations.Select(x => new StationDto(x.Id, x.Name));

    public IEnumerable<StationDetailsDto> MapStationDetailsDtos(IEnumerable<Station> stations)
        => stations.Select(
            x => new StationDetailsDto(x.Id, x.Name, x.Address.Country, x.Address.City, x.Address.Street));

    public StationDetailsDto MapStationDetailsDto(Station station)
        => new(station.Id, station.Name, station.Address.Country, station.Address.City, station.Address.Street);

    public Station MapStation(CreateStationDto stationDto)
        => new(Guid.NewGuid(), stationDto.Name, new Address(stationDto.Country, stationDto.City, stationDto.Street));

    public IEnumerable<Station> MapStations(IEnumerable<CreateStationDto> stationDtos)
        => stationDtos.Select(x => new Station(Guid.NewGuid(), x.Name, new Address(x.Country, x.City, x.Street)));
}