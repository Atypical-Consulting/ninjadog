# Ninjadog

The .NET REST API Source Generator

<!-- TOC -->
* [Ninjadog](#ninjadog)
  * [The long road to MVP](#the-long-road-to-mvp)
  * [How to start](#how-to-start)
  * [The Goal](#the-goal)
  * [Fonctionalites](#fonctionalites)
  * [Avantages](#avantages)
  * [Liste des generateurs](#liste-des-generateurs)
    * [Ninjadog](#ninjadog)
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
  * [Diagrams](#diagrams)
    * [Request/Response Flow](#requestresponse-flow)
<!-- TOC -->

## The long road to MVP
- [x] Solution qui compile
- [ ] BMC
- [ ] SWOT
- [ ] Plan financier
- [ ] Tarification
- [ ] Branding
  - [x] Nom
  - [ ] Logo
  - [ ] TagLine
  - [ ] Fichiers de presentations (Word, PowerPoint...)
- [ ] Avantages de la solution
- [ ] Audience cible
- [ ] Ecrire une documentation
- [ ] Site web Shopify avec vente de produit dematerialise
- [ ] Une demo client
- [ ] Une demo pour les institutions financieres (Noshak...)

## How to start
- Creation d'un projet .NET6 Web API vide
- Installez le package NuGet Ninjadog
- Creez vos entites en suivant le pattern DDD
- Ajoutez un attribut au dessus de vos entites
- Compilez :-)

## The Goal

Permettre a chaque developpeur .NET de garder son temps pour ce qui est vraiment important: **le business et non pas le code plomberie**.

## Fonctionalites

- Genere les endpoints REST en se basant sur une approche DDD (Domain Driven Design).
  - Create
  - Read All
  - Read One
  - Update
  - Delete
- Genere les clients d'API en C# et TypeScript.
  - Client C# accessible via le endpoint: `/cs-client`
  - Client TypeScript accessible via le endpoint: `/ts-client`

## Avantages
- BEAUCOUP de temps gagne
- MOINS de code a maintenir
- MOINS d'erreur
- FINI le "code plomberie"
- REDUIT la duree de la mise en production
- Genere egalement les clients d'API en C# et TypeScript

## Liste des generateurs

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
