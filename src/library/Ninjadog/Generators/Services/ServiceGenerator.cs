namespace Ninjadog.Services;

[Generator]
public sealed class ServiceGenerator : NinjadogBaseGenerator
{
    /// <inheritdoc />
    protected override GeneratorSetup Setup
        => new(
            st => $"{st.Model}Service",
            GenerateCode,
            "Services");

    private static string GenerateCode(TypeContext typeContext)
    {
        var (st, ns) = typeContext;
        var rootNs = typeContext.RootNamespace;

        var code = $$"""
            
            using {{rootNs}}.Domain;
            using {{rootNs}}.Mapping;
            using {{rootNs}}.Repositories;
            using FluentValidation;
            using FluentValidation.Results;
            
            {{WriteFileScopedNamespace(ns)}}
            
            public partial class {{st.ClassModelService}} : {{st.InterfaceModelService}}
            {
                private readonly {{st.InterfaceModelRepository}} {{st.FieldModelRepository}};
            
                public {{st.ClassModelService}}({{st.InterfaceModelRepository}} {{st.VarModelRepository}})
                {
                    {{st.FieldModelRepository}} = {{st.VarModelRepository}};
                }
            
                public async Task<bool> CreateAsync({{st.Model}} {{st.VarModel}})
                {
                    // TODO: rename existingUser variable
                    var {{st.VarExistingModel}} = await {{st.FieldModelRepository}}.GetAsync({{st.VarModel}}.Id.Value);
                    if ({{st.VarExistingModel}} is not null)
                    {
                        var message = $"A {{st.ModelHumanized}} with id {{{st.VarModel}}.Id} already exists";
                        throw new ValidationException(message, new []
                        {
                            new ValidationFailure(nameof({{st.Model}}), message)
                        });
                    }
            
                    var {{st.VarModelDto}} = {{st.VarModel}}.{{st.MethodToModelDto}}();
                    return await {{st.FieldModelRepository}}.CreateAsync({{st.VarModelDto}});
                }
            
                public async Task<{{st.Model}}?> GetAsync(Guid id)
                {
                    var {{st.VarModelDto}} = await {{st.FieldModelRepository}}.GetAsync(id);
                    return {{st.VarModelDto}}?.{{st.MethodToModel}}();
                }
            
                public async Task<IEnumerable<{{st.Model}}>> GetAllAsync()
                {
                    var {{st.VarModelDtos}} = await {{st.FieldModelRepository}}.GetAllAsync();
                    return {{st.VarModelDtos}}.Select(x => x.{{st.MethodToModel}}());
                }
            
                public async Task<bool> UpdateAsync({{st.Model}} {{st.VarModel}})
                {
                    var {{st.VarModelDto}} = {{st.VarModel}}.{{st.MethodToModelDto}}();
                    return await {{st.FieldModelRepository}}.UpdateAsync({{st.VarModelDto}});
                }
            
                public async Task<bool> DeleteAsync(Guid id)
                {
                    return await {{st.FieldModelRepository}}.DeleteAsync(id);
                }
            }
            """;

        return DefaultCodeLayout(code);
    }
}
