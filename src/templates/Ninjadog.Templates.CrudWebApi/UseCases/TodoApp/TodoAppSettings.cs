// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

namespace Ninjadog.Templates.CrudWebAPI.UseCases.TodoApp;

public record TodoAppSettings : NinjadogSettings
{
    public override NinjadogConfiguration Config { get; init; } = new TodoAppConfiguration();
    public override NinjadogEntities Entities { get; init; } = new TodoAppEntities();
}
