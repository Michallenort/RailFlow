using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Trains.Commands.Handlers;

internal sealed class DeleteTrainHandler : IRequestHandler<DeleteTrain>
{
    private readonly ITrainRepository _trainRepository;
    
    public DeleteTrainHandler(ITrainRepository trainRepository)
    {
        _trainRepository = trainRepository;
    }
    
    public async Task Handle(DeleteTrain request, CancellationToken cancellationToken)
    {
        var train = await _trainRepository.GetByIdAsync(request.Id);
        
        if (train is null)
        {
            throw new TrainNotFoundException(request.Id);
        }
        
        if (train.AssignedRoute is not null && 
            train.AssignedRoute.IsActive)
        {
            throw new RouteIsActiveException(train.AssignedRoute.Id);
        }
        
        await _trainRepository.DeleteAsync(train);
    }
}