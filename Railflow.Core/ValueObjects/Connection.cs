using Railflow.Core.Entities;

namespace Railflow.Core.ValueObjects;

public record Connection(IEnumerable<SubConnection> SubConnections);