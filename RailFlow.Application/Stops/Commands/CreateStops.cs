using MediatR;
using RailFlow.Application.Stops.DTO;

namespace RailFlow.Application.Stops.Commands;

public record CreateStops(IEnumerable<CreateStopDto> Stops) : IRequest;