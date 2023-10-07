using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class InvalidStopException : CustomException
{
    public InvalidStopException() : base($"Start and end station are not compliant.")
    {
        
    }
}