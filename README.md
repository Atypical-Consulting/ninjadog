# Ninjadog

The .NET REST API Source Generator

Ninjadog is a .NET REST API source generator that aims to save developers' time by automating the creation of boilerplate code. It follows the Domain Driven Design (DDD) approach and generates REST endpoints, API clients in C# and TypeScript, and much more. With Ninjadog, developers can focus on what's really important: the business logic.

<!-- TOC -->
* [Ninjadog](#ninjadog)
  * [The long road to MVP](#the-long-road-to-mvp)
  * [How to start](#how-to-start)
  * [The Goal](#the-goal)
  * [Features](#features)
  * [Benefits](#benefits)
  * [List of generators](#list-of-generators)
    * [Ninjadog](#ninjadog-1)
    * [Ninjadog.Contracts.Data](#ninjadogcontractsdata)
    * [Ninjadog.Contracts.Requests](#ninjadogcontractsrequests)
    * [Ninjadog.Contracts.Responses](#ninjadogcontractsresponses)
    * [Ninjadog.Database](#ninjadogdatabase)
    * [Ninjadog.Endpoints](#ninjadogendpoints)
    * [Ninjadog.Mapping](#ninjadogmapping)
    * [Ninjadog.Repositories](#ninjadogrepositories)
    * [Ninjadog.Services](#ninjadogservices)
    * [Ninjadog.Summaries](#ninjadogsummaries)
    * [Ninjadog.Validation](#ninjadogvalidation)
<!-- TOC -->

## The long road to MVP
- [x] Solution that compiles
- [ ] Branding
    - [x] Name
    - [ ] Logo
    - [ ] TagLine
- [ ] Benefits of the solution
- [ ] Target audience
- [ ] Write documentation
- [ ] A client demo

## How to start
- Create a .NET8 Web API empty project
- Install the NuGet package Ninjadog
- Create your entities following the DDD pattern
- Add an attribute above your entities
- Compile :-)

## The Goal

Allow every .NET developer to save their time for what is really important: **the business and not the plumbing code**.

## Features

- Generates REST endpoints based on a DDD approach.
    - Create
    - Read All
    - Read One
    - Update
    - Delete
- Generates API clients in C# and TypeScript.
    - C# client accessible via the endpoint: `/cs-client`
    - TypeScript client accessible via the endpoint: `/ts-client`

## Benefits
- A LOT of time saved
- LESS code to maintain
- LESS error
- FINISHED the "plumbing code"
- REDUCES the duration of the production launch
- Also generates API clients in C# and TypeScript

## List of generators

### Ninjadog
- [x] [Ninjadog](./doc/generators/NinjadogGenerator.md) | Single File

### Ninjadog.Contracts.Data
- [x] [DtoGenerator](./doc/generators/DtoGenerator.md) | By Model

### Ninjadog.Contracts.Requests
- [x] [CreateRequestGenerator](./doc/generators/CreateRequestGenerator.md) | By Model
- [x] [DeleteRequestGenerator](./doc/generators/DeleteRequestGenerator.md) | By Model
- [x] [GetRequestGenerator](./doc/generators/GetRequestGenerator.md) | By Model
- [x] [UpdateRequestGenerator](./doc/generators/UpdateRequestGenerator.md) | By Model

### Ninjadog.Contracts.Responses
- [x] [GetAllResponseGenerator](./doc/generators/GetAllResponseGenerator.md) | By Model
- [x] [ResponseGenerator](./doc/generators/ResponseGenerator.md) | By Model

### Ninjadog.Database
- [x] [DatabaseInitializerGenerator](./doc/generators/DatabaseInitializerGenerator.md) | Single File
- [x] [DbConnectionFactoryGenerator](./doc/generators/DbConnectionFactoryGenerator.md) | Single File

### Ninjadog.Endpoints
- [x] [CreateEndpointGenerator](./doc/generators/CreateEndpointGenerator.md) | By Model
- [x] [DeleteEndpointGenerator](./doc/generators/DeleteEndpointGenerator.md) | By Model
- [x] [GetAllEndpointGenerator](./doc/generators/GetAllEndpointGenerator.md) | By Model
- [x] [GetEndpointGenerator](./doc/generators/GetEndpointGenerator.md) | By Model
- [x] [UpdateEndpointGenerator](./doc/generators/UpdateEndpointGenerator.md) | By Model

### Ninjadog.Mapping
- [x] [ApiContractToDomainMapperGenerator](./doc/generators/ApiContractToDomainMapperGenerator.md) | Single File
- [x] [DomainToApiContractMapperGenerator](./doc/generators/DomainToApiContractMapperGenerator.md) | Single File
- [x] [DomainToDtoMapperGenerator](./doc/generators/DomainToDtoMapperGenerator.md) | Single File
- [x] [DtoToDomainMapperGenerator](./doc/generators/DtoToDomainMapperGenerator.md) | Single File

### Ninjadog.Repositories
- [x] [RepositoryGenerator](./doc/generators/RepositoryGenerator.md) | By Model
- [x] [RepositoryInterfaceGenerator](./doc/generators/RepositoryInterfaceGenerator.md) | By Model

### Ninjadog.Services
- [x] [ServiceGenerator](./doc/generators/ServiceGenerator.md) | By Model
- [x] [ServiceInterfaceGenerator](./doc/generators/ServiceInterfaceGenerator.md) | By Model

### Ninjadog.Summaries
- [x] [CreateSummaryGenerator](./doc/generators/CreateSummaryGenerator.md) | By Model
- [x] [DeleteSummaryGenerator](./doc/generators/DeleteSummaryGenerator.md) | By Model
- [x] [GetAllSummaryGenerator](./doc/generators/GetAllSummaryGenerator.md) | By Model
- [x] [GetSummaryGenerator](./doc/generators/GetSummaryGenerator.md) | By Model
- [x] [UpdateSummaryGenerator](./doc/generators/UpdateSummaryGenerator.md) | By Model

### Ninjadog.Validation
- [x] [CreateRequestValidatorGenerator](./doc/generators/CreateRequestValidatorGenerator.md) | By Model
- [x] [UpdateRequestValidatorGenerator](./doc/generators/UpdateRequestValidatorGenerator.md) | By Model
