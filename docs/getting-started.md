---
title: Getting Started
layout: default
nav_order: 2
---

# Getting Started

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later

## Installation

### Option 1 -- Global CLI tool

```bash
dotnet tool install -g Ninjadog.CLI
```

### Option 2 -- NuGet Package (library use)

```bash
dotnet add package Ninjadog
```

### Option 3 -- From Source

```bash
git clone https://github.com/Atypical-Consulting/Ninjadog.git
cd ninjadog
dotnet build
```

## Your First API

1. Create a new .NET 10 Web API project:

```bash
dotnet new web -n MyApi
cd MyApi
dotnet add package Ninjadog
```

2. Define your domain entity with the `[Ninjadog]` attribute:

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

3. Build and run:

```bash
dotnet build
dotnet run
```

Your API is now live with full CRUD endpoints for `Product`.

## Multiple Entities

Each entity gets its own isolated set of generated files:

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

Route constraints are dynamic -- `:guid`, `:int`, or untyped -- based on your entity's key type.
