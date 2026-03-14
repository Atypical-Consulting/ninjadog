namespace Ninjadog.Templates.CrudWebAPI.Template.Mapping;

/// <summary>
/// This template generates the DtoToDomainMapper class.
/// </summary>
public sealed class DtoToDomainMapperTemplate : MapperTemplateBase
{
    /// <inheritdoc />
    public override string Name => "DtoToDomainMapper";

    /// <inheritdoc />
    protected override string GenerateUsings(string rootNamespace)
    {
        return $"""
                using {rootNamespace}.Contracts.Data;
                using {rootNamespace}.Domain;
                """;
    }

    /// <inheritdoc />
    protected override string GenerateMethods(List<NinjadogEntityWithKey> entities)
    {
        return string.Join("\n", entities.Select(GenerateToModelMethods));
    }

    private static string GenerateToModelMethods(NinjadogEntityWithKey entity)
    {
        var st = entity.StringTokens;
        var properties = PropertyMappingGenerator.GenerateToDomainMappings(entity, st.VarModelDto, 3, generateAutoKey: false);

        return $$"""

                     public static {{st.Model}} {{st.MethodToModel}}(this {{st.ClassModelDto}} {{st.VarModelDto}})
                     {
                         return new {{st.Model}}
                         {
                             {{properties}}
                         };
                     }
                 """;
    }
}
