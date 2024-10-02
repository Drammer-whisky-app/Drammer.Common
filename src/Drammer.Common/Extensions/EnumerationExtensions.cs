using System.Diagnostics.CodeAnalysis;

namespace Drammer.Common.Extensions;

public static class EnumerationExtensions
{
    /// <summary>
    /// Returns true if the collection is null or empty.
    /// </summary>
    /// <param name="collection">
    /// The collection.
    /// </param>
    /// <typeparam name="T">
    /// The collection type.
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this ICollection<T>? collection)
    {
        return collection == null || collection.Count == 0;
    }
    
    /// <summary>
    /// Determines whether the collection is null or contains no elements.
    /// </summary>
    /// <typeparam name="T">The IEnumerable type.</typeparam>
    /// <param name="enumerable">The enumerable, which may be null or empty.</param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T>? enumerable)
    {
        if (enumerable == null)
        {
            return true;
        }
        
        return enumerable is ICollection<T> collection ? collection.Count == 0 : !enumerable.Any();
    }
}