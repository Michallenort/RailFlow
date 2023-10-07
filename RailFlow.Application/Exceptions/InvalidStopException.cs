using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class InvalidStopException : CustomException
{
    public InvalidStopException() : base($"Start and end station cannot be the same.")
    {
        
    }
}