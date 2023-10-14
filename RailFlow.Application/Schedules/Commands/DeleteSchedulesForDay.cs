using MediatR;

namespace RailFlow.Application.Schedules.Commands;

public record DeleteSchedulesForDay(DateOnly Date) : IRequest;