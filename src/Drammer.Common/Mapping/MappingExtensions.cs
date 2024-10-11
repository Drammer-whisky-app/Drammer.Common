using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Drammer.Common.Mapping;

public static class MappingExtensions
{
    /// <summary>
    /// Add mapping classes to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="assemblies">The assemblies to look in.</param>
    /// <param name="lifetime">The default lifetime when no <see cref="MappingLifetimeAttribute"/> is found.</param>
    /// <returns></returns>
    public static IServiceCollection AddMappers(
        this IServiceCollection services,
        IEnumerable<Assembly> assemblies,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        var types = assemblies.SelectMany(x => x.GetTypes()).Where(
            x => !x.IsInterface && !x.IsAbstract && x.GetInterfaces().Any(
                     y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IMapping<,>)));

        foreach (var type in types)
        {
            var l = lifetime;
            var customAttributes = type.GetCustomAttributes(typeof(MappingLifetimeAttribute)).ToList();

            if (customAttributes.Count != 0)
            {
                l = (customAttributes.First() as MappingLifetimeAttribute)?.Lifetime ?? l;
            }

            var interfaces = type.GetInterfaces().Where(
                x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IMapping<,>)).ToList();

            // more than one interface in this type, register only one implementation type
            // this works only for singletons
            if (l == ServiceLifetime.Singleton && interfaces.Count > 1)
            {
                services.Add(new ServiceDescriptor(type, type, l));

                foreach (var i in interfaces)
                {
                    services.Add(new ServiceDescriptor(i, x => x.GetRequiredService(type), l));
                }
            }
            else
            {
                foreach (var i in interfaces)
                {
                    services.Add(new ServiceDescriptor(i, type, l));
                }
            }
        }

        return services;
    }
}