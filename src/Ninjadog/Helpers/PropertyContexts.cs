namespace Ninjadog.Helpers;

public sealed class PropertyContexts : List<PropertyContext>
{
    public PropertyContexts(ITypeSymbol type)
    {
        var propertySymbols = GetPropertiesWithGetSet(type).ToArray();

        for (int i = 0; i < propertySymbols.Length; i++)
        {
            var property = propertySymbols[i];

            var isId = property.Name == "Id";
            var isLast = i == propertySymbols.Length - 1;
            var name = property.Name;

            Add(new PropertyContext(isId, isLast, name));
        }
    }
}
