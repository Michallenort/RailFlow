namespace RailFlow.Application.Assignments.DTO;

public record AssignmentDto(Guid Id, string UserName, Guid ScheduleId, TimeOnly StartHour, TimeOnly EndHour);