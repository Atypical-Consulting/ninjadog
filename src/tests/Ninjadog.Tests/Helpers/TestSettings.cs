using Ninjadog.Settings;
using Ninjadog.Settings.Config;
using Ninjadog.Settings.Entities;

namespace Ninjadog.Tests.Helpers;

/// <summary>
/// Factory methods for creating test settings with specific feature flag combinations.
/// Use these instead of one-off record types for simple configuration variations.
/// </summary>
public static class TestSettingsFactory
{
    public static ConfiguredSettings WithSoftDelete()
    {
        return TestSettingsBuilder.Default().WithSoftDelete().Build();
    }

    public static ConfiguredSettings WithAuditing()
    {
        return TestSettingsBuilder.Default().WithAuditing().Build();
    }

    public static ConfiguredSettings WithPostgres()
    {
        return TestSettingsBuilder.Default().WithDatabaseProvider("postgresql").Build();
    }

    public static ConfiguredSettings WithSqlServer()
    {
        return TestSettingsBuilder.Default().WithDatabaseProvider("sqlserver").Build();
    }

    public static ConfiguredSettings WithRelationships()
    {
        return TestSettingsBuilder.Default().WithEntities(new RelationshipEntitiesCollection()).Build();
    }

    public static ConfiguredSettings WithAot()
    {
        return TestSettingsBuilder.Default().WithAot().Build();
    }

    public static ConfiguredSettings WithAotSeeded()
    {
        return TestSettingsBuilder.Default().WithAot().WithEntities(new AotSeededEntitiesCollection()).Build();
    }

    public static ConfiguredSettings WithRateLimit()
    {
        return TestSettingsBuilder.Default().WithRateLimit().Build();
    }

    public static ConfiguredSettings WithCustomRateLimit(int permitLimit = 50, int windowSeconds = 30, int segmentsPerWindow = 3)
    {
        return TestSettingsBuilder.Default().WithRateLimit(new NinjadogRateLimitConfiguration(PermitLimit: permitLimit, WindowSeconds: windowSeconds, SegmentsPerWindow: segmentsPerWindow)).Build();
    }

    public static ConfiguredSettings WithVersioning()
    {
        return TestSettingsBuilder.Default().WithVersioning().Build();
    }

    public static ConfiguredSettings WithHeaderVersioning()
    {
        return TestSettingsBuilder.Default().WithVersioning(new NinjadogVersioningConfiguration(Strategy: "HeaderBased")).Build();
    }

    public static ConfiguredSettings WithAuth(params string[] roles)
    {
        return TestSettingsBuilder.Default().WithAuth(roles).Build();
    }
}

/// <summary>
/// Fluent builder for creating test settings with specific feature combinations.
/// </summary>
public sealed class TestSettingsBuilder
{
    private TestConfiguration _config = new();
    private NinjadogEntities _entities = new TestEntitiesCollection();
    private Dictionary<string, List<string>>? _enums;

    public static TestSettingsBuilder Default()
    {
        return new TestSettingsBuilder();
    }

    public TestSettingsBuilder WithSoftDelete(bool enabled = true)
    {
        _config = _config with { SoftDelete = enabled };
        return this;
    }

    public TestSettingsBuilder WithAuditing(bool enabled = true)
    {
        _config = _config with { Auditing = enabled };
        return this;
    }

    public TestSettingsBuilder WithDatabaseProvider(string provider)
    {
        _config = _config with { DatabaseProvider = provider };
        return this;
    }

    public TestSettingsBuilder WithAot(bool enabled = true)
    {
        _config = _config with { Aot = enabled };
        return this;
    }

    public TestSettingsBuilder WithRateLimit(NinjadogRateLimitConfiguration? rateLimit = null)
    {
        _config = _config with { RateLimit = rateLimit ?? new NinjadogRateLimitConfiguration() };
        return this;
    }

    public TestSettingsBuilder WithVersioning(NinjadogVersioningConfiguration? versioning = null)
    {
        _config = _config with { Versioning = versioning ?? new NinjadogVersioningConfiguration() };
        return this;
    }

    public TestSettingsBuilder WithAuth(params string[] roles)
    {
        _config = _config with { Auth = new NinjadogAuthConfiguration(Roles: roles) };
        return this;
    }

    public TestSettingsBuilder WithEntities(NinjadogEntities entities)
    {
        _entities = entities;
        return this;
    }

    public TestSettingsBuilder WithEnums(Dictionary<string, List<string>> enums)
    {
        _enums = enums;
        return this;
    }

    public ConfiguredSettings Build()
    {
        return new ConfiguredSettings(_config, _entities, _enums);
    }
}

public sealed record TestConfiguration()
    : NinjadogConfiguration(
        Name: "TestApp",
        Version: "1.0.0",
        Description: "Test application",
        RootNamespace: "TestApp.Api",
        OutputPath: "output",
        SaveGeneratedFiles: false);

public sealed class TestEntitiesCollection : NinjadogEntities
{
    public TestEntitiesCollection()
    {
        var guidEntity = TestEntities.CreateGuidKeyEntity();
        Add(guidEntity.Key, new(guidEntity.Properties));
    }
}

/// <summary>
/// Concrete NinjadogSettings for tests with a specific configuration and entity set.
/// </summary>
public sealed record ConfiguredSettings(NinjadogConfiguration Config, NinjadogEntities Entities, Dictionary<string, List<string>>? Enums = null)
    : NinjadogSettings(Config, Entities, Enums);

public sealed record TestSettings()
    : NinjadogSettings(new TestConfiguration(), new TestEntitiesCollection());

public sealed record EnumTestSettings()
    : NinjadogSettings(
        new TestConfiguration(),
        new TestEntitiesCollection(),
        new Dictionary<string, List<string>>
        {
            ["Priority"] = ["Low", "Medium", "High", "Critical"],
            ["Status"] = ["Draft", "Active", "Archived"],
        });

public sealed class SeededEntitiesCollection : NinjadogEntities
{
    public SeededEntitiesCollection()
    {
        var entity = TestEntities.CreateSeededEntity();
        Add(entity.Key, new(entity.Properties, null, entity.SeedData));
    }
}

public sealed record SeededSettings()
    : NinjadogSettings(new TestConfiguration(), new SeededEntitiesCollection());

public sealed class RelationshipEntitiesCollection : NinjadogEntities
{
    public RelationshipEntitiesCollection()
    {
        var parent = TestEntities.CreateParentEntity();
        var child = TestEntities.CreateChildEntity();
        Add(parent.Key, new(parent.Properties, parent.Relationships));
        Add(child.Key, new(child.Properties));
    }
}

public sealed class AotSeededEntitiesCollection : NinjadogEntities
{
    public AotSeededEntitiesCollection()
    {
        var guidEntity = TestEntities.CreateGuidKeyEntity();
        var seededEntity = TestEntities.CreateSeededEntity();
        Add(guidEntity.Key, new(guidEntity.Properties));
        Add(seededEntity.Key, new(seededEntity.Properties, null, seededEntity.SeedData));
    }
}
