using Drammer.Common.Paging;

namespace Drammer.Common.Tests.Paging;

public sealed class ExtendedPagedListTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void Constructor_WithEnumerable_SetsProperties()
    {
        // arrange
        var pageIndex = _fixture.Create<int>();
        var items = Enumerable.Range(1, 10).ToList();

        const int PageSize = 10;
        const int TotalRecords = 201;
        const int ExpectedTotalPages = 21;

        // act
        var list = new ExtendedPagedList<int>(pageIndex, PageSize, items, TotalRecords);

        // assert
        list.PageIndex.Should().Be(pageIndex);
        list.PageSize.Should().Be(PageSize);
        list.Should().BeEquivalentTo(items);
        list.TotalRecords.Should().Be(TotalRecords);
        list.TotalPages.Should().Be(ExpectedTotalPages);
    }
}