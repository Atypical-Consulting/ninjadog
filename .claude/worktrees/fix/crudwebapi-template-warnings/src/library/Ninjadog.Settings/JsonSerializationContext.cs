using System.Text.Json.Serialization;

namespace Ninjadog.Settings;

/// <summary>
/// A context for serialization and deserialization.
/// </summary>
[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DictionaryKeyPolicy = JsonKnownNamingPolicy.Unspecified,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(NinjadogSettings))]
public sealed partial class JsonSerializationContext
    : JsonSerializerContext;
