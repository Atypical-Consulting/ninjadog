---
title: Core
layout: default
parent: Generators
nav_order: 1
---

# Core Generator

## NinjadogGenerator

| | |
|---|---|
| **Scope** | Single File |
| **Namespace** | Ninjadog |

The root generator that orchestrates the entire code generation pipeline. It discovers all classes annotated with `[Ninjadog]` and delegates to the appropriate category-specific generators.

### What It Does

1. Scans the compilation for all classes with the `[Ninjadog]` attribute
2. Extracts metadata for each entity: class name, namespace, properties, key type
3. Invokes each category generator (endpoints, contracts, data layer, etc.) with the collected metadata
4. Produces the dependency injection registration code that wires all generated services into the DI container

{: .note }
> You never call `NinjadogGenerator` directly -- it runs automatically as part of `dotnet build` via Roslyn's source generator infrastructure.

### Generated Registration

The core generator also produces service registration extension methods so all generated repositories, services, and validators are automatically registered in your DI container.
