// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using Ninjadog.Settings.Entities.Properties;

namespace Ninjadog.Settings.Entities;

/// <summary>
/// Represents a Ninjadog entity definition.
/// This record holds the properties of an entity, which can include fields, data types, and key information.
/// It is used to define the structure and characteristics of an entity within the Ninjadog framework.
/// </summary>
/// <param name="Properties">The properties of the entity, including their types and key designation.</param>
public record NinjadogEntity(
    NinjadogEntityProperties Properties);
