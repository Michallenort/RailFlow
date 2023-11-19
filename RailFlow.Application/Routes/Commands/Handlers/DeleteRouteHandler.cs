using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Routes.Commands.Handlers;

public class DeleteRouteHandler : IRequestHandler<DeleteRoute>
{
    private readonly IRouteRepository _routeRepository;
    
    public DeleteRouteHandler(IRouteRepository routeRepository)
    {
        _routeRepository = routeRepository;
    }
    
    public async Task Handle(DeleteRoute request, CancellationToken cancellationToken)
    {
        var route = await _routeRepository.GetByIdAsync(request.Id);
        
        if (route is null)
        {
            throw new RouteNotFoundException(request.Id);
        }
        
        if (route.IsActive)
        {
            throw new RouteIsActiveException(route.Id);
        }
        
        await _routeRepository.DeleteAsync(route);
    }
}