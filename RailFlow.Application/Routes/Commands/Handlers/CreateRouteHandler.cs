using FluentValidation;
using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Entities;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Routes.Commands.Handlers;

internal sealed class CreateRouteHandler : IRequestHandler<CreateRoute>
{
    private readonly IRouteRepository _routeRepository;
    private readonly IStationRepository _stationRepository;
    private readonly ITrainRepository _trainRepository;
    private readonly IValidator<CreateRoute> _validator;
    
    public CreateRouteHandler(IRouteRepository routeRepository, ITrainRepository trainRepository, 
        IStationRepository stationRepository, IValidator<CreateRoute> validator)
    {
        _routeRepository = routeRepository;
        _stationRepository = stationRepository;
        _trainRepository = trainRepository;
        _validator = validator;
    }
    
    public async Task Handle(CreateRoute request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        if (await _routeRepository.GetByNameAsync(request.Name) is not null)
        {
            throw new RouteExistsException(request.Name);
        }
        
        var startStation = await _stationRepository.GetByNameAsync(request.StartStationName);
        
        if (startStation is null)
        {
            throw new StationNotFoundException(request.StartStationName);
        }
        
        var endStation = await _stationRepository.GetByNameAsync(request.EndStationName);
        
        if (endStation is null)
        {
            throw new StationNotFoundException(request.EndStationName);
        }

        var train = await _trainRepository.GetByNumberAsync(request.TrainNumber);
        
        if (train is null)
        {
            throw new TrainNotFoundException(request.TrainNumber);
        }

        if (train.AssignedRoute is not null)
        {
            throw new TrainAssignedException(train.Id);
        }
        
        var route = new Route(Guid.NewGuid(), request.Name, startStation.Id, endStation.Id, train.Id, false);
        
        await _routeRepository.AddAsync(route);
    }
}