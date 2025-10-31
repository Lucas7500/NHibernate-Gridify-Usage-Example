using BookStore.Domain.Models;
using BookStore.Domain.Persistence;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Infra.Extensions;
using BookStore.Infra.NHibernate;
using Gridify;
using NHibernate;
using NHibernate.Linq;

namespace BookStore.Infra.Repositories
{
    internal sealed class BooksRepositoryLINQ(NHibernateContext context) : IBooksRepository
    {
        public async Task AddAsync(Book entity, CancellationToken ct = default)
        {
            await context.Session.SaveAsync(entity, ct);
        }

        public async Task AddAuthorAsync(Author author, CancellationToken ct = default)
        {
            await context.Session.SaveAsync(author, ct);
        }

        public async Task AddOrUpdateAsync(Book entity, CancellationToken ct = default)
        {
            await context.Session.SaveOrUpdateAsync(entity, ct);
        }

        public async Task<long> CountAsync(CancellationToken ct = default)
        {
            IQueryable<Book> query = context.Session.Query<Book>();
            return await query.LongCountAsync(ct);
        }

        public async Task<long> CountAuthorsAsync(CancellationToken ct = default)
        {
            IQueryable<Author> query = context.Session.Query<Author>();
            return await query.LongCountAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            await context.Session.DeleteAsync(id, ct);
        }

        public async Task DeleteAsync(Book entity, CancellationToken ct = default)
        {
            await context.Session.DeleteAsync(entity, ct);
        }

        public async Task DeleteAuthorAsync(Author author, CancellationToken ct = default)
        {
            await context.Session.DeleteAsync(author, ct);
        }

        public async Task<Book?> GetAsync(int id, CancellationToken ct = default)
        {
            IQueryable<Book> query = context
                .Session
                .Query<Book>()
                .Where(b => b.Id == id);

            return await query.FirstOrDefaultAsync(ct);
        }

        public async Task<PagedResult<Book>> GetAllAsync(QueryRequest request, CancellationToken ct = default)
        {
            IQueryable<Book> query = context.Session.Query<Book>();
            IQueryable<Book> resultQuery = query.ApplyFilteringAndOrdering(request);

            return await resultQuery.ToPagedResultAsync(request, ct);
        }

        public async Task<PagedResult<Author>> GetAllAuthorsAsync(QueryRequest request, CancellationToken ct = default)
        {
            IQueryable<Author> query = context.Session.Query<Author>();
            IQueryable<Author> resultQuery = query.ApplyFilteringAndOrdering(request);

            return await resultQuery.ToPagedResultAsync(request, ct);
        }

        public async Task<Author?> GetAuthorByIdAsync(Guid id, CancellationToken ct = default)
        {
            IQueryable<Author> query = context
                .Session
                .Query<Author>()
                .Where(a => a.Id == id);

            return await query.FirstOrDefaultAsync(ct);
        }

        public async Task<PagedResult<Book>> GetBooksWithAuthorsFetchedAsync(QueryRequest request, CancellationToken ct = default)
        {
            IQueryable<Book> query = context
                .Session
                .Query<Book>()
                .Fetch(b => b.Author);

            IQueryable<Book> resultQuery = query.ApplyFilteringAndOrdering(request);

            return await resultQuery.ToPagedResultAsync(request, ct);
        }

        public async Task UpdateAsync(Book entity, CancellationToken ct = default)
        {
            await context.Session.UpdateAsync(entity, ct);
        }

        public async Task UpdateAuthorAsync(Author author, CancellationToken ct = default)
        {
            await context.Session.UpdateAsync(author, ct);
        }
    }
}
