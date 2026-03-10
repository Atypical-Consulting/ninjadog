---
title: Contributing
layout: default
nav_order: 7
---

# Contributing

Contributions are welcome!

## Getting Started

```bash
git clone https://github.com/Atypical-Consulting/Ninjadog.git
cd ninjadog
dotnet build
dotnet test
```

## Development Workflow

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Run the tests: `dotnet test`
4. Commit using [conventional commits](https://www.conventionalcommits.org/) (`git commit -m 'feat: add amazing feature'`)
5. Push to the branch (`git push origin feature/amazing-feature`)
6. Open a Pull Request

## Where to Look

| Area | Path |
|---|---|
| Generator templates | `src/library/Ninjadog.Engine/` |
| Snapshot tests | `src/tests/Ninjadog.Tests/Templates/` |
| CLI commands | `src/tools/Ninjadog.CLI/` |
| Generator docs | `doc/generators/` |

## License

This project is licensed under the Apache License 2.0 -- see the [LICENSE](https://github.com/Atypical-Consulting/Ninjadog/blob/dev/LICENSE) file for details.
