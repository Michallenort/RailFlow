namespace Railflow.Core.Entities;

public class Train
{
    public Guid Id { get; private set; }
    public int Number { get; private set; }
    public float? MaxSpeed { get; private set; }
    public int Capacity { get; private set; }

    public Train(Guid id, int number, float? maxSpeed, int capacity)
    {
        Id = id;
        Number = number;
        MaxSpeed = maxSpeed;
        Capacity = capacity;
    }
}