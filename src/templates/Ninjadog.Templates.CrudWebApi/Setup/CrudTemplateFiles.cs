// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Templates.CrudWebAPI.Template.Contracts.Data;
using Ninjadog.Templates.CrudWebAPI.Template.Contracts.Requests;

namespace Ninjadog.Templates.CrudWebAPI.Setup;

// this class is used to define the template files that will be used by the template engine

// we could use a template with different destination paths.

public class CrudTemplateFiles : NinjadogTemplateFiles
{
    public CrudTemplateFiles()
    {
        Add(new DtoTemplate());
        Add(new CreateRequestTemplate());
    }
}
