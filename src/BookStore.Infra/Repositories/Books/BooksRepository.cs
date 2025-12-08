using BookStore.Domain.Models.BookModel;
using BookStore.Domain.Persistence.Contracts.Books;
using BookStore.Domain.ValueObjects;
using BookStore.Infra.NHibernate;
using NHibernate.Linq;

namespace BookStore.Infra.Repositories.Books
{
    internal sealed class BooksRepository(NHibernateContext context) : IBooksRepository
    {
        public async Task<Book?> GetByIdAsync(BookId id, CancellationToken ct = default)
        {
            IQueryable<Book> query = context
                .Session
                .Query<Book>()
                .Where(b => b.IdValue == id.Value);

            return await query.FirstOrDefaultAsync(ct);
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
