---
title: Data Layer Generators
description: "Ninjadog data layer generators: multi-provider database initializer (SQLite, PostgreSQL, SQL Server), Dapper repositories, service layer, and connection factory with automatic type mapping."
layout: default
parent: Generators
nav_order: 4
---

# Data Layer Generators

## Database Provider Configuration

Ninjadog supports multiple database providers. Configure the provider in your `ninjadog.json`:

```json
{
  "config": {
    "database": {
      "provider": "postgresql"
    }
  }
}
```

{: .note }
> Supported values: `sqlite` (default), `postgresql`, `sqlserver`. When no provider is specified, Ninjadog defaults to SQLite.

The chosen provider affects generated code across the data layer -- connection factories, SQL dialect, type mappings, and NuGet package references are all adapted automatically.

## Database

### DatabaseInitializerGenerator

| Scope | Single File |
|---|---|

Generates a `DatabaseInitializer` class that creates tables for all entities. Column types are mapped automatically based on the configured database provider:

| C# Type | SQLite | PostgreSQL | SQL Server |
|---|---|---|---|
| `string` | TEXT | TEXT | NVARCHAR(MAX) |
| `int` | INTEGER | INTEGER | INT |
| `bool` | INTEGER | BOOLEAN | BIT |
| `decimal` | REAL | NUMERIC | DECIMAL(18,2) |
| `DateTime` | TEXT | TIMESTAMP | DATETIME2 |
| `DateOnly` | TEXT | DATE | DATE |
| `Guid` | CHAR(36) | UUID | UNIQUEIDENTIFIER |
| `enum` | INTEGER | INTEGER | INT |

{: .tip }
> The generator also adapts SQL syntax per provider. For example, PostgreSQL and SQL Server use provider-specific `NOW()` functions and pagination syntax (`LIMIT`/`OFFSET` vs `OFFSET`/`FETCH NEXT`).

### DbConnectionFactoryGenerator

| Scope | Single File |
|---|---|

Generates an `IDbConnectionFactory` interface and a provider-specific implementation for creating database connections. The generated factory class depends on the configured provider:

| Provider | Generated Factory | Connection Class |
|---|---|---|
| `sqlite` | `SqliteConnectionFactory` | `SqliteConnection` |
| `postgresql` | `NpgsqlConnectionFactory` | `NpgsqlConnection` |
| `sqlserver` | `SqlServerConnectionFactory` | `SqlConnection` |

{: .note }
> When using `postgresql` or `sqlserver`, the corresponding NuGet packages (`Npgsql` or `Microsoft.Data.SqlClient`) are automatically added to the generated project manifest.

## Repositories

### RepositoryGenerator

| Scope | Per Entity |
|---|---|

Generates a concrete repository with Dapper-based CRUD operations (`GetAllAsync`, `GetAsync`, `CreateAsync`, `UpdateAsync`, `DeleteAsync`).

### RepositoryInterfaceGenerator

| Scope | Per Entity |
|---|---|

Generates the repository interface for dependency injection.

## Services

### ServiceGenerator

| Scope | Per Entity |
|---|---|

Generates a service layer that delegates to the repository, providing a clean abstraction for endpoints.

### ServiceInterfaceGenerator

| Scope | Per Entity |
|---|---|

Generates the service interface for dependency injection.
