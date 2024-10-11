namespace Drammer.Common.Paging;

[Serializable]
public class ExtendedPagedList<T> : PagedList<T>, IExtendedPagedList<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExtendedPagedList{T}"/> class.
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
    /// <param name="totalRecords">
    /// The total records.
    /// </param>
    public ExtendedPagedList(int pageIndex, int pageSize, IEnumerable<T> entities, long totalRecords)
        : base(pageIndex, pageSize, entities)
    {
        TotalRecords = totalRecords;
    }

    /// <inheritdoc/>
    public int TotalPages => (int)Math.Ceiling(TotalRecords / (double)PageSize);

    /// <inheritdoc/>
    public long TotalRecords { get;  }
}