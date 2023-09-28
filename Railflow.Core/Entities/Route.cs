namespace Railflow.Core.Entities;

public class Route
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid StartStationId { get; private set; }
    public Station StartStation { get; private set; }
    public Guid EndStationId { get; private set; }
    public Station EndStation { get; private set; }
    public Guid TrainId { get; private set; }
    public Train Train { get; private set; }
    public ICollection<Stop> Stops { get; private set; }
    public bool IsActive { get; private set; }

    public Route()
    {
        
    }
    
    public Route(Guid id, string name, Guid startStationId, Guid endStationId, Guid trainId, bool isActive)
    {
        Id = id;
        Name = name;
        StartStationId = startStationId;
        EndStationId = endStationId;
        TrainId = trainId;
        IsActive = isActive;
    }
    
    public void Update(string? name, Guid? startStationId, Guid? endStationId, Guid? trainId)
    {
        Name = name ?? Name;
        StartStationId = startStationId ?? StartStationId;
        EndStationId = endStationId ?? EndStationId;
        TrainId = trainId ?? TrainId;
    }
    
    public void SetIsActive(bool isActive)
    {
        IsActive = isActive;
    }
}