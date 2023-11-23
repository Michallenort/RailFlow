using Railflow.Core.Entities;

namespace Railflow.Core.Repositories;

public interface IEmployeeAssignmentRepository
{
    Task<IEnumerable<EmployeeAssignment>> GetByScheduleIdAsync(Guid scheduleId);
    Task<EmployeeAssignment?> GetByIdAsync(Guid id);
    Task AddAsync(EmployeeAssignment employeeAssignment);
    Task DeleteAsync(EmployeeAssignment employeeAssignment);
}