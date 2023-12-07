using Railflow.Core.Entities;

namespace Railflow.Core.ValueObjects;

public record Connection(IEnumerable<SubConnection> SubConnections, string StartStationName,
    string EndStationName, TimeOnly StartHour, TimeOnly EndHour, long Price);