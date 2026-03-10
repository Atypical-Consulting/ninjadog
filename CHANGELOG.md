# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/), and this project adheres to [Semantic Versioning](https://semver.org/).

## [Unreleased]

### Added

- **Pagination** — `GetAll` endpoints support `?page=1&pageSize=10` query parameters, with `Page`, `PageSize`, and `TotalCount` metadata in the response.
- **CLI build command** — `ninjadog build` now executes the engine instead of throwing `NotImplementedException`.
- **Snapshot tests** — 14 Verify snapshot tests cover template output for databases, repositories, mappers, validators, and endpoints with varied entity types.

### Fixed

- **Type-aware code generation** — Mapper type strings now match `typeof(T).Name` output (`Guid`, `DateOnly` instead of `System.Guid`, `System.DateOnly`), fixing type-dependent code paths in all 4 mapper templates.
- **Dynamic entity keys** — Repositories, endpoints, and request contracts no longer hardcode `"Id"`. The primary key name and type are resolved from the entity definition, enabling entities with keys like `OrderId` (int) or `BookingRef` (string).
- **Type-aware database schema** — `DatabaseInitializer` generates correct SQLite column types per property (`INTEGER` for int/bool, `REAL` for decimal, `CHAR(36)` for Guid) instead of `TEXT` for everything.
- **Type-aware validation** — Validators skip `.NotEmpty()` for value types (`int`, `bool`, `decimal`) that always have a valid default value.
