using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Stations.Commands.Handlers;

internal sealed class CreateStationsHandler : IRequestHandler<CreateStations>
{
    private readonly IStationRepository _stationRepository;
    private readonly IStationMapper _stationMapper;
    
    public CreateStationsHandler(IStationRepository stationRepository, IStationMapper stationMapper)
    {
        _stationRepository = stationRepository;
        _stationMapper = stationMapper;
    }
    
    public async Task Handle(CreateStations request, CancellationToken cancellationToken)
    {
        var stations = _stationMapper.MapStations(request.Stations).ToList();
        
        var allStations = await _stationRepository.GetAllAsync();
        var allStationsNames = allStations.Select(x => x.Name);
        
        stations = stations.Where(station => !allStationsNames.Contains(station.Name)).ToList();
        
        await _stationRepository.AddRangeAsync(stations);
    }
}