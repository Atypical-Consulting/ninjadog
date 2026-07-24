namespace Ninjadog.Templates.CrudWebAPI.Template.Mapping;

/// <summary>
/// This template generates the ApiContractToDomainMapper class.
/// </summary>
public sealed class ApiContractToDomainMapperTemplate
    : MapperTemplateBase
{
    /// <inheritdoc />
    public override string Name => "ApiContractToDomainMapper";

    /// <inheritdoc />
    protected override string GenerateUsings(string rootNamespace)
    {
        return $"""
                using {rootNamespace}.Contracts.Requests;
                using {rootNamespace}.Domain;
                """;
    }

    /// <inheritdoc />
    protected override string GenerateMethods(List<NinjadogEntityWithKey> entities)
    {
        var toModelFromCreateMethods = string.Join("\n", entities.Select(GenerateToModelFromCreateMethods));
        var toModelFromUpdateMethods = string.Join("\n", entities.Select(GenerateToModelFromUpdateMethods));

        return $"{toModelFromCreateMethods}\n    {toModelFromUpdateMethods}";
    }

    private static string GenerateToModelFromCreateMethods(NinjadogEntityWithKey entity)
    {
        var st = entity.StringTokens;
        var properties = PropertyMappingGenerator.GenerateToDomainMappings(entity, "request", 3, generateAutoKey: true);

        return $$"""

                     public static {{st.Model}} {{st.MethodToModel}}(this {{st.ClassCreateModelRequest}} request)
                     {
                         return new {{st.Model}}
                         {
                             {{properties}}
                         };
                     }
                 """;
    }

    private static string GenerateToModelFromUpdateMethods(NinjadogEntityWithKey entity)
    {
        var st = entity.StringTokens;
        var properties = PropertyMappingGenerator.GenerateToDomainMappings(entity, "request", 3, generateAutoKey: false);

        return
            $$"""

                  public static {{st.Model}} {{st.MethodToModel}}(this {{st.ClassUpdateModelRequest}} request)
                  {
                      return new {{st.Model}}
                      {
                          {{properties}}
                      };
                  }
              """;
    }
}
