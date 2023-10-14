using MediatR;

namespace RailFlow.Application.Schedules.Commands;

public record AddSchedulesForDay(DateOnly Date) : IRequest;