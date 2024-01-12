// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Helpers;

public abstract class NinjadogIncrementalGeneratorBase : IIncrementalGenerator
{
    protected abstract IncrementalGeneratorSetup Setup { get; }

    /// <summary>
    /// Initializes the incremental generator with the specified context.
    /// </summary>
    /// <param name="context">The incremental generator initialization context.</param>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(
            CollectNinjadogModelTypes(context),
            GenerateCode);
    }

    /// <summary>
    /// Generates code based on the specified context and models.
    /// </summary>
    /// <param name="context">The source production context.</param>
    /// <param name="models">The array of ITypeSymbol models.</param>
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

    /// <summary>
    /// Generates a single file containing the generated code for all models.
    /// </summary>
    /// <param name="context">The source production context.</param>
    /// <param name="models">The array of ITypeSymbol models.</param>
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

    /// <summary>
    /// Generates multiple files, with each file containing the generated code for a single model.
    /// </summary>
    /// <param name="context">The source production context.</param>
    /// <param name="models">The array of ITypeSymbol models.</param>
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
