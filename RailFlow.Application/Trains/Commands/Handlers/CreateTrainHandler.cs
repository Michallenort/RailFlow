using FluentValidation;
using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Entities;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Trains.Commands.Handlers;

internal sealed class CreateTrainHandler : IRequestHandler<CreateTrain>
{
    private readonly ITrainRepository _trainRepository;
    private readonly IValidator<CreateTrain> _validator;
    
    public CreateTrainHandler(ITrainRepository trainRepository, IValidator<CreateTrain> validator)
    {
        _trainRepository = trainRepository;
        _validator = validator;
    }
    
    public async Task Handle(CreateTrain request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        if (await _trainRepository.GetByNumberAsync(request.Number) is not null)
        {
            throw new TrainExistsException(request.Number);
        }
        
        var train = new Train(Guid.NewGuid(), request.Number, request.MaxSpeed, request.Capacity);
        
        await _trainRepository.AddAsync(train);
    }
}