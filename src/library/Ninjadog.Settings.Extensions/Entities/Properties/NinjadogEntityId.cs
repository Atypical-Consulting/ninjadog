// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Settings.Extensions.Entities.Properties;

/// <summary>
/// Represents a special entity property that is designated as an identifier (ID).
/// This record extends <see cref="NinjadogEntityProperty"/>, marking the property as a key by default
/// and setting its type to <see cref="Guid"/>, commonly used for unique identifiers.
/// </summary>
public record NinjadogEntityId()
    : NinjadogEntityProperty<Guid>(true);
