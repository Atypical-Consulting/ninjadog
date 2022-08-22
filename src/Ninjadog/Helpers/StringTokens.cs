// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ninjadog.Helpers;

internal sealed record StringTokens
{
    public StringTokens(string modelNameInPascalCase)
    {
        string pascal = modelNameInPascalCase;
        string pascalPlural = pascal.Pluralize();
        string camel = pascal.Camelize();
        string camelPlural = pascal.Camelize().Pluralize();
        string dashed = pascal.Underscore().Dasherize();
        string dashedPlural = pascal.Pluralize().Underscore().Dasherize();
        string humanized = pascal.Underscore().Humanize().ToLower();
        string humanizedPlural = pascal.Pluralize().Underscore().Humanize().ToLower();

        // model
        Model = pascal;
        ModelHumanized = humanized;
        ModelEndpoint = dashedPlural;
        VarModel = camel;
        VarExistingModel = $"existing{pascal}";

        // models
        Models = pascalPlural;
        ModelsHumanized = humanizedPlural;
        VarModels = camelPlural;

        // dto
        ClassModelDto = $"{pascal}Dto";
        VarModelDto = $"{camel}Dto";
        VarModelDtos = $"{camel}Dtos";

        // model response
        ClassModelResponse = $"{pascal}Response";
        VarModelResponse = $"{camel}Response";

        // models response
        ClassGetAllModelsResponse = $"GetAll{pascalPlural}Response";
        VarModelsResponse = $"{camelPlural}Response";

        // repository
        InterfaceModelRepository = $"I{pascal}Repository";
        ClassModelRepository = $"{pascal}Repository";
        FieldModelRepository = $"_{camel}Repository";
        VarModelRepository = $"{camel}Repository";

        // service
        InterfaceModelService = $"I{pascal}Service";
        ClassModelService = $"{pascal}Service";
        FieldModelService = $"_{camel}Service";
        VarModelService = $"{camel}Service";

        // get all endpoint
        ClassGetAllModelsSummary = $"GetAll{pascalPlural}Summary";
        ClassGetAllModelsEndpoint = $"GetAll{pascalPlural}Endpoint";

        // get endpoint
        ClassGetModelSummary = $"Get{pascal}Summary";
        ClassGetModelRequest = $"Get{pascal}Request";
        ClassGetModelEndpoint = $"Get{pascal}Endpoint";

        // delete endpoint
        ClassDeleteModelSummary = $"Delete{pascal}Summary";
        ClassDeleteModelRequest = $"Delete{pascal}Request";
        ClassDeleteModelEndpoint = $"Delete{pascal}Endpoint";

        // create endpoint
        ClassCreateModelSummary = $"Create{pascal}Summary";
        ClassCreateModelRequest = $"Create{pascal}Request";
        ClassCreateModelRequestValidator = $"Create{pascal}RequestValidator";
        ClassCreateModelEndpoint = $"Create{pascal}Endpoint";

        // update endpoint
        ClassUpdateModelSummary = $"Update{pascal}Summary";
        ClassUpdateModelRequest = $"Update{pascal}Request";
        ClassUpdateModelRequestValidator = $"Update{pascal}RequestValidator";
        ClassUpdateModelEndpoint = $"Update{pascal}Endpoint";

        // methods
        MethodToModel = $"To{pascal}";
        MethodToModelDto = $"To{pascal}Dto";
        MethodToModelResponse = $"To{pascal}Response";
        MethodToModelsResponse = $"To{pascalPlural}Response";
    }

    public string Model { get; }

    public string ModelHumanized { get; }

    public string ModelEndpoint { get; }

    public string VarModel { get; }

    public string VarExistingModel { get; }

    public string Models { get; }

    public string ModelsHumanized { get; }

    public string VarModels { get; }

    public string ClassModelDto { get; }

    public string VarModelDto { get; }

    public string VarModelDtos { get; }

    public string ClassModelResponse { get; }

    public string VarModelResponse { get; }

    public string ClassGetAllModelsResponse { get; }

    public string VarModelsResponse { get; }

    public string InterfaceModelRepository { get; }

    public string ClassModelRepository { get; }

    public string FieldModelRepository { get; }

    public string VarModelRepository { get; }

    public string InterfaceModelService { get; }

    public string ClassModelService { get; }

    public string FieldModelService { get; }

    public string VarModelService { get; }

    public string ClassGetAllModelsSummary { get; }

    public string ClassGetAllModelsEndpoint { get; }

    public string ClassGetModelSummary { get; }

    public string ClassGetModelRequest { get; }

    public string ClassGetModelEndpoint { get; }

    public string ClassDeleteModelSummary { get; }

    public string ClassDeleteModelRequest { get; }

    public string ClassDeleteModelEndpoint { get; }

    public string ClassCreateModelSummary { get; }

    public string ClassCreateModelRequest { get; }

    public string ClassCreateModelRequestValidator { get; }

    public string ClassCreateModelEndpoint { get; }

    public string ClassUpdateModelSummary { get; }

    public string ClassUpdateModelRequest { get; }

    public string ClassUpdateModelRequestValidator { get; }

    public string ClassUpdateModelEndpoint { get; }

    public string MethodToModel { get; }

    public string MethodToModelDto { get; }

    public string MethodToModelResponse { get; }

    public string MethodToModelsResponse { get; }
}
