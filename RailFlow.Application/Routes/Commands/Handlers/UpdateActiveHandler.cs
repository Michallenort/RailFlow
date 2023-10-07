using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Routes.Commands.Handlers;

internal sealed class UpdateActiveHandler : IRequestHandler<UpdateActive>
{
    private readonly IRouteRepository _routeRepository;
    
    public UpdateActiveHandler(IRouteRepository routeRepository)
    {
        _routeRepository = routeRepository;
    }
    
    public async Task Handle(UpdateActive request, CancellationToken cancellationToken)
    {
        var route = await _routeRepository.GetByIdAsync(request.Id);

        if (route is null)
        {
            throw new RouteNotFoundException(request.Id);
        }

        if (!route.IsActive)
        {
            var stops = route.Stops.OrderBy(x => x.ArrivalHour).ToList();
            
            if (stops.First().StationId != route.StartStationId || 
                stops.Last().StationId != route.EndStationId)
            {
                throw new InvalidStopException();
            }
            
            route.SetIsActive(true);
        }
        else
        {
            route.SetIsActive(false);
        }

        await _routeRepository.UpdateAsync(route);
    }
}