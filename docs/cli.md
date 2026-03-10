---
title: CLI
layout: default
nav_order: 4
---

# CLI Reference

The `ninjadog` CLI is distributed as a .NET global tool.

## Installation

```bash
dotnet tool install -g Ninjadog.CLI
```

## Commands

### `ninjadog init`

Initializes a new Ninjadog project in the current directory.

```bash
ninjadog init
ninjadog init --name MyApp --output ./my-app
```

| Option | Description |
|---|---|
| `--name` | Project name |
| `--output` | Output directory |

### `ninjadog build`

Builds and runs the generator engine against your project configuration.

```bash
ninjadog build
ninjadog build --input ./ninjadog.json
```

| Option | Description |
|---|---|
| `--input` | Path to ninjadog.json configuration file |

### `ninjadog ninjadog`

Generates a new Ninjadog project with full scaffolding.

```bash
ninjadog ninjadog
```

## Uninstall

```bash
dotnet tool uninstall -g Ninjadog.CLI
```
