using System.Collections.Concurrent;
using Microsoft.FeatureManagement;

namespace WebApi.Features;

public class InMemoryFeatureManagerSnapshot : IFeatureManagerSnapshot
{
    private readonly IFeatureManager _featureManager;
    private readonly IDictionary<string, bool> _featureFlags;

    public InMemoryFeatureManagerSnapshot(IFeatureManager featureManager)
    {
        _featureManager = featureManager ?? throw new ArgumentNullException(nameof(featureManager));
        _featureFlags = new ConcurrentDictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
    }

    public async Task<bool> IsEnabledAsync(string feature)
    {
        if (!_featureFlags.TryGetValue(feature, out var isEnabled))
        {
            isEnabled = await _featureManager.IsEnabledAsync(feature);
            _featureFlags[feature] = isEnabled;
        }

        return isEnabled;
    }

    public async Task<bool> IsEnabledAsync<TContext>(string feature, TContext context)
    {
        if (!_featureFlags.TryGetValue(feature, out var isEnabled))
        {
            isEnabled = await _featureManager.IsEnabledAsync(feature, context);
            _featureFlags[feature] = isEnabled;
        }

        return isEnabled;
    }

    public async IAsyncEnumerable<string> GetFeatureNamesAsync()
    {
        foreach (var featureName in Enum.GetNames<FeatureFlags>())
        {
            yield return await Task.FromResult(featureName);
        }
    }
}