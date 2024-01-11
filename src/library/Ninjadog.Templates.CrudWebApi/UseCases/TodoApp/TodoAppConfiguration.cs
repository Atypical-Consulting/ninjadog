// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

namespace Ninjadog.Templates.CrudWebAPI.UseCases.TodoApp;

public record TodoAppConfiguration()
    : NinjadogConfiguration(
        "TodoApp",
        "1.0.0",
        "A application to manage todo lists.",
        "MyCompany.TodoApp");
