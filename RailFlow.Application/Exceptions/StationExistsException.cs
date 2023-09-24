using Railflow.Core.Exceptions;

namespace RailFlow.Application.Exceptions;

internal sealed class StationExistsException : CustomException
{
    public string StationName { get; set; }
    public StationExistsException(string stationName) : base($"Station: '{stationName}' already exists.")
    {
        StationName = stationName;
    }
}