using MediatR;

namespace RailFlow.Application.Trains.Commands;

public record CreateTrain(int Number, float? MaxSpeed, int Capacity) : IRequest;