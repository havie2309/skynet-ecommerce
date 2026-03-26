using System.Collections.Concurrent;

namespace Skinet.Api.Services;

public class AppMetrics
{
    private readonly ConcurrentDictionary<string, long> _counters = new();

    public void Increment(string key)
    {
        _counters.AddOrUpdate(key, 1, (_, current) => current + 1);
    }

    public IReadOnlyDictionary<string, long> Snapshot()
    {
        return new Dictionary<string, long>(_counters);
    }
}
