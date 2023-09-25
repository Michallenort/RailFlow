using MediatR;

namespace RailFlow.Application.Trains.Commands;

public record DeleteTrain(Guid Id) : IRequest;