using BookStore.Domain.Models.BookModel;
using BookStore.Domain.Persistence.Contracts.Books;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects;
using BookStore.Infra.NHibernate;
using BookStore.Infra.Utils;
using NHibernate;

namespace BookStore.Infra.Repositories.Books
{
    internal sealed class BooksRepositoryHQL(NHibernateContext context) : IQueryableBooksRepository
    {
        public async Task<PagedResult<Book>> GetAllAsync(QueryRequest request, CancellationToken ct = default)
        {
            // HQL does not support Gridify directly, so we're not implementing filtering or ordering
            IQuery query = HQL.GetAllQuery<Book>(context.Session);
            IQuery countQuery = HQL.CountQuery<Book>(context.Session);

            IQuery pagedQuery = query
                .SetFirstResult((request.Page - 1) * request.PageSize)
                .SetMaxResults(request.PageSize);

            int totalCount = await countQuery.UniqueResultAsync<int>(ct);
            IList<Book> books = await pagedQuery.ListAsync<Book>(ct);

            return new PagedResult<Book>(request.Page, request.PageSize, totalCount, books);
        }

        public async Task<PagedResult<Book>> GetAllWithAuthorsFetchedAsync(QueryRequest request, CancellationToken ct = default)
        {
            // HQL does not support Gridify directly, so we're not implementing filtering or ordering
            IQuery query = context.Session.CreateQuery("from Book b join fetch e.Author");
            IQuery countQuery = HQL.CountQuery<Book>(context.Session);

            IQuery pagedQuery = query
                .SetFirstResult((request.Page - 1) * request.PageSize)
                .SetMaxResults(request.PageSize);

            int totalCount = await countQuery.UniqueResultAsync<int>(ct);
            IList<Book> books = await pagedQuery.ListAsync<Book>(ct);

            return new PagedResult<Book>(request.Page, request.PageSize, totalCount, books);
        }

        public async Task<Book?> GetAsync(BookId id, CancellationToken ct = default)
        {
            IQuery query = HQL.GetByIdQuery<Book, BookId, int>(context.Session, id);
            return await query.UniqueResultAsync<Book?>(ct);
        }

        public async Task<long> CountAsync(CancellationToken ct = default)
        {
            IQuery query = HQL.CountQuery<Book>(context.Session);
            return await query.UniqueResultAsync<long>(ct);
        }
    }
}
