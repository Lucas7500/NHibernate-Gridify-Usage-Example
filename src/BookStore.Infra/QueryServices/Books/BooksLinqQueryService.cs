using BookStore.Application.DTOs.Books.Responses;
using BookStore.Application.Mappers;
using BookStore.Application.QueryServices.Contracts;
using BookStore.Domain.Models.BookModel;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects;
using BookStore.Infra.Mappers;
using BookStore.Infra.NHibernate;
using Gridify;
using NHibernate;
using NHibernate.Linq;

namespace BookStore.Infra.QueryServices.Books
{
    internal sealed class BooksLinqQueryService(NHibernateContext context) : IBooksQueryService
    {
        public async Task<PagedResult<BookOnlyResponse>> GetAllAsync(QueryRequest request, CancellationToken ct = default)
        {
            IGridifyQuery gridifyQuery = request.ToGridifyQuery();

            IQueryable<Book> query = context.Session.Query<Book>();
            IQueryable<Book> resultQuery = query.ApplyFilteringAndOrdering(gridifyQuery);

            IFutureValue<int> futureCount = resultQuery.ToFutureValue(q => q.Count());
            IFutureEnumerable<BookOnlyResponse> futureItems = resultQuery
                .ApplyPaging(gridifyQuery)
                .Select(a => a.ToBookOnlyBookResponse())
                .ToFuture();

            int count = await futureCount.GetValueAsync(ct);
            IEnumerable<BookOnlyResponse> items = await futureItems.GetEnumerableAsync(ct);

            return new PagedResult<BookOnlyResponse>(gridifyQuery.Page, gridifyQuery.PageSize, count, items);
        }

        public async Task<PagedResult<BookWithAuthorResponse>> GetAllWithAuthorsFetchedAsync(QueryRequest request, CancellationToken ct = default)
        {
            IGridifyQuery gridifyQuery = request.ToGridifyQuery();

            IQueryable<Book> query = context.Session.Query<Book>();
            IQueryable<Book> resultQuery = query.ApplyFilteringAndOrdering(gridifyQuery);

            IFutureValue<int> futureCount = resultQuery.ToFutureValue(q => q.Count());
            IFutureEnumerable<BookWithAuthorResponse> futureItems = resultQuery
                .ApplyPaging(gridifyQuery)
                .Select(a => a.ToBookWithAuthorResponse())
                .ToFuture();

            int count = await futureCount.GetValueAsync(ct);
            IEnumerable<BookWithAuthorResponse> items = await futureItems.GetEnumerableAsync(ct);

            return new PagedResult<BookWithAuthorResponse>(gridifyQuery.Page, gridifyQuery.PageSize, count, items);
        }

        public async Task<BookWithAuthorResponse?> GetByIdAsync(BookId id, CancellationToken ct = default)
        {
            IQueryable<BookWithAuthorResponse> query = context
                .Session
                .Query<Book>()
                .Where(b => b.IdValue == id.Value)
                .Select(b => b.ToBookWithAuthorResponse());

            return await query.FirstOrDefaultAsync(ct);
        }

        public async Task<long> CountAsync(CancellationToken ct = default)
        {
            IQueryable<Book> query = context.Session.Query<Book>();
            return await query.LongCountAsync(ct);
        }
    }
}
