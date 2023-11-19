using MediatR;
using RailFlow.Application.Exceptions;
using RailFlow.Application.Stations.DTO;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Stations.Queries.Handlers;

internal class GetStationScheduleHandler : IRequestHandler<GetStationSchedule, StationScheduleDto>
{
    private readonly IStationRepository _stationRepository;
    private readonly IStopRepository _stopRepository;
    private readonly IStationMapper _stationMapper;
    
    public GetStationScheduleHandler(IStationRepository stationRepository, IStopRepository stopRepository, 
        IStationMapper stationMapper)
    {
        _stationRepository = stationRepository;
        _stopRepository = stopRepository;
        _stationMapper = stationMapper;
    }
    
    public async Task<StationScheduleDto> Handle(GetStationSchedule request, CancellationToken cancellationToken)
    {
        var station = await _stationRepository.GetByIdAsync(request.Id);
        
        if (station is null)
        {
            throw new StationNotFoundException(request.Id);
        }
        
        return _stationMapper.MapStationScheduleDto(station);
    }
}