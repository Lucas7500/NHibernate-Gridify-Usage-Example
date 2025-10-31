using BookStore.Domain.Models;
using BookStore.Domain.Persistence;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Infra.Extensions;
using BookStore.Infra.NHibernate;
using NHibernate;

namespace BookStore.Infra.Repositories
{
    internal sealed class BooksRepositoryQueryOver(NHibernateContext context) : IQueryableBooksRepository
    {
        public async Task<long> CountAsync(CancellationToken ct = default)
        {
            IQueryOver<Book, Book> query = context.Session.QueryOver<Book>();
            
            return await query
                .ToRowCountQuery()
                .SingleOrDefaultAsync<long>(ct);
        }

        public async Task<long> CountAuthorsAsync(CancellationToken ct = default)
        {
            IQueryOver<Author, Author> query = context.Session.QueryOver<Author>();
            
            return await query
                .ToRowCountQuery()
                .SingleOrDefaultAsync<long>(ct);
        }

        public async Task<Book?> GetAsync(int id, CancellationToken ct = default)
        {
            IQueryOver<Book, Book> query = context.Session.QueryOver<Book>();

            return await query
                .Where(b => b.Id == id)
                .SingleOrDefaultAsync(ct);
        }

        public async Task<PagedResult<Book>> GetAllAsync(QueryRequest request, CancellationToken ct = default)
        {
            // QueryOver does not support Gridify directly, so we're not implementing filtering or ordering
            IQueryOver<Book, Book> query = context.Session.QueryOver<Book>();

            return await query.ToPagedResultAsync(request, ct);
        }

        public async Task<PagedResult<Author>> GetAllAuthorsAsync(QueryRequest request, CancellationToken ct = default)
        {
            // QueryOver does not support Gridify directly, so we're not implementing filtering or ordering
            IQueryOver<Author, Author> query = context.Session.QueryOver<Author>();

            return await query.ToPagedResultAsync(request, ct);

        }

        public async Task<Author?> GetAuthorByIdAsync(Guid id, CancellationToken ct = default)
        {
            // QueryOver does not support Gridify directly, so we're not implementing filtering or ordering
            IQueryOver<Author, Author> query = context.Session.QueryOver<Author>();

            return await query
                .Where(a => a.Id == id)
                .SingleOrDefaultAsync(ct);
        }

        public async Task<PagedResult<Book>> GetBooksWithAuthorsFetchedAsync(QueryRequest request, CancellationToken ct = default)
        {
            // QueryOver does not support Gridify directly, so we're not implementing filtering or ordering
            IQueryOver<Book, Book> query = context
                .Session
                .QueryOver<Book>()
                .Fetch(SelectMode.Fetch, b => b.Author);

            return await query.ToPagedResultAsync(request, ct);
        }
    }
}
