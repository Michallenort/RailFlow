using MediatR;
using RailFlow.Application.Stations.DTO;
using Railflow.Core.Entities;
using Railflow.Core.Pagination;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Stations.Queries.Handlers;

internal sealed class GetStationsHandler : IRequestHandler<GetStations, PagedList<StationDetailsDto>>
{
    private readonly IStationRepository _stationRepository;
    private readonly IStationMapper _stationMapper;
    
    public GetStationsHandler(IStationRepository stationRepository, IStationMapper stationMapper)
    {
        _stationRepository = stationRepository;
        _stationMapper = stationMapper;
    }
    
    public async Task<PagedList<StationDetailsDto>> Handle(GetStations request, CancellationToken cancellationToken)
    {
        IEnumerable<Station> stations;
        
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            stations = await _stationRepository.GetBySearchTermAsync(request.SearchTerm);
        }
        else
        {
            stations = await _stationRepository.GetAllAsync();
        }

        var pagedStations = PagedList<StationDetailsDto>
            .Create(_stationMapper.MapStationDetailsDtos(stations).ToList(), request.Page, request.PageSize);
        return pagedStations;
    }
}