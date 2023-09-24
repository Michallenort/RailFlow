using MediatR;
using RailFlow.Application.Stations.DTO;

namespace RailFlow.Application.Stations.Commands;

public record CreateStations(IEnumerable<CreateStationDto> Stations) : IRequest;