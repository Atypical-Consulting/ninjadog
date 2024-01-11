using Ninjadog.Settings;

namespace Ninjadog.Templates;

public record TemplateContext(
    NinjadogConfiguration Config,
    NinjadogEntities Entities);
