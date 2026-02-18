// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

using Octokit;

namespace Ninjadog.SaaS.Infrastructure.Ports;

/// <summary>
/// Service for interacting with GitHub repositories.
/// </summary>
public interface IGitHubRepositoryService
{
    /// <summary>
    /// Get all repositories for the current authenticated user.
    /// </summary>
    /// <returns>A collection of repositories.</returns>
    Task<IEnumerable<Repository>> GetRepositoriesAsync();
}
