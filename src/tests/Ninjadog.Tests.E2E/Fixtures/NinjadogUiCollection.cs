using System.Diagnostics.CodeAnalysis;

namespace Ninjadog.Tests.E2E.Fixtures;

[CollectionDefinition("NinjadogUi")]
[SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix", Justification = "xUnit collection fixture naming convention")]
public class NinjadogUiCollection : ICollectionFixture<NinjadogUiFixture>, ICollectionFixture<PlaywrightFixture>;
