using MediatR;
using RailFlow.Application.Exceptions;
using RailFlow.Application.Stations.DTO;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Stations.Queries.Handlers;

internal sealed class GetStationDetailsHandler : IRequestHandler<GetStationDetails, StationDetailsDto>
{
    private readonly IStationRepository _stationRepository;
    private readonly IStationMapper _stationMapper;
    
    public GetStationDetailsHandler(IStationRepository stationRepository, IStationMapper stationMapper)
    {
        _stationRepository = stationRepository;
        _stationMapper = stationMapper;
    }
    
    public async Task<StationDetailsDto> Handle(GetStationDetails request, CancellationToken cancellationToken)
    {
        var station = await _stationRepository.GetByIdAsync(request.Id);
        
        if (station is null)
        {
            throw new StationNotFoundException(request.Id);
        }

        return _stationMapper.MapStationDetailsDto(station);
    }
}