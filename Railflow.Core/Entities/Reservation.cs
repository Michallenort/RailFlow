

namespace Railflow.Core.Entities;

public class Reservation
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid FirstScheduleId { get; set; }
    public Schedule FirstSchedule { get; set; }
    public Guid? SecondScheduleId { get; set; }
    public Schedule? SecondSchedule { get; set; }
    public Guid StartStopId { get; set; }
    public Stop StartStop { get; set; }
    public TimeOnly StartHour { get; set; }
    public Guid EndStopId { get; set; }
    public Stop EndStop { get; set; }
    public TimeOnly EndHour { get; set; }
    public Guid? TransferStopId { get; set; }
    public Stop? TransferStop { get; set; }
    public long Price { get; set; }
    
    public Reservation()
    {
        
    }
    
    public Reservation(Guid id, DateOnly date, Guid userId, Guid firstScheduleId, Guid? secondScheduleId, 
        Guid startStopId, TimeOnly startHour, Guid endStopId, TimeOnly endHour, Guid? transferStopId, long price)
    {
        Id = id;
        Date = date;
        UserId = userId;
        FirstScheduleId = firstScheduleId;
        SecondScheduleId = secondScheduleId;
        StartStopId = startStopId;
        StartHour = startHour;
        EndStopId = endStopId;
        EndHour = endHour;
        TransferStopId = transferStopId;
        Price = price;
    }
}