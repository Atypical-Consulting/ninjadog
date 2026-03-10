// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Templates.CrudWebAPI.UseCases.TodoApp;

/// <summary>
/// Provides the specific configuration for the "TodoApp" application.
/// This sealed record inherits from NinjadogConfiguration and sets predefined values
/// tailored for the TodoApp project, such as its name, version, description, and paths.
/// It's an example of how specific project configurations can be defined using the Ninjadog framework.
/// </summary>
public sealed record TodoAppConfiguration()
    : NinjadogConfiguration(
        Name: "TodoApp",
        Version: "1.0.0",
        Description: "A application to manage todo lists.",
        RootNamespace: "TodoApp.CrudWebApi",
        OutputPath: "src/applications/TodoApp");
