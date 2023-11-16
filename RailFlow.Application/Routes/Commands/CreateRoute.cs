using MediatR;

namespace RailFlow.Application.Routes.Commands;

public record CreateRoute(string Name, string StartStationName, string EndStationName, int TrainNumber) : IRequest;