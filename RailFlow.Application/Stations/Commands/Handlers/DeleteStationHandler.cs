using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Stations.Commands.Handlers;

internal sealed class DeleteStationHandler : IRequestHandler<DeleteStation>
{
    private readonly IStationRepository _stationRepository;
    
    public DeleteStationHandler(IStationRepository stationRepository)
    {
        _stationRepository = stationRepository;
    }
    
    public async Task Handle(DeleteStation request, CancellationToken cancellationToken)
    {
        var station = await _stationRepository.GetByIdAsync(request.Id);
        
        if (station is null)
        {
            throw new StationNotFoundException(request.Id);
        }
        
        await _stationRepository.DeleteAsync(station);
    }
}