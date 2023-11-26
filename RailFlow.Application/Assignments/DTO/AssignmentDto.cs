namespace RailFlow.Application.Assignments.DTO;

public record AssignmentDto(Guid Id, string UserEmail, Guid ScheduleId, TimeOnly StartHour, TimeOnly EndHour);