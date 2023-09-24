using MediatR;

namespace RailFlow.Application.Stations.Commands;

public record CreateStation(string Name, string Country, string City, string Street) : IRequest;