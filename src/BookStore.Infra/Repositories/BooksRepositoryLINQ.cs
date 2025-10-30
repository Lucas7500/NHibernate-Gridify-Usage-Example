using BookStore.Domain.Models;
using BookStore.Domain.Persistence;
using BookStore.Domain.Persistence.Common;
using BookStore.Infra.NHibernate;
using NHibernate;
using NHibernate.Linq;

namespace BookStore.Infra.Repositories
{
    internal sealed class BooksRepositoryLINQ(NHibernateContext context) : IBooksRepository
    {
        public async Task Add(Book entity, CancellationToken ct = default)
        {
            await context.Session.SaveAsync(entity, ct);
        }

        public async Task AddAuthor(Author author, CancellationToken ct = default)
        {
            await context.Session.SaveAsync(author, ct);
        }

        public async Task AddOrUpdate(Book entity, CancellationToken ct = default)
        {
            await context.Session.SaveOrUpdateAsync(entity, ct);
        }

        public async Task<long> Count(CancellationToken ct = default)
        {
            IQueryable<Book> query = context.Session.Query<Book>();
            return await query.LongCountAsync(ct);
        }

        public async Task<long> CountAuthors(CancellationToken ct = default)
        {
            IQueryable<Author> query = context.Session.Query<Author>();
            return await query.LongCountAsync(ct);
        }

        public async Task Delete(int id, CancellationToken ct = default)
        {
            await context.Session.DeleteAsync(id, ct);
        }

        public async Task Delete(Book entity, CancellationToken ct = default)
        {
            await context.Session.DeleteAsync(entity, ct);
        }

        public async Task DeleteAuthor(Author author, CancellationToken ct = default)
        {
            await context.Session.DeleteAsync(author, ct);
        }

        public async Task<Book?> Get(int id, CancellationToken ct = default)
        {
            IQueryable<Book> query = context
                .Session
                .Query<Book>()
                .Where(b => b.Id == id);

            return await query.FirstOrDefaultAsync(ct);
        }

        public async Task<IEnumerable<Book>> GetAll(CancellationToken ct = default)
        {
            IQueryable<Book> query = context.Session.Query<Book>();
            return await query.ToListAsync(ct);
        }

        public async Task<IEnumerable<Author>> GetAllAuthors(CancellationToken ct = default)
        {
            IQueryable<Author> query = context.Session.Query<Author>();
            return await query.ToListAsync(ct);
        }

        public async Task<Author?> GetAuthor(Guid id, CancellationToken ct = default)
        {
            IQueryable<Author> query = context
                .Session
                .Query<Author>()
                .Where(a => a.Id == id);

            return await query.FirstOrDefaultAsync(ct);
        }

        public async Task<IEnumerable<Book>> GetBooksWithAuthors(CancellationToken ct = default)
        {
            IQueryable<Book> query = context
                .Session
                .Query<Book>()
                .Fetch(b => b.Author);

            return await query.ToListAsync(ct);
        }

        public async Task Update(Book entity, CancellationToken ct = default)
        {
            await context.Session.UpdateAsync(entity, ct);
        }

        public async Task UpdateAuthor(Author author, CancellationToken ct = default)
        {
            await context.Session.UpdateAsync(author, ct);
        }
    }
}
