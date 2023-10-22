using Railflow.Core.Entities;

namespace Railflow.Core.ValueObjects;

public record SubConnection(Schedule Schedule, IEnumerable<Stop> Stops);