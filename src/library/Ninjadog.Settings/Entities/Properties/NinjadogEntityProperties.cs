// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Settings.Entities.Properties;

/// <summary>
/// Represents a collection of properties for a Ninjadog entity.
/// This class extends a dictionary to map property names to their corresponding Ninjadog entity properties.
/// It's used to define the attributes and characteristics of each property in an entity.
/// </summary>
public class NinjadogEntityProperties
    : Dictionary<string, NinjadogEntityProperty>;
