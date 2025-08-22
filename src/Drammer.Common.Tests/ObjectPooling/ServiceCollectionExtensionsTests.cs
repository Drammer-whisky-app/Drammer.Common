using System.Text;
using Drammer.Common.ObjectPooling;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;

namespace Drammer.Common.Tests.ObjectPooling;

public sealed class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddStringBuilderObjectPool_ShouldRegisterServices()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();

        // Act
        var existingServiceCollection = serviceCollection.AddStringBuilderObjectPool();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Assert
        existingServiceCollection.Should().BeSameAs(serviceCollection);
        serviceProvider.GetService<ObjectPool<StringBuilder>>().Should().NotBeNull();
        serviceProvider.GetKeyedService<ObjectPoolProvider>("Drammer.ObjectPoolProvider").Should().BeOfType<DefaultObjectPoolProvider>();
    }
}