using FluentValidation;
using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Routes.Commands.Handlers;

internal sealed class UpdateRouteHandler : IRequestHandler<UpdateRoute>
{
    private readonly IRouteRepository _routeRepository;
    private readonly IStationRepository _stationRepository;
    private readonly ITrainRepository _trainRepository;
    private readonly IValidator<UpdateRoute> _validator;

    public UpdateRouteHandler(IRouteRepository routeRepository, IStationRepository stationRepository, 
        ITrainRepository trainRepository, IValidator<UpdateRoute> validator)
    {
        _routeRepository = routeRepository;
        _stationRepository = stationRepository;
        _trainRepository = trainRepository;
        _validator = validator;
    }

    public async Task Handle(UpdateRoute request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        var route = await _routeRepository.GetByIdAsync(request.Id);
        
        if (route is null)
        {
            throw new RouteNotFoundException(request.Id);
        }
        
        if (route.IsActive)
        {
            throw new RouteIsActiveException(route.Id);
        }

        var newRouteName = request.Route.Name ?? route.Name;
        var newStartStationId = request.Route.StartStationId ?? route.StartStationId;
        var newEndStationId = request.Route.EndStationId ?? route.EndStationId;
        var newTrainId = request.Route.TrainId ?? route.TrainId;

        if (newStartStationId is null)
        {
            throw new NullException("Start station", route.Id);
        }
        
        if (newEndStationId is null)
        {
            throw new NullException("End station", route.Id);
        }
        
        if (newTrainId is null)
        {
            throw new NullException("Train", route.Id);
        }
        
        if (request.Route.Name is not null && 
            await _routeRepository.GetByNameAsync(newRouteName) is not null)
        {
            throw new RouteExistsException(newRouteName);
        }
        
        if (await _stationRepository.GetByIdAsync(newStartStationId!.Value) is null)
        {
            throw new StationNotFoundException(newStartStationId!.Value);
        }
        
        if (await _stationRepository.GetByIdAsync(newEndStationId!.Value) is null)
        {
            throw new StationNotFoundException(newEndStationId!.Value);
        }
        
        if (newStartStationId == newEndStationId)
        {
            throw new EqualStationsException();
        }
        
        var train = await _trainRepository.GetByIdAsync(newTrainId!.Value);
        
        if (train is null)
        {
            throw new TrainNotFoundException(newTrainId!.Value);
        }
        
        if (request.Route.TrainId is not null && train.AssignedRoute is not null)
        {
            throw new TrainAssignedException(train.Id);
        }

        route.Update(request.Route.Name, request.Route.StartStationId, 
            request.Route.EndStationId, request.Route.TrainId);
        
        await _routeRepository.UpdateAsync(route);
    }
}