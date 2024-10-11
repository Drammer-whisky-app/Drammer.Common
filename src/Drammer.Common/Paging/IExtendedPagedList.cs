namespace Drammer.Common.Paging;

public interface IExtendedPagedList<T> : IPagedList<T>
{
    /// <summary>
    /// Gets the total pages.
    /// </summary>
    int TotalPages { get; }

    /// <summary>
    /// Gets the total records.
    /// </summary>
    long TotalRecords { get; }
}