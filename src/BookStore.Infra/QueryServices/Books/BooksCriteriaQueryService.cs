using BookStore.Application.DTOs.Books.Responses;
using BookStore.Application.QueryServices.Contracts;
using BookStore.Domain.Models.AuthorModel;
using BookStore.Domain.Models.BookModel;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects;
using BookStore.Infra.Extensions;
using BookStore.Infra.Mappers;
using BookStore.Infra.NHibernate;
using Gridify;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace BookStore.Infra.QueryServices.Books
{
    internal sealed class BooksCriteriaQueryService(NHibernateContext context) : IBooksQueryService
    {
        public async Task<PagedResult<BookOnlyResponse>> GetAllAsync(QueryRequest request, CancellationToken ct = default)
        {
            // Criteria does not support Gridify directly, so we're not implementing filtering or ordering
            ICriteria criteria = context.Session.CreateCriteria<Book>();

            return await criteria.ToPagedResultWithDtoAsync<BookOnlyResponse>(request.ToGridifyQuery(), ct);
        }

        public async Task<PagedResult<BookWithAuthorResponse>> GetAllWithAuthorsFetchedAsync(QueryRequest request, CancellationToken ct = default)
        {
            // Criteria does not support Gridify directly, so we're not implementing filtering or ordering
            const string BookAlias = "b";
            const string AuthorAlias = "a";

            ICriteria criteria = context.Session.CreateCriteria<Book>(BookAlias);
            ICriteria resultQuery = criteria
                .CreateAlias($"{BookAlias}.{nameof(Book.Author)}", AuthorAlias)
                .Fetch(nameof(Author));

            IGridifyQuery gridifyQuery = request.ToGridifyQuery();

            int count = await resultQuery
                .SetProjection(Projections.RowCount())
                .UniqueResultAsync<int>(ct);

            IList<BookWithAuthorResponse> items = await resultQuery
                .SetFirstResult((gridifyQuery.Page - 1) * gridifyQuery.PageSize)
                .SetMaxResults(gridifyQuery.PageSize)
                .SetResultTransformer(Transformers.AliasToBean<BookWithAuthorResponse>())
                .ListAsync<BookWithAuthorResponse>(ct);

            return new PagedResult<BookWithAuthorResponse>(gridifyQuery.Page, gridifyQuery.PageSize, count, items);
        }

        public async Task<BookWithAuthorResponse?> GetByIdAsync(BookId id, CancellationToken ct = default)
        {
            ICriteria criteria = context.Session.CreateCriteria<Book>();

            return await criteria
                .Add(Restrictions.Eq(nameof(Book.IdValue), id.Value))
                .SetResultTransformer(Transformers.AliasToBean<BookWithAuthorResponse>())
                .UniqueResultAsync<BookWithAuthorResponse?>(ct);
        }

        public async Task<long> CountAsync(CancellationToken ct = default)
        {
            ICriteria criteria = context.Session.CreateCriteria<Book>();

            return await criteria
                .SetProjection(Projections.RowCount())
                .UniqueResultAsync<long>(ct);
        }
    }
}
