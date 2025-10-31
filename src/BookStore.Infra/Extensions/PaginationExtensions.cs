using BookStore.Domain.Persistence.Responses;
using Gridify;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace BookStore.Infra.Extensions
{
    internal static class PaginationExtensions
    {
        internal static async Task<PagedResult<T>> ToPagedResultAsync<T>(
            this IQueryable<T> source,
            int page,
            int pageSize,
            CancellationToken ct = default)
        {
            int count = await source.CountAsync(ct);
            List<T> items = await source
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return new PagedResult<T>(page, pageSize, count, items);
        }

        internal static async Task<PagedResult<T>> ToPagedResultAsync<T>(
            this IQueryable<T> source,
            IGridifyPagination paginationRequest,
            CancellationToken ct = default)
        {
            IFutureValue<int> futureCount = source.ToFutureValue(q => q.Count());
            IFutureEnumerable<T> futureItems = source.ApplyPaging(paginationRequest).ToFuture();

            int count = await futureCount.GetValueAsync(ct);
            IEnumerable<T> items = await futureItems.GetEnumerableAsync(ct);

            return new PagedResult<T>(paginationRequest.Page, paginationRequest.PageSize, count, items);
        }

        internal static async Task<PagedResult<T>> ToPagedResultAsync<T>(
            this IQueryOver<T, T> source,
            IGridifyPagination paginationRequest,
            CancellationToken ct = default)
        {
            int count = await source
                .ToRowCountQuery()
                .SingleOrDefaultAsync<int>(ct);

            IList<T> items = await source
                .Skip((paginationRequest.Page - 1) * paginationRequest.PageSize)
                .Take(paginationRequest.PageSize)
                .ListAsync<T>(ct);

            return new PagedResult<T>(paginationRequest.Page, paginationRequest.PageSize, count, items);
        }
        
        internal static async Task<PagedResult<T>> ToPagedResultAsync<T>(
            this ICriteria source,
            IGridifyPagination paginationRequest,
            CancellationToken ct = default)
        {
            int count = await source
                .SetProjection(Projections.RowCount())
                .UniqueResultAsync<int>(ct);

            IList<T> items = await source
                .SetFirstResult((paginationRequest.Page - 1) * paginationRequest.PageSize)
                .SetMaxResults(paginationRequest.PageSize)
                .ListAsync<T>(ct);

            return new PagedResult<T>(paginationRequest.Page, paginationRequest.PageSize, count, items);
        }
    }
}
