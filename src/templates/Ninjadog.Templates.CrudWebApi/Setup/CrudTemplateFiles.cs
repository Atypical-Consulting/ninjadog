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
        // Contracts / Data
        Add(new DtoTemplate());

        // Contracts / Requests
        Add(new CreateRequestTemplate());
        Add(new DeleteRequestTemplate());
        Add(new GetRequestTemplate());
        Add(new UpdateRequestTemplate());

        // Contracts / Responses
        // Add(new GetAllResponseTemplate());
        // Add(new ResponseTemplate());

        // Database
        // Add(new DatabaseInitializerTemplate());
        // Add(new DbConnectionFactoryTemplate());

        // Endpoints
        // Add(new CreateEndpointTemplate());
        // Add(new DeleteEndpointTemplate());
        // Add(new GetAllEndpointTemplate());
        // Add(new GetEndpointTemplate());
        // Add(new UpdateEndpointTemplate());

        // Mapping
        // Add(new ApiContractToDomainMapperTemplate());
        // Add(new DomainToApiContractMapperTemplate());
        // Add(new DomainToDtoMapperTemplate());
        // Add(new DtoToDomainMapperTemplate());

        // Repositories
        // Add(new RepositoryTemplate());
        // Add(new RepositoryInterfaceTemplate());

        // Services
        // Add(new ServiceTemplate());
        // Add(new ServiceInterfaceTemplate());

        // Summaries
        // Add(new CreateSummaryTemplate());
        // Add(new DeleteSummaryTemplate());
        // Add(new GetAllSummaryTemplate());
        // Add(new GetSummaryTemplate());
        // Add(new UpdateSummaryTemplate());

        // Validation
        // Add(new CreateRequestValidatorTemplate());
        // Add(new UpdateRequestValidatorTemplate());
    }
}
