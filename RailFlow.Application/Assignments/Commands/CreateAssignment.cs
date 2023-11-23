using MediatR;

namespace RailFlow.Application.Assignments.Commands;

public record CreateAssignment(string UserEmail, string RouteName, DateOnly Date, 
    TimeOnly StartHour, TimeOnly EndHour) : IRequest;