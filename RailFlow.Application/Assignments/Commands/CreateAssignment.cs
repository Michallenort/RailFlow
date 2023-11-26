using MediatR;

namespace RailFlow.Application.Assignments.Commands;

public record CreateAssignment(string UserEmail, Guid ScheduleId, 
    TimeOnly StartHour, TimeOnly EndHour) : IRequest;