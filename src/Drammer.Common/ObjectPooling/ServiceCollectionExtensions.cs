using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;

namespace Drammer.Common.ObjectPooling;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds an object pool of string builders to the service collection.
    /// </summary>
    /// <param name="serviceCollection">The service collection.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddStringBuilderObjectPool(this IServiceCollection serviceCollection)
    {
        const string ObjectPoolProviderKey = $"{nameof(Drammer)}.{nameof(ObjectPoolProvider)}";

        serviceCollection.AddKeyedSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>(ObjectPoolProviderKey);
        serviceCollection.AddSingleton<ObjectPool<StringBuilder>>(sp =>
        {
            var provider = sp.GetRequiredKeyedService<ObjectPoolProvider>(ObjectPoolProviderKey);
            return provider.Create(new DefaultPooledObjectPolicy<StringBuilder>());
        });

        return serviceCollection;
    }
}