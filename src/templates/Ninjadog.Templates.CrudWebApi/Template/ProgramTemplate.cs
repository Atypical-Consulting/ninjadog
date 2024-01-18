// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Templates.CrudWebAPI.Template;

/// <summary>
/// This template generates the Program.cs file for the Template project.
/// </summary>
public class ProgramTemplate : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "Program";

    /// <inheritdoc />
    public override NinjadogContentFile GenerateOne(NinjadogSettings ninjadogSettings)
    {
        var rootNamespace = ninjadogSettings.Config.RootNamespace;
        const string fileName = "Program.cs";

        return CreateNinjadogContentFile(fileName,
            $$"""

              using {{rootNamespace}};
              using {{rootNamespace}}.Database;

              const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

              var builder = WebApplication.CreateBuilder(args);
              var services = builder.Services;
              var config = builder.Configuration;

              services.AddCors(options =>
              {
                  options.AddPolicy(name: myAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7270");
                      });
              });

              services.AddNinjadog(config);

              var app = builder.Build();

              app.UseCors(myAllowSpecificOrigins);
              app.UseNinjadog();

              await app.Services
                  .GetRequiredService<DatabaseInitializer>()
                  .InitializeAsync()
                  .ConfigureAwait(false);

              app.Run();
              """);
    }
}
