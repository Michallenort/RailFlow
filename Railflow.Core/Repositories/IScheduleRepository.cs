using System.Collections;
using Railflow.Core.Entities;

namespace Railflow.Core.Repositories;

public interface IScheduleRepository
{
    Task<IEnumerable<Schedule>> GetByDateAsync(DateOnly date);
    Task<Schedule?> GetByNameAndDateAsync(string name, DateOnly date);
    Task AddRangeAsync(IEnumerable<Schedule> schedules);
    Task DeleteRangeAsync(IEnumerable<Schedule> schedules);
}