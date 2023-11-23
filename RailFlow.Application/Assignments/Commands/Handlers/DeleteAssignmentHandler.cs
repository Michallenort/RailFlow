using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Assignments.Commands.Handlers;

public class DeleteAssignmentHandler : IRequestHandler<DeleteAssignment>
{
    private readonly IEmployeeAssignmentRepository _employeeAssignmentRepository;
    
    public DeleteAssignmentHandler(IEmployeeAssignmentRepository employeeAssignmentRepository)
    {
        _employeeAssignmentRepository = employeeAssignmentRepository;
    }
    
    public async Task Handle(DeleteAssignment request, CancellationToken cancellationToken)
    {
        var assignment = await _employeeAssignmentRepository.GetByIdAsync(request.Id);
        
        if (assignment is null)
        {
            throw new AssignmentNotFoundException(request.Id);
        }
        
        await _employeeAssignmentRepository.DeleteAsync(assignment);    
    }
}