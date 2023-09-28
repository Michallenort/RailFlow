namespace Railflow.Core.Entities;

public class Stop
{
    public Guid Id { get; private set; }
    public TimeOnly ArrivalHour { get; private set; }
    public TimeOnly DepartureHour { get; private set; }
    public Guid StationId { get; private set; }
    public Station Station { get; private set; }
    public Guid RouteId { get; private set; }
    public Route Route { get; private set; }
    
    public Stop()
    {
        
    }
    
    public Stop(Guid id, TimeOnly arrivalHour, TimeOnly departureHour, Guid stationId, Guid routeId)
    {
        Id = id;
        ArrivalHour = arrivalHour;
        DepartureHour = departureHour;
        StationId = stationId;
        RouteId = routeId;
    }
    
    public void Update(TimeOnly? arrivalHour, TimeOnly? departureHour)
    {
        ArrivalHour = arrivalHour ?? ArrivalHour;
        DepartureHour = departureHour ?? DepartureHour;
    }
}