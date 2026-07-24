FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Ninjadog.CLI/Ninjadog.CLI.csproj", "src/Ninjadog.CLI/"]
RUN dotnet restore "src/Ninjadog.CLI/Ninjadog.CLI.csproj"
COPY . .
WORKDIR "/src/src/Ninjadog.CLI"
RUN dotnet build "Ninjadog.CLI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Ninjadog.CLI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ninjadog.CLI.dll"]
