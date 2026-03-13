namespace Ninjadog.Templates.CrudWebAPI.Template.Mapping;

/// <summary>
/// This template generates the DomainToDtoMapper class.
/// </summary>
public sealed class DomainToDtoMapperTemplate
    : MapperTemplateBase
{
    /// <inheritdoc />
    public override string Name => "DomainToDtoMapper";

    /// <inheritdoc />
    protected override string GetClassName()
    {
        return "DomainToDtoMapper";
    }

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
        return string.Join("\n", entities.Select(GenerateToModelDtoMethods));
    }

    private static string GenerateToModelDtoMethods(NinjadogEntityWithKey entity)
    {
        var st = entity.StringTokens;
        var properties = PropertyMappingGenerator.GenerateFromDomainMappings(entity, st.VarModel, 3);

        return $$"""

                     public static {{st.ClassModelDto}} {{st.MethodToModelDto}}(this {{st.Model}} {{st.VarModel}})
                     {
                         return new {{st.ClassModelDto}}
                         {
                             {{properties}}
                         };
                     }
                 """;
    }
}
