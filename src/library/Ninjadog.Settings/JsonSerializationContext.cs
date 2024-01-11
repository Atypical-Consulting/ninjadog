using System.Text.Json.Serialization;

namespace Ninjadog.Settings;

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DictionaryKeyPolicy = JsonKnownNamingPolicy.Unspecified)]
[JsonSerializable(typeof(NinjadogSettings))]
internal partial class JsonSerializationContext
    : JsonSerializerContext;
