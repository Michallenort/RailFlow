using Railflow.Core.Entities;

namespace Railflow.Core.Repositories;

public interface IEmployeeAssignmentRepository
{
    Task<IEnumerable<EmployeeAssignment>> GetByScheduleIdAsync(Guid scheduleId);
    Task<IEnumerable<EmployeeAssignment>> GetByEmployeeIdAsync(Guid employeeId);
    Task<EmployeeAssignment?> GetByIdAsync(Guid id);
    Task AddAsync(EmployeeAssignment employeeAssignment);
    Task DeleteAsync(EmployeeAssignment employeeAssignment);
}