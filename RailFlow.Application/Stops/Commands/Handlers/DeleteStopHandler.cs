using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Stops.Commands.Handlers;

internal sealed class DeleteStopHandler : IRequestHandler<DeleteStop>
{
    private readonly IStopRepository _stopRepository;
    
    public DeleteStopHandler(IStopRepository stopRepository)
    {
        _stopRepository = stopRepository;
    }
    
    public async Task Handle(DeleteStop request, CancellationToken cancellationToken)
    {
        var stop = await _stopRepository.GetByIdAsync(request.Id);
        
        if (stop is null)
        {
            throw new StopNotFoundException(request.Id);
        }
        
        await _stopRepository.DeleteAsync(stop);    
    }
}