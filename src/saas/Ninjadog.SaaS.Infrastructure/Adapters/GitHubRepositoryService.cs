// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

using Ninjadog.SaaS.Infrastructure.Ports;
using Octokit;

namespace Ninjadog.SaaS.Infrastructure.Adapters;

/// <summary>
/// Service for interacting with GitHub repositories.
/// </summary>
public class GitHubRepositoryService : IGitHubRepositoryService
{
    private const string ClientId = "7c7e7fd61758ad76d995";
    private const string ClientSecret = "6c4a50631214df5a650efa91324697550b602133";

    private readonly GitHubClient _client = new(new ProductHeaderValue("Ninjadog-SaaS"));

    /// <inheritdoc />
    public async Task<IEnumerable<Repository>> GetRepositoriesAsync()
    {
        var csrf = Guid.NewGuid().ToString("N");

        var request = new OauthLoginRequest(ClientId)
        {
            Scopes = { "repo", "user", "notifications" },
            State = csrf
        };

        // NOTE: user must be navigated to this URL
        var oauthLoginUrl = _client.Oauth.GetGitHubLoginUrl(request);

        var repositories = await _client.Repository.GetAllForCurrent();
        return repositories;
    }
}
