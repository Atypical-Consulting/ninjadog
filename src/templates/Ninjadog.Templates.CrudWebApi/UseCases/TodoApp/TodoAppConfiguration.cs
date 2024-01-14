// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Settings.Config;

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
        RootNamespace: "MyCompany.TodoApp",
        OutputPath: "src/applications/TodoApp");
