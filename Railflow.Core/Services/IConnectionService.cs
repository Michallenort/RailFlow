using Railflow.Core.ValueObjects;

namespace Railflow.Core.Services;

public interface IConnectionService
{
    Task<IEnumerable<Connection>> FindConnectionAsync(string startStation, string endStation,
        DateOnly date);
}