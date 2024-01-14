// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Settings.Entities.Properties;

namespace Ninjadog.Settings.Extensions;

/// <summary>
/// Represents a special entity property that is designated as an identifier (ID).
/// This record extends <see cref="NinjadogEntityProperty"/>, marking the property as a key by default
/// and setting its type to <see cref="Guid"/>, commonly used for unique identifiers.
/// </summary>
public record NinjadogEntityId()
    : NinjadogEntityProperty(nameof(Guid), true);
