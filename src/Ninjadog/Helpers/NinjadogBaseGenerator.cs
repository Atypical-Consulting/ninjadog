namespace Ninjadog.Helpers;

public abstract class NinjadogBaseGenerator : IIncrementalGenerator
{
    protected abstract GeneratorSetup Setup { get; }

    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(
            CollectNinjadogModelTypes(context),
            GenerateCode);
    }

    private void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> models)
    {
        if (models.IsDefaultOrEmpty)
        {
            return;
        }

        try
        {
            switch (Setup.CodeGenerationMode)
            {
                case CodeGenerationMode.SingleFile:
                    GenerateSingleFile(context, models);
                    break;
                case CodeGenerationMode.ByModel:
                    GenerateMultipleFiles(context, models);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }

    private void GenerateSingleFile(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> models)
    {
        var className = Setup.GetClassName();
        var subNamespace = Setup.SubNamespace;

        var typeContexts = models
            .Select(model =>
            {
                var st = new StringTokens(model.Name);
                return new TypeContext(model, className, subNamespace, st);
            })
            .ToImmutableArray();

        var hintName = $"{typeContexts[0].Ns}.{className}.g.cs";
        var code = Setup.GenerateCode(typeContexts);

        context.AddSource(hintName, code);
    }

    private void GenerateMultipleFiles(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> models)
    {
        foreach (var model in models)
        {
            var st = new StringTokens(model.Name);
            var className = Setup.GetClassName(st);
            var subNamespace = Setup.SubNamespace;
            var typeContext = new TypeContext(model, className, subNamespace, st);

            var code = Setup.GenerateCode(typeContext);

            context.AddSource(typeContext.HintName, code);
        }
    }
}
