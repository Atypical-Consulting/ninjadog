// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

namespace Ninjadog.Helpers;

public sealed class PropertyContexts : List<PropertyContext>
{
    public PropertyContexts(ITypeSymbol type)
    {
        var propertySymbols = GetPropertiesWithGetSet(type).ToArray();

        for (var i = 0; i < propertySymbols.Length; i++)
        {
            var property = propertySymbols[i];

            var isId = property.Name == "Id";
            var isLast = i == propertySymbols.Length - 1;
            var name = property.Name;

            Add(new PropertyContext(isId, isLast, name));
        }
    }
}
