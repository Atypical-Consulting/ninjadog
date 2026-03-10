---
title: Architecture
layout: default
nav_order: 3
---

# Architecture

## How It Works

```mermaid
flowchart TB
    A["Your C# Source<br/>[Ninjadog] Domain Entities"] --> B["Roslyn Compiler<br/>Ninjadog Source Generators"]
    B --> C["Contracts<br/>Requests, Responses"]
    B --> D["Data Layer<br/>DTOs, Mappers"]
    B --> E["Clients<br/>C#, TypeScript"]
    C --> F["Endpoints<br/>CRUD"]
    D --> G["Repos<br/>Services"]
    E --> H["Validators<br/>OpenAPI"]
    F --> I["Compiled Assembly<br/>Full REST API"]
    G --> I
    H --> I
```

## Key Design Decisions

- **Compile-time generation** -- No runtime reflection, no startup penalty
- **Source Generators (not T4/CLI)** -- Integrated into the standard build pipeline
- **Per-entity isolation** -- Each entity gets independent files; no cross-entity coupling
- **Convention over configuration** -- Sensible defaults for routes, validation, and database schema
- **Type-aware output** -- Route constraints, SQL column types, and validation rules adapt to property types

## Tech Stack

| Layer | Technology |
|---|---|
| Runtime | .NET 10, C# 13 |
| Code Generation | Roslyn Source Generators |
| API Framework | FastEndpoints |
| Database | SQLite + Dapper |
| Validation | FluentValidation |
| OpenAPI | FastEndpoints.Swagger |
| Client Generation | FastEndpoints.ClientGen |
| Architecture | Domain-Driven Design (DDD) |
| CLI | Spectre.Console |

## Project Structure

```
ninjadog/
├── src/
│   ├── library/                             # Core generator libraries
│   │   ├── Ninjadog.Engine/                 # Main source generator engine
│   │   ├── Ninjadog.Engine.Core/            # Core generator abstractions
│   │   ├── Ninjadog.Engine.Infrastructure/  # Infrastructure utilities
│   │   ├── Ninjadog.Helpers/                # Shared helper functions
│   │   ├── Ninjadog.Settings/               # Generator configuration
│   │   └── Ninjadog.Settings.Extensions/    # Settings extension methods
│   ├── tools/
│   │   └── Ninjadog.CLI/                    # Command-line interface
│   ├── templates/
│   │   └── Ninjadog.Templates.CrudWebApi/   # CRUD Web API template
│   └── tests/
│       └── Ninjadog.Tests/                  # Snapshot + unit tests
├── docs/                                    # GitHub Pages documentation
├── doc/                                     # Generator reference docs
├── Ninjadog.sln                             # Solution file
└── global.json                              # .NET SDK version config
```

## NuGet Packages

| Package | Description |
|---|---|
| `Ninjadog.Engine` | Main source generator engine |
| `Ninjadog.Engine.Core` | Core generator abstractions |
| `Ninjadog.Engine.Infrastructure` | Infrastructure utilities |
| `Ninjadog.Helpers` | Shared helper functions |
| `Ninjadog.Settings` | Generator configuration |
| `Ninjadog.Settings.Extensions` | Settings extension methods |
| `Ninjadog.Templates.CrudWebApi` | CRUD Web API template |
| `Ninjadog.CLI` | Command-line tool (`dotnet tool install -g Ninjadog.CLI`) |
