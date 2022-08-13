using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SourceGenerator;

public static class Utilities
{
    internal static bool CouldBeEnumerationAsync(
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

    internal static string? ExtractName(NameSyntax? name)
    {
        return name switch
        {
            SimpleNameSyntax ins => ins.Identifier.Text,
            QualifiedNameSyntax qns => qns.Right.Identifier.Text,
            _ => null
        };
    }

    internal static ITypeSymbol? GetEnumTypeOrNull(
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

    internal static bool IsEnumeration(ISymbol type)
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

    internal static IEnumerable<string> GetPropertiesWithGetSet(ITypeSymbol type)
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

    internal static string? GetRootNamespace(ITypeSymbol type)
    {
        string? ns;

        if (type.ContainingNamespace.IsGlobalNamespace)
        {
            ns = null;
        }
        else
        {
            string[] strings = type.ContainingNamespace.ToString().Split('.');
            strings = strings.Take(strings.Length - 1).ToArray();
            ns = strings.Aggregate((s1, s2) => s1 + "." + s2);
        }

        return ns;
    }
}