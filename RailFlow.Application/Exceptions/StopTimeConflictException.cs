using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class StopTimeConflictException : CustomException
{
    public StopTimeConflictException() : base("Stop time conflict.")
    {
    }
}