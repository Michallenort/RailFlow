using MediatR;
using RailFlow.Application.Trains.DTO;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Trains.Queries.Handlers;

internal sealed class GetTrainsHandler : IRequestHandler<GetTrains, IEnumerable<TrainDto>>
{
    private readonly ITrainRepository _trainRepository;
    private readonly ITrainMapper _trainMapper;
    
    public GetTrainsHandler(ITrainRepository trainRepository, ITrainMapper trainMapper)
    {
        _trainRepository = trainRepository;
        _trainMapper = trainMapper;
    }
    
    public async Task<IEnumerable<TrainDto>> Handle(GetTrains request, CancellationToken cancellationToken)
    {
        var trains = await _trainRepository.GetAllAsync();
        return _trainMapper.MapTrainDtos(trains);
    }
}