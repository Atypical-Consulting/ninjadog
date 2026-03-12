namespace Ninjadog.Templates.CrudWebAPI.Template;

/// <summary>
/// This template generates the AppJsonSerializerContext.cs file for Native AOT support.
/// It produces a System.Text.Json source-generated serializer context with [JsonSerializable]
/// attributes for all Request, Response, and DTO types.
/// </summary>
public sealed class JsonSerializerContextTemplate : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "JsonSerializerContext";

    /// <inheritdoc />
    public override NinjadogContentFile GenerateOne(NinjadogSettings ninjadogSettings)
    {
        if (!ninjadogSettings.Config.Aot)
        {
            return NinjadogContentFile.Empty;
        }

        var rootNamespace = ninjadogSettings.Config.RootNamespace;
        var entities = ninjadogSettings.Entities.FromKeys();
        const string fileName = "AppJsonSerializerContext.cs";

        var content =
            $$"""

              using System.Text.Json.Serialization;
              using {{rootNamespace}}.Contracts.Data;
              using {{rootNamespace}}.Contracts.Requests;
              using {{rootNamespace}}.Contracts.Responses;
              using {{rootNamespace}}.Domain;

              {{WriteFileScopedNamespace(rootNamespace)}}

              [JsonSourceGenerationOptions(
                  PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
                  DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
              {{GenerateSerializableAttributes(entities)}}
              public sealed partial class AppJsonSerializerContext : JsonSerializerContext;
              """;

        return CreateNinjadogContentFile(fileName, content);
    }

    private static string GenerateSerializableAttributes(List<NinjadogEntityWithKey> entities)
    {
        IndentedStringBuilder sb = new(0);

        // ErrorResponse used by the validation exception handler
        sb.AppendLine("[JsonSerializable(typeof(ErrorResponse))]");

        foreach (var entity in entities)
        {
            var st = entity.StringTokens;

            // Domain entity
            sb.AppendLine($"[JsonSerializable(typeof({st.Model}))]");

            // DTO
            sb.AppendLine($"[JsonSerializable(typeof({st.ClassModelDto}))]");

            // Requests
            sb.AppendLine($"[JsonSerializable(typeof({st.ClassCreateModelRequest}))]");
            sb.AppendLine($"[JsonSerializable(typeof({st.ClassUpdateModelRequest}))]");

            // Responses
            sb.AppendLine($"[JsonSerializable(typeof({st.ClassModelResponse}))]");
            sb.AppendLine($"[JsonSerializable(typeof({st.ClassGetAllModelsResponse}))]");
        }

        // Remove trailing newline
        var result = sb.ToString().TrimEnd('\n', '\r');
        return result;
    }
}
