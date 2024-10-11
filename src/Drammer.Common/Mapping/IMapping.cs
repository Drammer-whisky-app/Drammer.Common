using System.Diagnostics.CodeAnalysis;

namespace Drammer.Common.Mapping;

/// <summary>
/// The mapping interface.
/// </summary>
/// <typeparam name="TSource">The source class.</typeparam>
/// <typeparam name="TDestination">The destination class.</typeparam>
public interface IMapping<in TSource, out TDestination>
    where TDestination : class
    where TSource : class
{
    /// <summary>
    /// A mapping function which converts the source to the destination.
    /// </summary>
    /// <param name="source">The source object.</param>
    /// <returns>The destination object.</returns>
    [return: NotNullIfNotNull(nameof(source))]
    TDestination? Map(TSource? source);
}