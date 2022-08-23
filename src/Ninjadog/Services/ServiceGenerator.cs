using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;
using static Ninjadog.Helpers.Utilities;

namespace Ninjadog.Services;

[Generator]
public sealed class ServiceGenerator : NinjadogBaseGenerator
{
    protected override void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> models)
    {
        if (models.IsDefaultOrEmpty)
        {
            return;
        }

        foreach (var type in models)
        {
            StringTokens st = new(type.Name);
            var className = $"{st.Model}Service";

            context.AddSource(
                $"{GetRootNamespace(type)}.Services.{className}.g.cs",
                GenerateCode(type));
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var rootNs = GetRootNamespace(type);
        var ns = rootNs is not null ? $"{rootNs}.Services" : null;

        StringTokens _ = new(type.Name);

        var code = @$"
using {rootNs}.Domain;
using {rootNs}.Mapping;
using {rootNs}.Repositories;
using FluentValidation;
using FluentValidation.Results;

{WriteFileScopedNamespace(ns)}

public partial class {_.ClassModelService} : {_.InterfaceModelService}
{{
    private readonly {_.InterfaceModelRepository} {_.FieldModelRepository};

    public {_.ClassModelService}({_.InterfaceModelRepository} {_.VarModelRepository})
    {{
        {_.FieldModelRepository} = {_.VarModelRepository};
    }}

    public async Task<bool> CreateAsync({_.Model} {_.VarModel})
    {{
        // TODO: rename existingUser variable
        var {_.VarExistingModel} = await {_.FieldModelRepository}.GetAsync({_.VarModel}.Id.Value);
        if ({_.VarExistingModel} is not null)
        {{
            var message = $""A {_.ModelHumanized} with id {{{_.VarModel}.Id}} already exists"";
            throw new ValidationException(message, new []
            {{
                new ValidationFailure(nameof({_.Model}), message)
            }});
        }}

        var {_.VarModelDto} = {_.VarModel}.{_.MethodToModelDto}();
        return await {_.FieldModelRepository}.CreateAsync({_.VarModelDto});
    }}

    public async Task<{_.Model}?> GetAsync(Guid id)
    {{
        var {_.VarModelDto} = await {_.FieldModelRepository}.GetAsync(id);
        return {_.VarModelDto}?.{_.MethodToModel}();
    }}

    public async Task<IEnumerable<{_.Model}>> GetAllAsync()
    {{
        var {_.VarModelDtos} = await {_.FieldModelRepository}.GetAllAsync();
        return {_.VarModelDtos}.Select(x => x.{_.MethodToModel}());
    }}

    public async Task<bool> UpdateAsync({_.Model} {_.VarModel})
    {{
        var {_.VarModelDto} = {_.VarModel}.{_.MethodToModelDto}();
        return await {_.FieldModelRepository}.UpdateAsync({_.VarModelDto});
    }}

    public async Task<bool> DeleteAsync(Guid id)
    {{
        return await {_.FieldModelRepository}.DeleteAsync(id);
    }}
}}";

        return DefaultCodeLayout(code);
    }
}
