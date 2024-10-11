using System.Reflection;
using Drammer.Common.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace Drammer.Common.Tests.Mapping;

public sealed class MappingExtensionsTests
{
    [Fact]
    public void AddMappers_WhenCalled_AddsMappers()
    {
        // arrange
        var serviceCollection = new ServiceCollection();
        var assembly = Assembly.GetAssembly(typeof(MappingExtensionsTests))!;

        // act
        serviceCollection.AddMappers([assembly]);

        // assert
        serviceCollection.Should().NotBeEmpty();
        serviceCollection.Should().ContainSingle(x => x.ServiceType == typeof(IMapping<SourceModel, DestinationModel>));
    }

    [Fact]
    public void AddMappers_WhenCalled_AddsMappersWithLifetime()
    {
        // arrange
        var serviceCollection = new ServiceCollection();
        var assembly = Assembly.GetAssembly(typeof(MappingExtensionsTests))!;

        // act
        serviceCollection.AddMappers([assembly]);

        // assert
        serviceCollection.Should().NotBeEmpty();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var scope = serviceProvider.CreateScope();

        var singletonMapping1 = scope.ServiceProvider.GetRequiredService<IMapping<TestModel1, TestModel1>>();
        var singletonMapping2 = scope.ServiceProvider.GetRequiredService<IMapping<TestModel1, TestModel1>>();
        singletonMapping1.Should().Be(singletonMapping2);

        var transientMapping1 = scope.ServiceProvider.GetRequiredService<IMapping<SourceModel, DestinationModel>>();
        var transientMapping2 = scope.ServiceProvider.GetRequiredService<IMapping<SourceModel, DestinationModel>>();
        transientMapping1.Should().NotBe(transientMapping2);
    }

    public sealed class TestModel1
    {
        public string? Id { get; set; }
    }

    [MappingLifetime(ServiceLifetime.Singleton)]
    public sealed class SingletonMapping : IMapping<TestModel1, TestModel1>
    {
        public TestModel1? Map(TestModel1? source)
        {
            if (source == null)
            {
                return null;
            }

            return new TestModel1
            {
                Id = source.Id
            };
        }
    }
}