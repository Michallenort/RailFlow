using MediatR;
using RailFlow.Application.Assignments.DTO;

namespace RailFlow.Application.Assignments.Queries;

public record GetAssignmentsForEmployee(Guid EmployeeId) : IRequest<IEnumerable<AssignmentsForEmployeeDto>>;