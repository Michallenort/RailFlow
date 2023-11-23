using RailFlow.Application.Assignments.DTO;
using Railflow.Core.Entities;

namespace RailFlow.Application.Assignments;

public interface IAssignmentMapper
{
    IEnumerable<AssignmentDto> MapAssignmentDtos(IEnumerable<EmployeeAssignment> assignment);
}

internal class AssignmentMapper :  IAssignmentMapper
{
    public IEnumerable<AssignmentDto> MapAssignmentDtos(IEnumerable<EmployeeAssignment> assignment)
        => assignment.Select(x => new AssignmentDto(x.Id, x.User.FirstName + " " + x.User.LastName, 
            x.ScheduleId, x.StartHour, x.EndHour));
}