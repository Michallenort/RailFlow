using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class NullException : CustomException
{
    public string Item { get; set; }
    public Guid Id { get; set; }
    
    public NullException(string item, Guid id) : base($"{item} with id: {id} can not be null.")
    {
        Item = item;
        Id = id;
    }
}