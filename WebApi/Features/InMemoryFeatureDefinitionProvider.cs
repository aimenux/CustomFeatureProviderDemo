using Microsoft.FeatureManagement;

namespace WebApi.Features
{
    public class InMemoryFeatureDefinitionProvider : IFeatureDefinitionProvider
    {
        public Task<FeatureDefinition> GetFeatureDefinitionAsync(string featureName)
        {
            var features = GetFeatures();

            if (features.TryGetValue(featureName, out var isEnabled))
            {
                return Task.FromResult(GetFeatureDefinition(featureName, isEnabled));
            }

            return Task.FromResult(default(FeatureDefinition));
        }

        public async IAsyncEnumerable<FeatureDefinition> GetAllFeatureDefinitionsAsync()
        {
            var features = GetFeatures();
            foreach (var (featureName, _) in features)
            {
                var featureDefinition = await GetFeatureDefinitionAsync(featureName);
                yield return featureDefinition;
            }
        }

        private static FeatureDefinition GetFeatureDefinition(string featureName, bool isEnabled)
        {
            var featureDefinition = new FeatureDefinition
            {
                Name = featureName
            };

            if (isEnabled)
            {
                featureDefinition.EnabledFor = new[]
                {
                    new FeatureFilterConfiguration
                    {
                        Name = "AlwaysOn"
                    }
                };
            }

            return featureDefinition;
        }

        private static IDictionary<string, bool> GetFeatures()
        {
            return new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase)
            {
                [nameof(FeatureFlags.FeatureA)] = true,
                [nameof(FeatureFlags.FeatureB)] = false
            };
        }
    }
}
