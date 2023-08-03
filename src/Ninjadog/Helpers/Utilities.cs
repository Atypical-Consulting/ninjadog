using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Ninjadog.Helpers;

internal static class Utilities
{
    internal static bool CouldBeNinjadogModelAsync(
        SyntaxNode syntaxNode,
        CancellationToken cancellationToken)
    {
        if (syntaxNode is not AttributeSyntax attribute)
        {
            return false;
        }

        var name = ExtractName(attribute.Name);

        return name is "NinjadogModel" or "NinjadogModelAttribute";
    }

    internal static string? ExtractName(NameSyntax? name)
    {
        return name switch
        {
            SimpleNameSyntax ins => ins.Identifier.Text,
            QualifiedNameSyntax qns => qns.Right.Identifier.Text,
            _ => null
        };
    }

    internal static ITypeSymbol? GetNinjadogModelTypeOrNull(
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

        return context.SemanticModel.GetDeclaredSymbol(classDeclaration) is not ITypeSymbol type || !IsNinjadogModel(type)
            ? null
            : type;
    }

    internal static bool IsNinjadogModel(ISymbol type)
    {
        return type.GetAttributes()
            .Any(a => a.AttributeClass is
            {
                Name: "NinjadogModelAttribute",
                ContainingNamespace:
                {
                    Name: "Ninjadog",
                    ContainingNamespace.IsGlobalNamespace: true
                }
            });
    }

    internal static IEnumerable<string> GetItemNames(ITypeSymbol type)
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

    internal static IEnumerable<IPropertySymbol> GetPropertiesWithGetSet(ITypeSymbol type)
    {
        return type.GetMembers()
            .Select(m =>
            {
                if (m.IsStatic ||
                    m.DeclaredAccessibility != Accessibility.Public ||
                    m is not IPropertySymbol property)
                {
                    return null;
                }

                return property;
            })
            .Where(property => property is not null)!;
    }

    internal static string? GetRootNamespace(ITypeSymbol type)
    {
        string? ns;

        if (type.ContainingNamespace.IsGlobalNamespace)
        {
            ns = null;
        }
        else
        {
            var strings = type.ContainingNamespace.ToString().Split('.');
            strings = strings.Take(strings.Length - 1).ToArray();
            ns = strings.Aggregate((s1, s2) => s1 + "." + s2);
        }

        return ns;
    }

    internal static string DefaultCodeLayout(string code)
    {
        return SourceGenerationHelper.Header +
               SourceGenerationHelper.NullableEnable +
               "\n" +
               code +
               "\n" +
               SourceGenerationHelper.NullableDisable;
    }

    internal static string? WriteFileScopedNamespace(string? ns)
    {
        return ns is not null
            ? $"namespace {ns};"
            : null;
    }

    public static IncrementalValueProvider<ImmutableArray<ITypeSymbol>> CollectNinjadogModelTypes(
        IncrementalGeneratorInitializationContext context)
    {
        return context.SyntaxProvider
            .CreateSyntaxProvider(CouldBeNinjadogModelAsync, GetNinjadogModelTypeOrNull)
            .Where(type => type is not null)
            .Collect()!;
    }
}
