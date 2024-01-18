// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Engine.Infrastructure.Services;

public static class InfrastructureConstants
{
    public const Environment.SpecialFolder UserProfile = Environment.SpecialFolder.UserProfile;
    public const string NinjadogProjects = "NinjadogProjects";
    public const string NinjadogSettingsFile = "ninjadog.json";
    public static string BaseFolder { get; } = Path.Combine(Environment.GetFolderPath(UserProfile), NinjadogProjects);
}
