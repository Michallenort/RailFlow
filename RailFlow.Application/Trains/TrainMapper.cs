using RailFlow.Application.Trains.DTO;
using Railflow.Core.Entities;

namespace RailFlow.Application.Trains;

internal interface ITrainMapper
{
    TrainDto MapTrainDto(Train train);
    IEnumerable<TrainDto> MapTrainDtos(IEnumerable<Train> trains);
}

internal sealed class TrainMapper : ITrainMapper
{
    public TrainDto MapTrainDto(Train train)
        => new(train.Id, train.Number, train.MaxSpeed, train.Capacity);

    public IEnumerable<TrainDto> MapTrainDtos(IEnumerable<Train> trains)
        => trains.Select(x => new TrainDto(x.Id, x.Number, x.MaxSpeed, x.Capacity));
}