namespace Railflow.Core.Entities;

public class Role
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    
    public Role(int id, string name)
    {
        Id = id;
        Name = name;
    }
    
}