[![.NET](https://github.com/aimenux/CustomFeatureProviderDemo/actions/workflows/ci.yml/badge.svg)](https://github.com/aimenux/CustomFeatureProviderDemo/actions/workflows/ci.yml)

# CustomFeatureProviderDemo
```
Using custom feature definition provider in order to read features
```

> In this demo, i m using a [custom feature definition provider](https://github.com/microsoft/FeatureManagement-Dotnet#custom-feature-providers) in order to read features from in-memory storage and not from json file (which is the default behaviour).
>
> In addition to the custom feature definition provider, i m using a [custom feature manager snapshot](https://github.com/microsoft/FeatureManagement-Dotnet#snapshot) in order to prevent inconsistency of feature value during a single request.
> 

**`Tools`** : vs22, net 6.0, feature management
