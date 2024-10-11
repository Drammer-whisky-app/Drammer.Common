namespace Drammer.Common.Paging;

[Serializable]
public class PagedList<T> : List<T>, IPagedList<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
    /// </summary>
    public PagedList()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
    /// </summary>
    /// <param name="pageIndex">
    /// The page index.
    /// </param>
    /// <param name="pageSize">
    /// The page size.
    /// </param>
    public PagedList(int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
    /// </summary>
    /// <param name="pageIndex">
    /// The page index.
    /// </param>
    /// <param name="pageSize">
    /// The page size.
    /// </param>
    /// <param name="entities">
    /// The entities for this page.
    /// </param>
    public PagedList(int pageIndex, int pageSize, IEnumerable<T> entities)
        : this(pageIndex, pageSize)
    {
        AddRange(entities);
    }

    /// <summary>
    /// Gets or sets the page index.
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    /// Gets or sets the page size.
    /// </summary>
    public int PageSize { get; set; }
}