// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Settings.Entities.Properties;

/// <summary>
/// Represents a property of a Ninjadog entity.
/// This record holds information about the type of the property and whether it is a key.
/// It is used to define the individual attributes of an entity, such as data type and key status.
/// </summary>
/// <param name="Type">The data type of the property.</param>
/// <param name="IsKey">Indicates whether the property is a key field. Defaults to false.</param>
public record NinjadogEntityProperty(
    string Type,
    bool IsKey = false);
