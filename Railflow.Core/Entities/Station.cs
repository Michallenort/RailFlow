using Railflow.Core.ValueObjects;

namespace Railflow.Core.Entities;

public class Station
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Address Address { get; private set; }
    public ICollection<Stop> Stops { get; private set; }
    
    public Station()
    {
        
    }
    
    public Station(Guid id, string name, Address address)
    {
        Id = id;
        Name = name;
        Address = address;
    }
}