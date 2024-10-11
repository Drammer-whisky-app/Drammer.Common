using System.Diagnostics.CodeAnalysis;

namespace Drammer.Common.Mapping;

/// <summary>
/// The IMapper interface.
/// </summary>
public interface IMapper
{
    /// <summary>
    /// Maps a non-nullable object.
    /// </summary>
    /// <param name="source">The source object.</param>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <returns>A <see cref="TDestination"/> object.</returns>
    [return: NotNullIfNotNull(nameof(source))]
    TDestination? Map<TSource, TDestination>(TSource? source)
        where TDestination : class
        where TSource : class;

    /// <summary>
    /// Gets a mapping.
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <returns>An <see cref="IMapping{TSource,TDestination}"/>.</returns>
    IMapping<TSource, TDestination>? GetMapping<TSource, TDestination>()
        where TDestination : class
        where TSource : class;

    /// <summary>
    /// Gets a required mapping. Throws an exception when the mapping is not found.
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <exception cref="InvalidOperationException">Thrown when the mapping is not found.</exception>
    /// <returns>An <see cref="IMapping{TSource,TDestination}"/>.</returns>
    IMapping<TSource, TDestination> GetRequiredMapping<TSource, TDestination>()
        where TDestination : class
        where TSource : class;
}