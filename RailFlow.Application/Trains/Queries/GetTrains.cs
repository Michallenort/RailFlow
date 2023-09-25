using MediatR;
using RailFlow.Application.Trains.DTO;

namespace RailFlow.Application.Trains.Queries;

public record GetTrains() : IRequest<IEnumerable<TrainDto>>;