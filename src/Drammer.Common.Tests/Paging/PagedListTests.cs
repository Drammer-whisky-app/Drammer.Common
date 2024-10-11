using Drammer.Common.Paging;

namespace Drammer.Common.Tests.Paging;

public sealed class PagedListTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void Constructor_DefaultConstructor_SetsProperties()
    {
        // act
        var list = new PagedList<int>();

        // assert
        list.PageIndex.Should().Be(0);
        list.PageSize.Should().Be(0);
    }

    [Fact]
    public void Constructor_WithPageIndexAndSize_SetsProperties()
    {
        // arrange
        var pageIndex = _fixture.Create<int>();
        var pageSize = _fixture.Create<int>();

        // act
        var list = new PagedList<int>(pageIndex, pageSize);

        // assert
        list.PageIndex.Should().Be(pageIndex);
        list.PageSize.Should().Be(pageSize);
    }

    [Fact]
    public void Constructor_WithEnumerable_SetsProperties()
    {
        // arrange
        var pageIndex = _fixture.Create<int>();
        var pageSize = _fixture.Create<int>();
        var items = Enumerable.Range(1, 10).ToList();

        // act
        var list = new PagedList<int>(pageIndex, pageSize, items);

        // assert
        list.PageIndex.Should().Be(pageIndex);
        list.PageSize.Should().Be(pageSize);
        list.Should().BeEquivalentTo(items);
    }
}