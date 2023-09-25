using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class TrainExistsException : CustomException
{
    public int TrainNumber { get; set; }
    
    public TrainExistsException(int trainNumber) : base($"Train: '{trainNumber}' already exists.")
    {
        TrainNumber = trainNumber;
    }
}