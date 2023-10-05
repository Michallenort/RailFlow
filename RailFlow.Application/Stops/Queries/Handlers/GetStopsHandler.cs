using MediatR;
using RailFlow.Application.Stops.DTO;
using Railflow.Core.Repositories;
using System.Collections.Generic;

namespace RailFlow.Application.Stops.Queries.Handlers;

internal sealed class GetStopsHandler : IRequestHandler<GetStops, IEnumerable<StopDto>>
{
    private readonly IStopRepository _stopRepository;
    private readonly IStopMapper _stopMapper;
    
    public GetStopsHandler(IStopRepository stopRepository, IStopMapper stopMapper)
    {
        _stopRepository = stopRepository;
        _stopMapper = stopMapper;
    }
    
    public async Task<IEnumerable<StopDto>> Handle(GetStops request, CancellationToken cancellationToken)
    {
        var stops = await _stopRepository.GetByRouteIdAsync(request.RouteId);
        return _stopMapper.MapStopDto(stops).OrderBy(x => x.DepartureTime);
    }
}