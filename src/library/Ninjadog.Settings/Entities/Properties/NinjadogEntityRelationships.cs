// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Settings.Entities.Properties;

/// <summary>
/// Represents a collection of relationships for a Ninjadog entity.
/// This class extends a dictionary to map entity names to their corresponding Ninjadog entity relationships.
/// It's used to define the relationships between entities.
/// </summary>
public class NinjadogEntityRelationships
    : Dictionary<string, NinjadogEntityRelationship>;
