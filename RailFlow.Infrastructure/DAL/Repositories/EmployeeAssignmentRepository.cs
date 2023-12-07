using Microsoft.EntityFrameworkCore;
using Railflow.Core.Entities;
using Railflow.Core.Repositories;

namespace RailFlow.Infrastructure.DAL.Repositories;

internal class EmployeeAssignmentRepository : IEmployeeAssignmentRepository
{
    private readonly DbSet<EmployeeAssignment> _employeeAssignments;
    
    public EmployeeAssignmentRepository(TrainDbContext dbContext)
    {
        _employeeAssignments = dbContext.EmployeeAssignments;
    }

    public async Task<IEnumerable<EmployeeAssignment>> GetByScheduleIdAsync(Guid scheduleId)
        => await _employeeAssignments
            .Where(x => x.ScheduleId == scheduleId)
            .Include(x => x.User)
            .Include(x => x.Schedule)
            .AsNoTracking()
            .ToListAsync();

    public async Task<IEnumerable<EmployeeAssignment>> GetByEmployeeIdAsync(Guid employeeId)
        => await _employeeAssignments
            .Where(x => x.UserId == employeeId)
            .Include(x => x.User)
            .Include(x => x.Schedule)
            .ThenInclude(x => x.Route)
            .AsNoTracking()
            .ToListAsync();

    public async Task<EmployeeAssignment?> GetByIdAsync(Guid id)
        => await _employeeAssignments
            .SingleOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(EmployeeAssignment employeeAssignment)
        => await _employeeAssignments.AddAsync(employeeAssignment);
    
    public async Task DeleteAsync(EmployeeAssignment employeeAssignment)
        => await Task.Run(() => _employeeAssignments.Remove(employeeAssignment));
}