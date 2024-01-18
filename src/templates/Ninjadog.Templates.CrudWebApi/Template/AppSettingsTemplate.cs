// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Templates.CrudWebAPI.Template;

/// <summary>
/// This template generates the appsettings.json file for the Template project.
/// </summary>
public class AppSettingsTemplate : NinjadogTemplate
{
    /// <inheritdoc />
    public override string Name => "appsettings";

    /// <inheritdoc />
    public override NinjadogContentFile GenerateOne(NinjadogSettings ninjadogSettings)
    {
        const string fileName = "appsettings.json";

        const string content =
            """
            {
              "Database": {
                "ConnectionString": "Data Source=./customer.db"
              },
              "Logging": {
                "LogLevel": {
                  "Default": "Information",
                  "Microsoft.AspNetCore": "Warning"
                }
              },
              "AllowedHosts": "*"
            }
            """;

        return CreateNinjadogContentFile(fileName, content, false);
    }
}
