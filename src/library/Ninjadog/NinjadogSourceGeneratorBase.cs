// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;
using Ninjadog.Configuration;

namespace Ninjadog;

public abstract class NinjadogSourceGeneratorBase
    : ISourceGenerator
{
    // protected abstract SourceGeneratorSetup Setup { get; }

    public virtual void Initialize(GeneratorInitializationContext context)
    {
        // No initialization required for this generator.
    }

    public virtual void Execute(GeneratorExecutionContext context)
    {
        // If you would like to put some data to non-compilable file (e.g. a .json file), mark it as an Additional File.

        // Go through all files marked as an Additional File in file properties.
        foreach (var additionalFile in context.AdditionalFiles)
        {
            if (additionalFile == null)
            {
                continue;
            }

            // Check if the file name is the specific file that we expect.
            if (Path.GetFileName(additionalFile.Path) != "ninjadogsettings.json")
            {
                continue;
            }

            var text = additionalFile.GetText();
            if (text == null)
            {
                continue;
            }

            if (JsonSerializer.Deserialize(
                    text.ToString(),
                    typeof(NinjadogSettings),
                    JsonSerializationContext.Default) is not NinjadogSettings ninjadogConfig)
            {
                continue;
            }

            GenerateCode(context, ninjadogConfig);
        }
    }

    protected abstract void GenerateCode(
        GeneratorExecutionContext context,
        NinjadogSettings ninjadogSettings);
}

public abstract class NinjadogSingleFileSourceGeneratorBase
    : NinjadogSourceGeneratorBase
{
    /// <summary>
    /// Generates a single file containing the generated code for all models.
    /// </summary>
    /// <param name="context">The generator execution context.</param>
    /// <param name="ninjadogSettings">The ninjadog configuration.</param>
    protected override void GenerateCode(
        GeneratorExecutionContext context,
        NinjadogSettings ninjadogSettings)
    {
        // var className = Setup.GetClassName();
        // var subNamespace = Setup.SubNamespace;

        // old version
        // var typeContexts = ninjadogConfig
        //     .Entities
        //     .Select(model =>
        //     {
        //         var st = new StringTokens(model.Key);
        //         return new TypeContext(model, className, subNamespace, st);
        //     })
        //     .ToImmutableArray();

        // no need to create TypeContext apparently
        // var typeContexts = ninjadogConfig
        //     .Entities
        //     .Select(model => new StringTokens(model.Key))
        //     .ToImmutableArray();

        // var hintName = $"YourWebAPI.{className}.g.cs";
        // var code = Setup.GenerateCode(ninjadogSettings);

        // context.AddSource(hintName, code);
    }
}

public abstract class NinjadogMultipleFilesSourceGeneratorBase
    : NinjadogSourceGeneratorBase
{
    /// <summary>
    /// Generates multiple files, with each file containing the generated code for a single model.
    /// </summary>
    /// <param name="context">The generator execution context.</param>
    /// <param name="ninjadogSettings">The ninjadog configuration.</param>
    protected override void GenerateCode(
        GeneratorExecutionContext context,
        NinjadogSettings ninjadogSettings)
    {
        foreach (var entity in ninjadogSettings.Entities.FromKeys())
        {
            var code = GenerateCode(ninjadogSettings.Config, entity);
            var hintName = $"Chocapic.{entity.GetStringTokens().ClassModelDto}.g.cs";
            context.AddSource(hintName, code);
        }
    }

    protected abstract string GenerateCode(
        NinjadogConfiguration config,
        NinjadogEntityWithKey entity);
}
