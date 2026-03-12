namespace Ninjadog.Templates.CrudWebAPI.Template.Docker;

/// <summary>
/// This template generates a Dockerfile for the Web API project.
/// </summary>
public class DockerfileTemplate : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "Dockerfile";

    /// <inheritdoc />
    public override NinjadogContentFile GenerateOne(NinjadogSettings ninjadogSettings)
    {
        var name = ninjadogSettings.Config.Name;
        var rootNamespace = ninjadogSettings.Config.RootNamespace;

        var content =
            $$"""
              # Stage 1: Build
              FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
              WORKDIR /src

              COPY ["{{name}}.csproj", "."]
              RUN dotnet restore

              COPY . .
              RUN dotnet publish -c Release -o /app/publish

              # Stage 2: Runtime
              FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
              WORKDIR /app

              EXPOSE 8080

              COPY --from=build /app/publish .

              ENTRYPOINT ["dotnet", "{{rootNamespace}}.dll"]
              """;

        return CreateNinjadogContentFile("Dockerfile", content, false);
    }
}
