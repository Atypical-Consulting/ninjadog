// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SourceGenerator;

internal sealed record StringTokens
{
    public StringTokens(string modelNameInPascalCase)
    {
        var sv = new StringVariations(modelNameInPascalCase);

        // model
        Model = sv.Pascal;
        ModelHumanized = sv.Humanized;
        ModelEndpoint = sv.DashedPlural;
        VarModel = sv.Camel;
        VarExistingModel = $"existing{sv.Pascal}";

        // models
        Models = sv.PascalPlural;
        ModelsHumanized = sv.HumanizedPlural;
        VarModels = sv.CamelPlural;

        // dto
        ClassModelDto = $"{sv.Pascal}Dto";
        VarModelDto = $"{sv.Camel}Dto";
        VarModelDtos = $"{sv.Camel}Dtos";

        // model response
        ClassModelResponse = $"{sv.Pascal}Response";
        VarModelResponse = $"{sv.Camel}Response";

        // models response
        ClassGetAllModelsResponse = $"GetAll{sv.PascalPlural}Response";
        VarModelsResponse = $"{sv.CamelPlural}Response";

        // repository
        InterfaceModelRepository = $"I{sv.Pascal}Repository";
        ClassModelRepository = $"{sv.Pascal}Repository";
        FieldModelRepository = $"_{sv.Camel}Repository";
        VarModelRepository = $"{sv.Camel}Repository";

        // service
        InterfaceModelService = $"I{sv.Pascal}Service";
        ClassModelService = $"{sv.Pascal}Service";
        FieldModelService = $"_{sv.Camel}Service";
        VarModelService = $"{sv.Camel}Service";

        // get all endpoint
        ClassGetAllModelsSummary = $"GetAll{sv.PascalPlural}Summary";
        ClassGetAllModelsEndpoint = $"GetAll{sv.PascalPlural}Endpoint";

        // get endpoint
        ClassGetModelSummary = $"Get{sv.Pascal}Summary";
        ClassGetModelRequest = $"Get{sv.Pascal}Request";
        ClassGetModelEndpoint = $"Get{sv.Pascal}Endpoint";

        // delete endpoint
        ClassDeleteModelSummary = $"Delete{sv.Pascal}Summary";
        ClassDeleteModelRequest = $"Delete{sv.Pascal}Request";
        ClassDeleteModelEndpoint = $"Delete{sv.Pascal}Endpoint";

        // create endpoint
        ClassCreateModelSummary = $"Create{sv.Pascal}Summary";
        ClassCreateModelRequest = $"Create{sv.Pascal}Request";
        ClassCreateModelRequestValidator = $"Create{sv.Pascal}RequestValidator";
        ClassCreateModelEndpoint = $"Create{sv.Pascal}Endpoint";

        // update endpoint
        ClassUpdateModelSummary = $"Update{sv.Pascal}Summary";
        ClassUpdateModelRequest = $"Update{sv.Pascal}Request";
        ClassUpdateModelRequestValidator = $"Update{sv.Pascal}RequestValidator";
        ClassUpdateModelEndpoint = $"Update{sv.Pascal}Endpoint";

        // methods
        MethodToModel = $"To{sv.Pascal}";
        MethodToModelDto = $"To{sv.Pascal}Dto";
        MethodToModelResponse = $"To{sv.Pascal}Response";
        MethodToModelsResponse = $"To{sv.PascalPlural}Response";
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
