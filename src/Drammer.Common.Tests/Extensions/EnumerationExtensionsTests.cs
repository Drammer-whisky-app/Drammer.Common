using System.Collections.ObjectModel;
using Drammer.Common.Extensions;
using FluentAssertions;

namespace Drammer.Common.Tests.Extensions;

public sealed class EnumerationExtensionsTests
{
    [Fact]
    public void IsNullOrEmpty_WhenCollectionIsNull_ReturnsTrue()
    {
        // arrange
        Collection<int>? collection = null;

        // act
        var result = collection.IsNullOrEmpty();

        // assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsNullOrEmpty_WhenCollectionIsEmpty_ReturnsTrue()
    {
        // arrange
        var collection = new Collection<int>();

        // act
        var result = collection.IsNullOrEmpty();

        // assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsNullOrEmpty_WhenCollectionIsNotEmpty_ReturnsFalse()
    {
        // arrange
        var collection = new Collection<int> {1};

        // act
        var result = collection.IsNullOrEmpty();

        // assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsNullOrEmpty_WhenEnumerableIsNull_ReturnsTrue()
    {
        // arrange
        IEnumerable<int>? enumerable = null;

        // act
        var result = enumerable.IsNullOrEmpty();

        // assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsNullOrEmpty_WhenEnumerableIsEmpty_ReturnsTrue()
    {
        // arrange
        var enumerable = Enumerable.Empty<int>();

        // act
        var result = enumerable.IsNullOrEmpty();

        // assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsNullOrEmpty_WhenEnumerableIsNotEmpty_ReturnsFalse()
    {
        // arrange
        var enumerable = Enumerable.Range(1, 1);

        // act
        var result = enumerable.IsNullOrEmpty();

        // assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsNullOrEmpty_WhenCollectionAsEnumerableIsNull_ReturnsTrue()
    {
        // arrange
        Collection<int>? collection = null;
        var enumerable = collection!.AsEnumerable();

        // act
        var result = enumerable.IsNullOrEmpty();

        // assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsNullOrEmpty_WhenCollectionAsEnumerableIsEmpty_ReturnsTrue()
    {
        // arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        var collection = new Collection<int>();
        var enumerable = collection.AsEnumerable();

        // act
        var result = enumerable.IsNullOrEmpty();

        // assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsNullOrEmpty_WhenCollectionAsEnumerableIsNotEmpty_ReturnsFalse()
    {
        // arrange
        var collection = new Collection<int> {1};
        var enumerable = collection.AsEnumerable();

        // act
        var result = enumerable.IsNullOrEmpty();

        // assert
        result.Should().BeFalse();
    }
}