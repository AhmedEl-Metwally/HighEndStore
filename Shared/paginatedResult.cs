
namespace Shared
{
    public record PaginatedResult<TData> (int pageIndex, int pageSize, int TotalCount, IEnumerable<TData> Data )
    {

    }
}
