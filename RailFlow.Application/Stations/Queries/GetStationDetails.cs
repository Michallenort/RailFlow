using MediatR;
using RailFlow.Application.Stations.DTO;

namespace RailFlow.Application.Stations.Queries;

public record GetStationDetails(Guid Id) : IRequest<StationDetailsDto>;