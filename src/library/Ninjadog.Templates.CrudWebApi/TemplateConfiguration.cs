// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

namespace Ninjadog.Templates.CrudWebAPI;

public record TemplateConfiguration()
    : NinjadogConfiguration(
        "CrudWebAPI",
        "1.0.0-alpha.1",
        "Your ASP.NET Core Web API project with CRUD operations",
        "MyCompany.TodoApp");
