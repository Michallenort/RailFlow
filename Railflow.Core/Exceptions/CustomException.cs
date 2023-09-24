namespace Railflow.Core.Exceptions;

public abstract class CustomException : Exception
{
    protected CustomException(string stationName) : base(stationName)
    {
    }   
}