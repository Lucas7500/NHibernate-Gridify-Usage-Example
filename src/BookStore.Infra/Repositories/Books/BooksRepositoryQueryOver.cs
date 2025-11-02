using BookStore.Domain.Models.BookModel;
using BookStore.Domain.Persistence.Contracts.Books;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects;
using BookStore.Infra.Extensions;
using BookStore.Infra.Mappers;
using BookStore.Infra.NHibernate;
using NHibernate;

namespace BookStore.Infra.Repositories.Books
{
    internal sealed class BooksRepositoryQueryOver(NHibernateContext context) : IQueryableBooksRepository
    {
        public async Task<PagedResult<Book>> GetAllAsync(QueryRequest request, CancellationToken ct = default)
        {
            // QueryOver does not support Gridify directly, so we're not implementing filtering or ordering
            IQueryOver<Book, Book> query = context.Session.QueryOver<Book>();

            return await query.ToPagedResultAsync(request.ToGridifyQuery(), ct);
        }

        public async Task<PagedResult<Book>> GetAllWithAuthorsFetchedAsync(QueryRequest request, CancellationToken ct = default)
        {
            // QueryOver does not support Gridify directly, so we're not implementing filtering or ordering
            IQueryOver<Book, Book> query = context
                .Session
                .QueryOver<Book>()
                .Fetch(SelectMode.Fetch, b => b.Author);

            return await query.ToPagedResultAsync(request.ToGridifyQuery(), ct);
        }

        public async Task<Book?> GetAsync(BookId id, CancellationToken ct = default)
        {
            IQueryOver<Book, Book> query = context.Session.QueryOver<Book>();

            return await query
                .Where(b => b.IdValue == id.Value)
                .SingleOrDefaultAsync(ct);
        }

        public async Task<long> CountAsync(CancellationToken ct = default)
        {
            IQueryOver<Book, Book> query = context.Session.QueryOver<Book>();

            return await query
                .ToRowCountQuery()
                .SingleOrDefaultAsync<long>(ct);
        }
    }
}
