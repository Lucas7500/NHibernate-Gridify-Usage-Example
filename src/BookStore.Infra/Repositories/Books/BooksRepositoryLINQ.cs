using BookStore.Domain.Models.BookModel;
using BookStore.Domain.Persistence;
using BookStore.Domain.Persistence.Contracts.Books;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects;
using BookStore.Infra.Extensions;
using BookStore.Infra.Mappers;
using BookStore.Infra.NHibernate;
using Gridify;
using NHibernate;
using NHibernate.Linq;

namespace BookStore.Infra.Repositories.Books
{
    internal sealed class BooksRepositoryLINQ(NHibernateContext context) : IQueryableBooksRepository, IWriteableBooksRepository
    {
        public async Task<PagedResult<Book>> GetAllAsync(QueryRequest request, CancellationToken ct = default)
        {
            IGridifyQuery gridifyQuery = request.ToGridifyQuery();

            IQueryable<Book> query = context.Session.Query<Book>();
            IQueryable<Book> resultQuery = query.ApplyFilteringAndOrdering(gridifyQuery);

            return await resultQuery.ToPagedResultAsync(gridifyQuery, ct);
        }

        public async Task<PagedResult<Book>> GetAllWithAuthorsFetchedAsync(QueryRequest request, CancellationToken ct = default)
        {
            IGridifyQuery gridifyQuery = request.ToGridifyQuery();

            IQueryable<Book> query = context
                .Session
                .Query<Book>()
                .Fetch(b => b.Author);

            IQueryable<Book> resultQuery = query.ApplyFilteringAndOrdering(gridifyQuery);

            return await resultQuery.ToPagedResultAsync(gridifyQuery, ct);
        }

        public async Task<Book?> GetAsync(BookId id, CancellationToken ct = default)
        {
            IQueryable<Book> query = context
                .Session
                .Query<Book>()
                .Where(b => b.IdValue == id.Value);

            return await query.FirstOrDefaultAsync(ct);
        }

        public async Task<long> CountAsync(CancellationToken ct = default)
        {
            IQueryable<Book> query = context.Session.Query<Book>();
            return await query.LongCountAsync(ct);
        }

        public async Task AddAsync(Book entity, CancellationToken ct = default)
        {
            await context.Session.SaveAsync(entity, ct);
        }

        public async Task AddOrUpdateAsync(Book entity, CancellationToken ct = default)
        {
            await context.Session.SaveOrUpdateAsync(entity, ct);
        }

        public async Task UpdateAsync(Book entity, CancellationToken ct = default)
        {
            await context.Session.UpdateAsync(entity, ct);
        }

        public async Task DeleteAsync(BookId id, CancellationToken ct = default)
        {
            await context.Session.DeleteAsync(id.Value, ct);
        }

        public async Task DeleteAsync(Book entity, CancellationToken ct = default)
        {
            await context.Session.DeleteAsync(entity, ct);
        }
    }
}
