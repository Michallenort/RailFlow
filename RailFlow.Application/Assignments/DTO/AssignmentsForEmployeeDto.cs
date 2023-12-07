namespace RailFlow.Application.Assignments.DTO;

public record AssignmentsForEmployeeDto(Guid Id, string RouteName, DateOnly Date, TimeOnly StartHour, TimeOnly EndHour);