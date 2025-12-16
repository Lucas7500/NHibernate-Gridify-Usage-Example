using BookStore.Domain.Models.BookModel;
using BookStore.Domain.Persistence.Contracts.Books;
using BookStore.Domain.ValueObjects;
using NHibernate;
using NHibernate.Linq;

namespace BookStore.Infra.Repositories.Books
{
    internal sealed class BooksRepository(ISession session) : IBooksRepository
    {
        public async Task<Book?> GetByIdAsync(BookId id, CancellationToken ct = default)
        {
            IQueryable<Book> query = session
                .Query<Book>()
                .Fetch(b => b.Author)
                .Where(b => b.IdValue == id.Value);

            return await query.FirstOrDefaultAsync(ct);
        }

        public async Task AddAsync(Book entity, CancellationToken ct = default)
        {
            await session.SaveAsync(entity, ct);
        }

        public async Task AddOrUpdateAsync(Book entity, CancellationToken ct = default)
        {
            await session.SaveOrUpdateAsync(entity, ct);
        }

        public async Task UpdateAsync(Book entity, CancellationToken ct = default)
        {
            await session.UpdateAsync(entity, ct);
        }

        public async Task DeleteAsync(BookId id, CancellationToken ct = default)
        {
            await session
                .Query<Book>()
                .Where(b => b.IdValue == id.Value)
                .DeleteAsync(ct);
        }

        public async Task DeleteAsync(Book entity, CancellationToken ct = default)
        {
            await session.DeleteAsync(entity, ct);
        }
    }
}
