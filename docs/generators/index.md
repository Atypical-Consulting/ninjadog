---
title: Generators
layout: default
nav_order: 5
has_children: true
---

# Generators

Ninjadog includes **30 generators** organized into 11 categories. Each generator produces either a single shared file or a per-entity file.

| Category | Generators | Scope |
|---|---|---|
| **Core** | NinjadogGenerator | Single file |
| **Contracts -- Data** | DtoGenerator | Per entity |
| **Contracts -- Requests** | Create, Delete, Get, Update | Per entity |
| **Contracts -- Responses** | GetAllResponse, Response | Per entity |
| **Database** | DatabaseInitializer, DbConnectionFactory | Single file |
| **Endpoints** | Create, Delete, GetAll, Get, Update | Per entity |
| **Mapping** | ApiContract-to-Domain, Domain-to-ApiContract, Domain-to-Dto, Dto-to-Domain | Single file |
| **Repositories** | Repository, RepositoryInterface | Per entity |
| **Services** | Service, ServiceInterface | Per entity |
| **Summaries** | Create, Delete, GetAll, Get, Update | Per entity |
| **Validation** | CreateRequestValidator, UpdateRequestValidator | Per entity |

## Generation Modes

- **Single File** -- One file generated regardless of how many entities exist (e.g., database initializer creates all tables)
- **Per Entity (By Model)** -- One file per `[Ninjadog]`-annotated entity (e.g., each entity gets its own repository)
