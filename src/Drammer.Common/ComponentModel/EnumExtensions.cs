using System.Resources;

namespace Drammer.Common.ComponentModel;

public static class EnumExtensions
{
    public static string DisplayName(this Enum value, bool localize = true)
    {
        // Get the type
        var type = value.GetType();

        // Get fieldinfo for this type
        var fieldInfo = type.GetField(value.ToString());

        // Get the stringvalue attributes
        var attribs = fieldInfo!.GetCustomAttributes(
            typeof(LocalizedDisplayNameAttribute), false) as LocalizedDisplayNameAttribute[];

        // Return the first if there was a match.
        var result = value.ToString();
        if (attribs is {Length: > 0})
        {
            var attrib = attribs[0];
            result = attrib.DisplayName;

            if (localize && attrib.NameResourceType != null)
            {
                var manager = new ResourceManager(attrib.NameResourceType);
                result = manager.GetString(attrib.DisplayName);
            }
        }

        return !string.IsNullOrEmpty(result) ? result : value.ToString();
    }
}