using FluentValidation;
using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Entities;
using Railflow.Core.Repositories;
using Railflow.Core.ValueObjects;

namespace RailFlow.Application.Stations.Commands.Handlers;

internal sealed class CreateStationHandler : IRequestHandler<CreateStation>
{
    private readonly IStationRepository _stationRepository;
    private readonly IValidator<CreateStation> _validator;
    
    public CreateStationHandler(IStationRepository stationRepository, IValidator<CreateStation> validator)
    {
        _stationRepository = stationRepository;
        _validator = validator;
    }
    
    public async Task Handle(CreateStation request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        if (await _stationRepository.GetByNameAsync(request.Name) is not null)
        {
            throw new StationExistsException(request.Name);
        }
        
        var station = new Station(Guid.NewGuid(), request.Name, new Address(request.Country, request.City, request.Street));
        
        await _stationRepository.AddAsync(station);
    }
}