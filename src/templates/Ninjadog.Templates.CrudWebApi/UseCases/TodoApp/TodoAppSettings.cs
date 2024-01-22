// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Templates.CrudWebAPI.UseCases.TodoApp;

/// <summary>
/// Represents the settings for the TodoApp template.
/// </summary>
public record TodoAppSettings()
    : NinjadogSettings(
        new TodoAppConfiguration(),
        new TodoAppEntities());
