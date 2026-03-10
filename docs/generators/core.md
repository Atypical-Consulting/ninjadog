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
