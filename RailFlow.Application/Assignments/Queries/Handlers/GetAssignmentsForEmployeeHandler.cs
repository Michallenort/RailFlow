using MediatR;
using RailFlow.Application.Assignments.DTO;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Assignments.Queries.Handlers;

internal sealed class GetAssignmentsForEmployeeHandler : 
    IRequestHandler<GetAssignmentsForEmployee, IEnumerable<AssignmentsForEmployeeDto>>
{
    private readonly IEmployeeAssignmentRepository _assignmentRepository;
    private readonly IAssignmentMapper _assignmentMapper;
    
    public GetAssignmentsForEmployeeHandler(IEmployeeAssignmentRepository assignmentRepository, IAssignmentMapper assignmentMapper)
    {
        _assignmentRepository = assignmentRepository;
        _assignmentMapper = assignmentMapper;
    }
    
    public async Task<IEnumerable<AssignmentsForEmployeeDto>> Handle(GetAssignmentsForEmployee request, CancellationToken cancellationToken)
    {
        var assignments = await _assignmentRepository.GetByEmployeeIdAsync(request.EmployeeId);
        
        return _assignmentMapper.MapAssignmentsForEmployeeDtos(assignments).OrderBy(x => x.Date);
    }
}