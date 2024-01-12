// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Templates.CrudWebAPI.Template.Contracts.Data;
using Ninjadog.Templates.CrudWebAPI.Template.Contracts.Requests;
using Ninjadog.Templates.CrudWebAPI.Template.Contracts.Responses;

namespace Ninjadog.Templates.CrudWebAPI.Setup;

/// <summary>
/// Represents a specific collection of templates for CRUD operations in a Web API.
/// This class initializes various types of request, response, and data transfer object (DTO) templates.
/// </summary>
public class CrudTemplateFiles : NinjadogTemplateFiles
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CrudTemplateFiles"/> class,
    /// adding templates for different CRUD operations.
    /// </summary>
    public CrudTemplateFiles()
    {
        AddTemplates(
            "Contracts/Data",
            new DtoTemplate());

        AddTemplates(
            "Contracts/Requests",
            new CreateRequestTemplate(),
            new DeleteRequestTemplate(),
            new GetRequestTemplate(),
            new UpdateRequestTemplate());

        AddTemplates(
            "Contracts/Responses",
            new GetAllResponseTemplate(),
            new ResponseTemplate());

        // AddTemplates(
        //     "Database",
        //     new DatabaseInitializerTemplate(),
        //     new DbConnectionFactoryTemplate());

        // AddTemplates(
        //     "Endpoints",
        //     new CreateEndpointTemplate(),
        //     new DeleteEndpointTemplate(),
        //     new GetAllEndpointTemplate(),
        //     new GetEndpointTemplate(),
        //     new UpdateEndpointTemplate());

        // AddTemplates(
        //     "Mapping",
        //     new ApiContractToDomainMapperTemplate(),
        //     new DomainToApiContractMapperTemplate(),
        //     new DomainToDtoMapperTemplate(),
        //     new DtoToDomainMapperTemplate());

        // AddTemplates(
        //     "Repositories",
        //     new RepositoryTemplate(),
        //     new RepositoryInterfaceTemplate());

        // AddTemplates(
        //     "Services",
        //     new ServiceTemplate(),
        //     new ServiceInterfaceTemplate());

        // AddTemplates(
        //     "Summaries",
        //     new CreateSummaryTemplate(),
        //     new DeleteSummaryTemplate(),
        //     new GetAllSummaryTemplate(),
        //     new GetSummaryTemplate(),
        //     new UpdateSummaryTemplate());

        // AddTemplates(
        //     "Validation",
        //     new CreateRequestValidatorTemplate(),
        //     new UpdateRequestValidatorTemplate());
    }
}
