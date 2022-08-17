using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SourceGenerator.Services;

[Generator]
public sealed class ServiceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValueProvider<ImmutableArray<ITypeSymbol>> enumTypes = context.SyntaxProvider
            .CreateSyntaxProvider(Utilities.CouldBeEnumerationAsync, Utilities.GetEnumTypeOrNull)
            .Where(type => type is not null)
            .Collect()!;

        context.RegisterSourceOutput(enumTypes, GenerateCode);
    }

    private static void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> enumerations)
    {
        if (enumerations.IsDefaultOrEmpty)
        {
            return;
        }

        foreach (var type in enumerations)
        {
            var code = GenerateCode(type);
            var typeNamespace = Utilities.GetRootNamespace(type) + ".Services";

            StringVariations sv = new(type.Name);
            var className = $"{sv.Pascal}Service";

            context.AddSource($"{typeNamespace}.{className}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        string? rootNs = Utilities.GetRootNamespace(type);
        string? ns = rootNs is not null ? $"{rootNs}.Services" : null;
        StringVariations sv = new(type.Name);

        var name = type.Name;
        var lower = name.ToLower();
        var dto = $"{name}Dto";
        var items = Utilities.GetItemNames(type);

        return StringConstants.FileHeader + @$"

using {rootNs}.Domain;
using {rootNs}.Mapping;
using {rootNs}.Repositories;
using FluentValidation;
using FluentValidation.Results;

{(ns is null ? null : $@"namespace {ns}
{{")}
    public partial class {name}Service : I{name}Service
    {{
        private readonly I{name}Repository _{lower}Repository;

        public {name}Service(I{name}Repository {lower}Repository)
        {{
            _{lower}Repository = {lower}Repository;
        }}

        public async Task<bool> CreateAsync({name} {lower})
        {{
            var existingUser = await _{lower}Repository.GetAsync({lower}.Id.Value);
            if (existingUser is not null)
            {{
                var message = $""A user with id {{{lower}.Id}} already exists"";
                throw new ValidationException(message, new []
                {{
                    new ValidationFailure(nameof({name}), message)
                }});
            }}

            var {lower}Dto = {lower}.To{name}Dto();
            return await _{lower}Repository.CreateAsync({lower}Dto);
        }}

        public async Task<{name}?> GetAsync(Guid id)
        {{
            var {lower}Dto = await _{lower}Repository.GetAsync(id);
            return {lower}Dto?.To{name}();
        }}

        public async Task<IEnumerable<{name}>> GetAllAsync()
        {{
            var {lower}Dtos = await _{lower}Repository.GetAllAsync();
            return {lower}Dtos.Select(x => x.To{name}());
        }}

        public async Task<bool> UpdateAsync({name} {lower})
        {{
            var {lower}Dto = {lower}.To{name}Dto();
            return await _{lower}Repository.UpdateAsync({lower}Dto);
        }}

        public async Task<bool> DeleteAsync(Guid id)
        {{
            return await _{lower}Repository.DeleteAsync(id);
        }}
    }}
{(ns is null ? null : @"}
")}";
    }
}
