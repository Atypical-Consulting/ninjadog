// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Settings.Entities;

/// <summary>
/// Represents a collection of Ninjadog entities.
/// This abstract class serves as a dictionary mapping entity names to their corresponding Ninjadog entity definitions.
/// It can be used to define and manage the various entities involved in the templating and code generation process.
/// </summary>
public abstract class NinjadogEntities
    : Dictionary<string, NinjadogEntity>;
