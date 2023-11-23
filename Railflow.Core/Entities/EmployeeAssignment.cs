namespace Railflow.Core.Entities;

public class EmployeeAssignment
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }
    public Guid ScheduleId { get; private set; }
    public Schedule Schedule { get; private set; }
    public TimeOnly StartHour { get; private set; }
    public TimeOnly EndHour { get; private set; }
    
    public EmployeeAssignment()
    {
        
    }
    
    public EmployeeAssignment(Guid id, Guid userId, Guid scheduleId, TimeOnly startHour, TimeOnly endHour)
    {
        Id = id;
        UserId = userId;
        ScheduleId = scheduleId;
        StartHour = startHour;
        EndHour = endHour;
    }
}