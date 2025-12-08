using BookStore.Application.DTOs.Authors.Responses;
using BookStore.Application.Mappers;
using BookStore.Application.QueryServices.Contracts;
using BookStore.Domain.Models.AuthorModel;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects;
using BookStore.Infra.Mappers;
using BookStore.Infra.NHibernate;
using Gridify;
using NHibernate;
using NHibernate.Linq;

namespace BookStore.Infra.QueryServices.Authors
{
    internal sealed class AuthorsLinqQueryService(NHibernateContext context) : IAuthorsQueryService
    {
        public async Task<PagedResult<AuthorResponse>> GetAllAsync(QueryRequest request, CancellationToken ct = default)
        {
            IGridifyQuery gridifyQuery = request.ToGridifyQuery();

            IQueryable<Author> query = context.Session.Query<Author>();
            IQueryable<Author> resultQuery = query.ApplyFilteringAndOrdering(gridifyQuery);

            IFutureValue<int> futureCount = resultQuery.ToFutureValue(q => q.Count());
            IFutureEnumerable<AuthorResponse> futureItems = resultQuery
                .ApplyPaging(gridifyQuery)
                .Select(a => a.ToResponse())
                .ToFuture();

            int count = await futureCount.GetValueAsync(ct);
            IEnumerable<AuthorResponse> items = await futureItems.GetEnumerableAsync(ct);

            return new PagedResult<AuthorResponse>(gridifyQuery.Page, gridifyQuery.PageSize, count, items);
        }

        public async Task<AuthorResponse?> GetByIdAsync(AuthorId id, CancellationToken ct = default)
        {
            IQueryable<AuthorResponse> query = context
                .Session
                .Query<Author>()
                .Where(a => a.IdValue == id.Value)
                .Select(a => a.ToResponse());

            return await query.FirstOrDefaultAsync(ct);
        }

        public async Task<long> CountAsync(CancellationToken ct = default)
        {
            IQueryable<Author> query = context.Session.Query<Author>();
            return await query.LongCountAsync(ct);
        }
    }
}
