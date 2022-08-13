using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SourceGenerator;

[Generator]
public sealed class DemoGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValueProvider<ImmutableArray<ITypeSymbol>> enumTypes = context.SyntaxProvider
            .CreateSyntaxProvider(CouldBeEnumerationAsync, GetEnumTypeOrNull)
            .Where(type => type is not null)
            .Collect()!;
        
        context.RegisterSourceOutput(enumTypes, GenerateCode);
    }

    private static bool CouldBeEnumerationAsync(
        SyntaxNode syntaxNode,
        CancellationToken cancellationToken)
    {
        if (syntaxNode is not AttributeSyntax attribute)
        {
            return false;
        }

        string? name = ExtractName(attribute.Name);

        return name is "EnumGeneration" or "EnumGenerationAttribute";
    }

    private static string? ExtractName(NameSyntax? name)
    {
        return name switch
        {
            SimpleNameSyntax ins => ins.Identifier.Text,
            QualifiedNameSyntax qns => qns.Right.Identifier.Text,
            _ => null
        };
    }

    private static ITypeSymbol? GetEnumTypeOrNull(
        GeneratorSyntaxContext context,
        CancellationToken cancellationToken)
    {
        var attributeSyntax = (AttributeSyntax)context.Node;

        // "attribute.Parent" is "AttributeListSyntax"
        // "attribute.Parent.Parent" is a C# fragment the attributes are applied to
        if (attributeSyntax.Parent?.Parent is not ClassDeclarationSyntax classDeclaration)
        {
            return null;
        }

        var type = context.SemanticModel.GetDeclaredSymbol(classDeclaration) as ITypeSymbol;

        return type is null || !IsEnumeration(type) ? null : type;
    }

    private static bool IsEnumeration(ISymbol type)
    {
        return type.GetAttributes()
            .Any(a => a.AttributeClass is
            {
                Name: "EnumGenerationAttribute",
                ContainingNamespace:
                {
                    Name: "DemoLibrary",
                    ContainingNamespace.IsGlobalNamespace: true
                }
            });
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
            var typeNamespace = type.ContainingNamespace.IsGlobalNamespace
                ? null
                : $"{type.ContainingNamespace}.";

            context.AddSource($"{typeNamespace}{type.Name}.g.cs", code);
        }
    }

    private static string GenerateCode(ITypeSymbol type)
    {
        var ns = type.ContainingNamespace.IsGlobalNamespace
            ? null
            : type.ContainingNamespace.ToString();
        var name = type.Name;
        var lower = name.ToLower();
        var dto = $"{name}Dto";
        var items = GetItemNames(type);

        return StringConstants.FileHeader + @$"

using System.Collections.Generic;
using DemoLibrary.Database;
using Dapper;

{(ns is null ? null : $@"namespace {ns}
{{")}
    partial class {name}
    {{
        private static IReadOnlyList<{name}> _items;
        public static IReadOnlyList<{name}> Items => _items ??= GetItems();

        private static IReadOnlyList<{name}> GetItems()
        {{
            return new[] {{ {string.Join(", ", items)} }};
        }}
    }}
{(ns is null ? null : @"}
")}";
    }

    private static IEnumerable<string> GetItemNames(ITypeSymbol type)
    {
        return type.GetMembers()
            .Select(m =>
            {
                if (!m.IsStatic ||
                    m.DeclaredAccessibility != Accessibility.Public ||
                    m is not IFieldSymbol field)
                {
                    return null;
                }

                return SymbolEqualityComparer.Default.Equals(field.Type, type)
                    ? field.Name
                    : null;
            })
            .Where(field => field is not null)!;
    }
}