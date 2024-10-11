using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace Drammer.Common.Mapping;

/// <summary>
/// The mapper class.
/// </summary>
public class Mapper : IMapper
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="Mapper"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    public Mapper(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    /// <inheritdoc/>
    [return: NotNullIfNotNull(nameof(source))]
    public TDestination? Map<TSource, TDestination>(TSource? source)
        where TSource : class
        where TDestination : class
    {
        var service = GetMapping<TSource, TDestination>();
        if (service == null)
        {
            throw new InvalidOperationException(
                $"No mapping exists for source: `{typeof(TSource).Name}` -> dest: `{typeof(TDestination).Name}`");
        }

        return service.Map(source);
    }

    /// <inheritdoc/>
    public IMapping<TSource, TDestination>? GetMapping<TSource, TDestination>()
        where TSource : class
        where TDestination : class
    {
        return _serviceProvider.GetService<IMapping<TSource, TDestination>>();
    }

    /// <inheritdoc/>
    public IMapping<TSource, TDestination> GetRequiredMapping<TSource, TDestination>()
        where TSource : class
        where TDestination : class
    {
        return _serviceProvider.GetRequiredService<IMapping<TSource, TDestination>>();
    }
}