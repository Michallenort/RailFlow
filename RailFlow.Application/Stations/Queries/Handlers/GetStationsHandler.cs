using MediatR;
using RailFlow.Application.Stations.DTO;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Stations.Queries.Handlers;

internal sealed class GetStationsHandler : IRequestHandler<GetStations, IEnumerable<StationDto>>
{
    private readonly IStationRepository _stationRepository;
    private readonly IStationMapper _stationMapper;
    
    public GetStationsHandler(IStationRepository stationRepository, IStationMapper stationMapper)
    {
        _stationRepository = stationRepository;
        _stationMapper = stationMapper;
    }
    
    public async Task<IEnumerable<StationDto>> Handle(GetStations request, CancellationToken cancellationToken)
    {
        var stations = await _stationRepository.GetAllAsync();
        return _stationMapper.MapStationDtos(stations);
    }
}