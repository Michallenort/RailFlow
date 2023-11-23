using MediatR;
using RailFlow.Application.Assignments.DTO;

namespace RailFlow.Application.Assignments.Queries;

public record GetAssignments(Guid ScheduleId) : IRequest<IEnumerable<AssignmentDto>>;