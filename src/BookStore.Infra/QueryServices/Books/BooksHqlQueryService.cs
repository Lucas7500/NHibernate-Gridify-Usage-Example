using BookStore.Application.DTOs.Books.Responses;
using BookStore.Application.QueryServices.Contracts;
using BookStore.Domain.Models.BookModel;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects;
using BookStore.Infra.NHibernate;
using BookStore.Infra.Utils;
using NHibernate;
using NHibernate.Transform;

namespace BookStore.Infra.QueryServices.Books
{
    internal sealed class BooksHqlQueryService(NHibernateContext context) : IBooksQueryService
    {
        public async Task<PagedResult<BookOnlyResponse>> GetAllAsync(QueryRequest request, CancellationToken ct = default)
        {
            // HQL does not support Gridify directly, so we're not implementing filtering or ordering
            IQuery query = HQL.GetAllQuery<Book>(context.Session);
            IQuery countQuery = HQL.CountQuery<Book>(context.Session);

            IQuery pagedQuery = query
                .SetFirstResult((request.Page - 1) * request.PageSize)
                .SetMaxResults(request.PageSize);

            int totalCount = await countQuery.UniqueResultAsync<int>(ct);
            
            IList<BookOnlyResponse> books = await pagedQuery
                .SetResultTransformer(Transformers.AliasToBean<BookOnlyResponse>())
                .ListAsync<BookOnlyResponse>(ct);

            return new PagedResult<BookOnlyResponse>(request.Page, request.PageSize, totalCount, books);
        }

        public async Task<PagedResult<BookWithAuthorResponse>> GetAllWithAuthorsFetchedAsync(QueryRequest request, CancellationToken ct = default)
        {
            // HQL does not support Gridify directly, so we're not implementing filtering or ordering
            IQuery query = context.Session.CreateQuery("from Book b join fetch e.Author");
            IQuery countQuery = HQL.CountQuery<Book>(context.Session);

            IQuery pagedQuery = query
                .SetFirstResult((request.Page - 1) * request.PageSize)
                .SetMaxResults(request.PageSize);

            int totalCount = await countQuery.UniqueResultAsync<int>(ct);

            IList<BookWithAuthorResponse> books = await pagedQuery
                .SetResultTransformer(Transformers.AliasToBean<BookWithAuthorResponse>())
                .ListAsync<BookWithAuthorResponse>(ct);

            return new PagedResult<BookWithAuthorResponse>(request.Page, request.PageSize, totalCount, books);
        }

        public async Task<BookWithAuthorResponse?> GetByIdAsync(BookId id, CancellationToken ct = default)
        {
            IQuery query = HQL.GetByIdQuery<Book, BookId, int>(context.Session, id);
            return await query
                .SetResultTransformer(Transformers.AliasToBean<BookWithAuthorResponse>())
                .UniqueResultAsync<BookWithAuthorResponse?>(ct);
        }

        public async Task<long> CountAsync(CancellationToken ct = default)
        {
            IQuery query = HQL.CountQuery<Book>(context.Session);
            return await query.UniqueResultAsync<long>(ct);
        }
    }
}
