using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class InvalidEmailOrPasswordException : CustomException
{
    public InvalidEmailOrPasswordException() : base("Invalid email or password.")
    {
    }
}