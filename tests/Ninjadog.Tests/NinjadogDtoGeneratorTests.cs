// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Microsoft.CodeAnalysis.CSharp;
using Ninjadog.Tests.Utils;

namespace Ninjadog.Tests;

public class NinjadogDtoGeneratorTests
{
    private const string NinjadogSettingsText =
        """
        {
          "config": {
            "name": "TodoApp",
            "version": "1.0.0",
            "description": "A simple todo app",
            "rootNamespace": "MyCustomer.TodoApp"
          },
          "entities": {
            "TodoList": {
              "properties": {
                "Id": {
                  "type": "Guid",
                  "isKey": true
                },
                "Title": {
                  "type": "string"
                },
                "Items": {
                  "type": "List<TodoItem>"
                }
              }
            },
            "TodoItem": {
              "properties": {
                "Id": {
                  "type": "Guid",
                  "isKey": true
                },
                "Description": {
                  "type": "string"
                },
                "IsCompleted": {
                  "type": "bool"
                }
              }
            }
          }
        }
        """;

    private static readonly string[] ExpectedFiles =
    [
        "TodoList.g.cs",
        "TodoItem.g.cs"
    ];

    [Fact]
    public void GenerateClassesBasedOnNinjadogSettings()
    {
        // Create an instance of the source generator.
        var generator = new NinjadogDtoGenerator();

        // Source generators should be tested using 'GeneratorDriver'.
        var driver = CSharpGeneratorDriver.Create(new[] { generator },
            new[]
            {
                // Add the additional file separately from the compilation.
                new TestAdditionalFile("./ninjadogsettings.json", NinjadogSettingsText)
            });

        // To run generators, we can use an empty compilation.
        var compilation = CSharpCompilation.Create(nameof(NinjadogDtoGeneratorTests));

        // Run generators. Don't forget to use the new compilation rather than the previous one.
        driver.RunGeneratorsAndUpdateCompilation(compilation, out var newCompilation, out _);

        // Retrieve all files in the compilation.
        var generatedFiles = newCompilation.SyntaxTrees
            .Select(t => Path.GetFileName(t.FilePath))
            .ToArray();

        // In this case, it is enough to check the file name.
        Assert.Equivalent(ExpectedFiles, generatedFiles);
        // TODO: Replace with FluentAssertions
        // generatedFiles.Should().HaveCount(2);
        // generatedFiles.Should().BeEquivalentTo(Expected);
    }
}
