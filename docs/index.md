---
title: Home
layout: home
nav_order: 1
---

<p align="center">
  <img src="{{ '/assets/images/logo.png' | relative_url }}" alt="Ninjadog logo" width="256" />
</p>

# Ninjadog
{: .text-center }

**One attribute. Full REST API. Zero boilerplate.**
{: .fs-6 .fw-300 .text-center }

Ninjadog uses C# Source Generators to produce your entire API stack at compile time. No runtime reflection, no code-gen CLI step, no files to keep in sync. Change your entity, rebuild, done.
{: .fs-5 .fw-300 }

[Get Started](/Ninjadog/getting-started){: .btn .btn-primary .fs-5 .mb-4 .mb-md-0 .mr-2 }
[View on GitHub](https://github.com/Atypical-Consulting/Ninjadog){: .btn .fs-5 .mb-4 .mb-md-0 }

---

## Why Ninjadog?

| | Without Ninjadog | With Ninjadog |
|---|---|---|
| **Code you write** | ~500+ lines per entity | ~10 lines per entity |
| **Files to maintain** | 20+ per entity | 1 per entity |
| **Layers in sync** | Manual | Automatic |
| **Runtime cost** | Depends on approach | Zero (compile-time) |
| **Reflection** | Often required | None |
| **Time to full CRUD** | Hours | Seconds |

## What Gets Generated

For **each** entity annotated with `[Ninjadog]`, the generator produces approximately **30 files**:

| Category | Generated Files | Description |
|---|---|---|
| **Endpoints** | 5 | Create, GetAll (paginated), GetOne, Update, Delete |
| **Contracts** | 7 | DTOs, request objects, response objects |
| **Data Layer** | 4 | Repository + interface, service + interface |
| **Mapping** | 4 | Domain-to-DTO, DTO-to-Domain, Domain-to-Contract, Contract-to-Domain |
| **Validation** | 2 | Create + Update request validators |
| **OpenAPI** | 5 | Summaries for each endpoint |
| **Database** | 2 | Initializer + connection factory |
| **Clients** | 2 | C# and TypeScript API clients |

## Quick Example

```csharp
using Ninjadog;

[Ninjadog]
public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

Build and run -- you now have a full CRUD API:

```
POST   /products              Create a new product
GET    /products              List all products (paginated)
GET    /products/{id:guid}    Get a single product
PUT    /products/{id:guid}    Update a product
DELETE /products/{id:guid}    Delete a product
```

{: .note }
> Route constraints are generated automatically based on your key type -- `:guid` for `Guid`, `:int` for `int`, and untyped for `string`.

---

[Get Started in 2 minutes](/Ninjadog/getting-started){: .btn .btn-primary .mr-2 }
[Explore the Architecture](/Ninjadog/architecture){: .btn .mr-2 }
[Browse all Generators](/Ninjadog/generators){: .btn }
