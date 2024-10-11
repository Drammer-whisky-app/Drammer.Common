using Microsoft.Extensions.DependencyInjection;

namespace Drammer.Common.Mapping;

[AttributeUsage(AttributeTargets.Class)]
public sealed class MappingLifetimeAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingLifetimeAttribute"/> class.
    /// </summary>
    /// <param name="lifetime">The lifetime.</param>
    public MappingLifetimeAttribute(ServiceLifetime lifetime)
    {
        Lifetime = lifetime;
    }

    /// <summary>
    /// Gets the lifetime.
    /// </summary>
    public ServiceLifetime Lifetime { get; }
}