using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class DeleteException : CustomException
{
    public string Item { get; set; }
    
    public DeleteException(string item) : base($"{item} could not be deleted.")
    {
        Item = item;
    }
}