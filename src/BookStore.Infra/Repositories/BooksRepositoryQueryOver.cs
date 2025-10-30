using BookStore.Domain.Models;
using BookStore.Domain.Persistence;
using BookStore.Infra.NHibernate;
using NHibernate;

namespace BookStore.Infra.Repositories
{
    internal sealed class BooksRepositoryQueryOver(NHibernateContext context) : IQueryableBooksRepository
    {
        public async Task<long> Count(CancellationToken ct = default)
        {
            IQueryOver<Book, Book> query = context.Session.QueryOver<Book>();
            
            return await query
                .ToRowCountQuery()
                .SingleOrDefaultAsync<long>(ct);
        }

        public async Task<long> CountAuthors(CancellationToken ct = default)
        {
            IQueryOver<Author, Author> query = context.Session.QueryOver<Author>();
            
            return await query
                .ToRowCountQuery()
                .SingleOrDefaultAsync<long>(ct);
        }

        public async Task<Book?> Get(int id, CancellationToken ct = default)
        {
            IQueryOver<Book, Book> query = context.Session.QueryOver<Book>();

            return await query
                .Where(b => b.Id == id)
                .SingleOrDefaultAsync(ct);
        }

        public async Task<IEnumerable<Book>> GetAll(CancellationToken ct = default)
        {
            IQueryOver<Book, Book> query = context.Session.QueryOver<Book>();
            return await query.ListAsync<Book>(ct);
        }

        public async Task<IEnumerable<Author>> GetAllAuthors(CancellationToken ct = default)
        {
            IQueryOver<Author, Author> query = context.Session.QueryOver<Author>();
            return await query.ListAsync<Author>(ct);
        }

        public async Task<Author?> GetAuthor(Guid id, CancellationToken ct = default)
        {
            IQueryOver<Author, Author> query = context.Session.QueryOver<Author>();

            return await query
                .Where(a => a.Id == id)
                .SingleOrDefaultAsync(ct);
        }

        public async Task<IEnumerable<Book>> GetBooksWithAuthors(CancellationToken ct = default)
        {
            IQueryOver<Book, Book> query = context
                .Session
                .QueryOver<Book>()
                .Fetch(SelectMode.Fetch, b => b.Author);

            return await query.ListAsync<Book>(ct);
        }
    }
}
