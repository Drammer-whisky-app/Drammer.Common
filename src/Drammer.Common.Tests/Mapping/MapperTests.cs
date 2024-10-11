using Drammer.Common.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace Drammer.Common.Tests.Mapping;

public sealed class MapperTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void Map_MappingNotFound_ThrowException()
    {
        // arrange
        var serviceCollection = new ServiceCollection();
        var mapper = CreateMapper(serviceCollection);
        var source = _fixture.Create<SourceModel>();

        // act
        var action = () => mapper.Map<SourceModel, DestinationModel>(source);

        // assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Map_GivenNonNullableModel_ReturnsModel()
    {
        // arrange
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IMapping<SourceModel, DestinationModel>, TestModelMapping>();
        var mapper = CreateMapper(serviceCollection);
        var source = _fixture.Create<SourceModel>();

        // act
        var result = mapper.Map<SourceModel, DestinationModel>(source);

        // assert
        result.Should().NotBeNull();
        result.Name.Should().Be(source.Name);

    }

    [Fact]
    public void Map_GivenNull_ReturnsNull()
    {
        // arrange
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IMapping<SourceModel, DestinationModel>, TestModelMapping>();
        var mapper = CreateMapper(serviceCollection);
        var source = _fixture.Create<SourceModel>();

        // act
        var result = mapper.Map<SourceModel, DestinationModel>(null);

        // assert
        result.Should().BeNull();

    }

    [Fact]
    public void GetMapping_MappingNotExists_ReturnsNull()
    {
        // arrange
        var serviceCollection = new ServiceCollection();
        var mapper = CreateMapper(serviceCollection);

        // act
        var result = mapper.GetMapping<SourceModel, DestinationModel>();

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public void GetMapping_MappingExists_ReturnsMapping()
    {
        // arrange
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IMapping<SourceModel, DestinationModel>, TestModelMapping>();
        var mapper = CreateMapper(serviceCollection);

        // act
        var result = mapper.GetMapping<SourceModel, DestinationModel>();

        // assert
        result.Should().NotBeNull();
        result.Should().BeOfType<TestModelMapping>();
    }

    [Fact]
    public void GetRequiredMapping_MappingNotExists_ThrowException()
    {
        // arrange
        var serviceCollection = new ServiceCollection();
        var mapper = CreateMapper(serviceCollection);

        // act
        var action = () => mapper.GetRequiredMapping<SourceModel, DestinationModel>();

        // assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GetRequiredMapping_MappingExists_ReturnsMapping()
    {
        // arrange
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IMapping<SourceModel, DestinationModel>, TestModelMapping>();
        var mapper = CreateMapper(serviceCollection);

        // act
        var result = mapper.GetRequiredMapping<SourceModel, DestinationModel>();

        // assert
        result.Should().NotBeNull();
        result.Should().BeOfType<TestModelMapping>();
    }

    private static Mapper CreateMapper(ServiceCollection? serviceCollection = null)
    {
        var serviceProvider = (serviceCollection ?? new ServiceCollection()).BuildServiceProvider();
        return new Mapper(serviceProvider);
    }
}