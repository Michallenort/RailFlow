namespace Railflow.Core.Pagination;

public class PagedList<T>
{
    public IEnumerable<T> Items { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    
    private PagedList(IEnumerable<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
    


    public static PagedList<T> Create(IEnumerable<T> query, int page, int pageSize)
    {
        var queryListed = query.ToList();
        var totalCount = queryListed.Count();
        var items = queryListed.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return new(items, page, pageSize, totalCount);
    }
}