using FluentValidation;
using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Entities;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Assignments.Commands.Handlers;

internal class CreateAssignmentHandler : IRequestHandler<CreateAssignment>
{
    private readonly IUserRepository _userRepository;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IEmployeeAssignmentRepository _employeeAssignmentRepository;
    private readonly IValidator<CreateAssignment> _validator;
    
    public CreateAssignmentHandler(IUserRepository userRepository, IScheduleRepository scheduleRepository, 
        IEmployeeAssignmentRepository employeeAssignmentRepository, IValidator<CreateAssignment> validator)
    {
        _userRepository = userRepository;
        _scheduleRepository = scheduleRepository;
        _employeeAssignmentRepository = employeeAssignmentRepository;
        _validator = validator;
    }
    
    public async Task Handle(CreateAssignment request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        var user = await _userRepository.GetByEmailAsync(request.UserEmail);

        if (user is null)
        {
            throw new UserNotFoundException(request.UserEmail);
        }
        
        if (user.RoleId != 2)
        {
            throw new UserIsNotEmployeeException(request.UserEmail);
        }
        
        var schedule = await _scheduleRepository.GetByIdAsync(request.ScheduleId);
        
        if (schedule is null)
        {
            throw new ScheduleNotFoundException(request.ScheduleId);
        }
        
        var assignment = new EmployeeAssignment(Guid.NewGuid(), user.Id, schedule.Id , request.StartHour, request.EndHour);
        await _employeeAssignmentRepository.AddAsync(assignment);
    }
}