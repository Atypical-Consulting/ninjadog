// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Ninjadog.Helpers;

namespace Ninjadog;

public abstract class NinjadogBaseGenerator : IIncrementalGenerator
{
    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(
            source: Utilities.CollectNinjadogModelTypes(context),
            action: GenerateCode);
    }

    protected abstract void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<ITypeSymbol> models);
}
