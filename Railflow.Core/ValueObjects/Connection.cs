using Railflow.Core.Entities;

namespace Railflow.Core.ValueObjects;

public record Connection(IEnumerable<SubConnection> SubConnections, Guid StartStopId, string StartStationName,
    Guid EndStopId, string EndStationName, TimeOnly StartHour, TimeOnly EndHour, long Price);