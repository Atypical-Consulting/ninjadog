// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Settings.Entities;

/// <summary>
/// Represents a collection of Ninjadog entities.
/// This abstract class serves as a dictionary mapping entity names to their corresponding Ninjadog entity definitions.
/// It can be used to define and manage the various entities involved in the templating and code generation process.
/// </summary>
public abstract class NinjadogEntities
    : Dictionary<string, NinjadogEntity>;
