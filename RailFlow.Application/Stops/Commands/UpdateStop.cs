using MediatR;
using RailFlow.Application.Stops.DTO;

namespace RailFlow.Application.Stops.Commands;

public record UpdateStop(Guid Id, UpdateStopDto Stop) : IRequest;