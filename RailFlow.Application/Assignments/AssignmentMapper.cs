using RailFlow.Application.Assignments.DTO;
using Railflow.Core.Entities;

namespace RailFlow.Application.Assignments;

public interface IAssignmentMapper
{
    IEnumerable<AssignmentDto> MapAssignmentDtos(IEnumerable<EmployeeAssignment> assignment);
    IEnumerable<AssignmentsForEmployeeDto> MapAssignmentsForEmployeeDtos(IEnumerable<EmployeeAssignment> assignment);
}

internal class AssignmentMapper :  IAssignmentMapper
{
    public IEnumerable<AssignmentDto> MapAssignmentDtos(IEnumerable<EmployeeAssignment> assignment)
        => assignment.Select(x => new AssignmentDto(x.Id, x.User.Email, 
            x.ScheduleId, x.StartHour, x.EndHour));

    public IEnumerable<AssignmentsForEmployeeDto> MapAssignmentsForEmployeeDtos(IEnumerable<EmployeeAssignment> assignment)
        => assignment.Select(x => new AssignmentsForEmployeeDto(x.Id, x.Schedule.Route.Name, 
            x.Schedule.Date, x.StartHour, x.EndHour));
}