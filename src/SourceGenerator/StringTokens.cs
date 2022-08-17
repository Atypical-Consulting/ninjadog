// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SourceGenerator;

internal sealed record StringTokens
{
    public StringTokens(string modelNameInPascalCase)
    {
        var sv = new StringVariations(modelNameInPascalCase);

        ClassModel = sv.Pascal;
        VarModel = sv.Camel;
        VarModels = sv.CamelPlural;
        VarExistingModel = $"existing{sv.Pascal}";

        ClassModelDto = $"{sv.Pascal}Dto";
        VarModelDto = $"{sv.Camel}Dto";
        VarModelDtos = $"{sv.Camel}Dtos";

        ClassModelResponse = $"{sv.Pascal}Response";
        VarModelResponse = $"{sv.Camel}Response";

        ClassGetAllModelsResponse = $"GetAll{sv.PascalPlural}Response";
        VarModelsResponse = $"{sv.CamelPlural}Response";

        ClassModelRepository = $"{sv.Pascal}Repository";
        InterfaceModelRepository = $"I{sv.Pascal}Repository";
        FieldModelRepository = $"_{sv.Camel}Repository";
        VarModelRepository = $"{sv.Camel}Repository";

        ClassModelService = $"{sv.Pascal}Service";
        InterfaceModelService = $"I{sv.Pascal}Service";
        FieldModelService = $"_{sv.Camel}Service";
        VarModelService = $"{sv.Camel}Service";

        ClassGetAllModelsSummary = $"GetAll{sv.PascalPlural}Summary";
        ClassGetAllModelsEndpoint = $"GetAll{sv.PascalPlural}Endpoint";

        ClassGetModelRequest = $"Get{sv.Pascal}Request";
        ClassGetModelSummary = $"Get{sv.Pascal}Summary";
        ClassGetModelEndpoint = $"Get{sv.Pascal}Endpoint";

        ClassDeleteModelSummary = $"Delete{sv.Pascal}Summary";
        ClassDeleteModelRequest = $"Delete{sv.Pascal}Request";
        ClassDeleteModelEndpoint = $"Delete{sv.Pascal}Endpoint";

        ClassCreateModelSummary = $"Create{sv.Pascal}Summary";
        ClassCreateModelRequest = $"Create{sv.Pascal}Request";
        ClassCreateModelRequestValidator = $"Create{sv.Pascal}RequestValidator";
        ClassCreateModelEndpoint = $"Create{sv.Pascal}Endpoint";

        ClassUpdateModelSummary = $"Update{sv.Pascal}Summary";
        ClassUpdateModelRequest = $"Update{sv.Pascal}Request";
        ClassUpdateModelRequestValidator = $"Update{sv.Pascal}RequestValidator";
        ClassUpdateModelEndpoint = $"Update{sv.Pascal}Endpoint";

        MethodToModel = $"To{sv.Pascal}";
        MethodToModelDto = $"To{sv.Pascal}Dto";
        MethodToModelResponse = $"To{sv.Pascal}Response";
        MethodToModelsResponse = $"To{sv.PascalPlural}Response";

        EndpointModels = sv.DashedPlural;
        ModelHumanized = sv.Humanized;
        Models = sv.PascalPlural;
    }

    public string ClassUpdateModelRequestValidator { get; set; }

    public string ClassCreateModelRequestValidator { get; set; }

    public string MethodToModelDto { get; set; }

    public string VarModelDtos { get; set; }

    public string VarModelDto { get; set; }

    public string FieldModelRepository { get; set; }

    public string VarModelRepository { get; set; }

    public string ClassModel { get; set; }

    public string VarExistingModel { get; set; }

    public string ClassUpdateModelEndpoint { get; set; }

    public string VarModelsResponse { get; set; }

    public string VarModels { get; set; }

    public string ClassGetAllModelsEndpoint { get; set; }

    public string MethodToModelsResponse { get; set; }

    public string ClassGetModelEndpoint { get; set; }

    public string MethodToModelResponse { get; set; }

    public string VarModelService { get; set; }

    public string FieldModelService { get; set; }

    public string InterfaceModelService { get; set; }

    public string InterfaceModelRepository { get; set; }

    public string ClassModelService { get; set; }

    public string ClassModelRepository { get; set; }

    public string VarModelResponse { get; set; }

    public string VarModel { get; set; }

    public string MethodToModel { get; set; }

    public string ClassCreateModelEndpoint { get; }

    public string ClassModelDto { get; }

    public string ClassCreateModelRequest { get; }

    public string ClassDeleteModelRequest { get; }

    public string ClassUpdateModelRequest { get; }

    public string ClassModelResponse { get; }

    public string ClassGetAllModelsResponse { get; }

    public string ClassGetModelRequest { get; }

    public string ClassCreateModelSummary { get; }

    public string ClassDeleteModelSummary { get; }

    public string ClassGetAllModelsSummary { get; }

    public string ClassGetModelSummary { get; }

    public string ClassUpdateModelSummary { get; }

    public string ClassDeleteModelEndpoint { get; }

    public string EndpointModels { get; }

    public string ModelHumanized { get; }

    public string Models { get; }
}
