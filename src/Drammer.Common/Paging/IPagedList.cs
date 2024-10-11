namespace Drammer.Common.Paging;

public interface IPagedList<T> : IList<T>
{
    /// <summary>
    /// Gets or sets the page index.
    /// </summary>
    int PageIndex { get; set; }

    /// <summary>
    /// Gets or sets the page size.
    /// </summary>
    int PageSize { get; set; }
}