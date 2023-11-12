using System.Collections;
using MediatR;
using RailFlow.Application.Trains.DTO;
using Railflow.Core.Entities;
using Railflow.Core.Pagination;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Trains.Queries.Handlers;

internal sealed class GetTrainsHandler : IRequestHandler<GetTrains, PagedList<TrainDto>>
{
    private readonly ITrainRepository _trainRepository;
    private readonly ITrainMapper _trainMapper;
    
    public GetTrainsHandler(ITrainRepository trainRepository, ITrainMapper trainMapper)
    {
        _trainRepository = trainRepository;
        _trainMapper = trainMapper;
    }
    
    public async Task<PagedList<TrainDto>> Handle(GetTrains request, CancellationToken cancellationToken)
    {
        IEnumerable<Train> trains;
        
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            trains = await _trainRepository.GetBySearchTermAsync(request.SearchTerm);
        }
        else
        {
            trains = await _trainRepository.GetAllAsync();
        }
        
        var pagedTrains = PagedList<TrainDto>
            .Create(_trainMapper.MapTrainDtos(trains).ToList(), request.Page, request.PageSize);
        return pagedTrains;
    }
}