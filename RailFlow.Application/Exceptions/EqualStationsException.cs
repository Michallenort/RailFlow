using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class EqualStationsException : CustomException
{
    public EqualStationsException() : base($"Start and end stations cannot be the same.")
    {

    }
}