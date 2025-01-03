namespace BuildingBlocks.Pagination;
public class PaginatedResult<TEntity>
    (int pageIndex, int pageSize, long count, long pageCount, IEnumerable<TEntity> data)
    where TEntity : class
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageSize;
    public long Count { get; } = count;
    public long PageCount { get; } = pageCount;
    public IEnumerable<TEntity> Data { get; } = data;
}
