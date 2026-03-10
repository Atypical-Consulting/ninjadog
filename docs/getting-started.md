---
title: Getting Started
layout: default
nav_order: 2
---

# Getting Started
{: .no_toc }

Get a full CRUD REST API running in under 2 minutes.
{: .fs-6 .fw-300 }

<details open markdown="block">
  <summary>Table of contents</summary>
  {: .text-delta }
1. TOC
{:toc}
</details>

---

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later

{: .tip }
> Verify your SDK version with `dotnet --version`. You need **10.0** or higher.

## Installation

Choose one of the following installation methods:

### Option 1 -- NuGet Package (recommended)

Add Ninjadog directly to your project:

```bash
dotnet add package Ninjadog
```

### Option 2 -- Global CLI tool

Install the CLI for project scaffolding:

```bash
dotnet tool install -g Ninjadog.CLI
```

### Option 3 -- From Source

```bash
git clone https://github.com/Atypical-Consulting/Ninjadog.git
cd ninjadog
dotnet build
```

## Your First API

Follow these steps to create a fully functional REST API from a single entity class.

### Step 1 -- Create a new project

```bash
dotnet new web -n MyApi
cd MyApi
dotnet add package Ninjadog
```

### Step 2 -- Define your domain entity

Open `Program.cs` (or create a new file) and add:

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

The `[Ninjadog]` attribute tells the source generator to produce the full API stack for this entity.

### Step 3 -- Build and run

```bash
dotnet build
dotnet run
```

{: .note }
> During the build, Ninjadog generates ~30 files including endpoints, DTOs, validators, repositories, services, mappers, and OpenAPI documentation. These files are generated at compile time and do not appear on disk.

### Step 4 -- Verify it works

Open your browser or use `curl` to test the endpoints:

```bash
# Create a product
curl -X POST http://localhost:5000/products \
  -H "Content-Type: application/json" \
  -d '{"name": "Widget", "price": 9.99}'

# List all products (paginated)
curl http://localhost:5000/products

# Get a single product
curl http://localhost:5000/products/{id}

# Update a product
curl -X PUT http://localhost:5000/products/{id} \
  -H "Content-Type: application/json" \
  -d '{"name": "Updated Widget", "price": 12.99}'

# Delete a product
curl -X DELETE http://localhost:5000/products/{id}
```

You should see JSON responses for each operation. The GetAll endpoint returns paginated results with `totalCount` metadata.

## Multiple Entities

Each entity gets its own isolated set of generated files. Add as many entities as you need:

```csharp
[Ninjadog]
public class Movie
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
}

[Ninjadog]
public class Order
{
    public int OrderId { get; set; }    // int key -- routes use :int constraint
    public string CustomerName { get; set; }
    public decimal Total { get; set; }
}
```

{: .tip }
> Route constraints are dynamic -- `:guid`, `:int`, or untyped -- based on your entity's key type. An `int` key produces `/orders/{id:int}`, while a `Guid` key produces `/movies/{id:guid}`.

## What Happens Under the Hood

When you build, Ninjadog's source generators scan your code for `[Ninjadog]`-annotated classes and produce:

| What | Example for `Product` |
|---|---|
| **Endpoints** | `CreateProductEndpoint`, `GetAllProductsEndpoint`, etc. |
| **Contracts** | `CreateProductRequest`, `ProductResponse`, `ProductDto` |
| **Validation** | `CreateProductRequestValidator` with `.NotEmpty()` for strings |
| **Data Layer** | `IProductRepository`, `ProductRepository`, `IProductService`, `ProductService` |
| **Mapping** | Extension methods like `.ToProductResponse()`, `.ToProductDto()` |
| **Database** | `DatabaseInitializer` with SQLite `CREATE TABLE` for all entities |
| **OpenAPI** | Swagger summaries for every endpoint |

---

## Next Steps

- [Architecture](/Ninjadog/architecture) -- Understand the design decisions and tech stack
- [CLI Reference](/Ninjadog/cli) -- Scaffold projects with the CLI tool
- [Generators](/Ninjadog/generators) -- Deep dive into all 30 generators
- [Generated Examples](/Ninjadog/examples) -- See real generated code output
