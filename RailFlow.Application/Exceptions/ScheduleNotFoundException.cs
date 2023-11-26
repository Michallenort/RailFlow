using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class ScheduleNotFoundException : NotFoundException
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateOnly Date { get; set; }
    public ScheduleNotFoundException(string name, DateOnly date) : 
        base($"Schedule with name: '{name}' and date: '{date}' does not exists.")
    {
        Name = name;
        Date = date;
    }
    
    
    public ScheduleNotFoundException(Guid id) : 
        base($"Schedule with id: '{id}' does not exists.")
    {
        Id = id;
    }
}