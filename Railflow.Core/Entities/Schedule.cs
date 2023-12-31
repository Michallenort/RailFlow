namespace Railflow.Core.Entities;

public class Schedule
{
    public Guid Id { get; private set; }
    public DateOnly Date { get; private set; }
    public Guid RouteId { get; private set; }
    public Route Route { get; set; }
    public ICollection<EmployeeAssignment> EmployeeAssignments { get; private set; }

    public Schedule()
    {
        
    }
    
    public Schedule(Guid id, DateOnly date, Guid routeId)
    {
        Id = id;
        Date = date;
        RouteId = routeId;
    }
}