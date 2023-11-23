using MediatR;
using RailFlow.Application.Assignments.DTO;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Assignments.Queries.Handlers;

public class GetAssignmentsHandler : IRequestHandler<GetAssignments, IEnumerable<AssignmentDto>>
{
    private readonly IEmployeeAssignmentRepository _employeeAssignmentRepository;
    private readonly IAssignmentMapper _assignmentMapper;
    
    public GetAssignmentsHandler(IEmployeeAssignmentRepository employeeAssignmentRepository, IAssignmentMapper assignmentMapper)
    {
        _employeeAssignmentRepository = employeeAssignmentRepository;
        _assignmentMapper = assignmentMapper;
    }
    
    public async Task<IEnumerable<AssignmentDto>> Handle(GetAssignments request, CancellationToken cancellationToken)
    {
        var assignments = await _employeeAssignmentRepository.GetByScheduleIdAsync(request.ScheduleId);
        
        return _assignmentMapper.MapAssignmentDtos(assignments);
    }
}