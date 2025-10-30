using BookStore.Domain.Models;
using BookStore.Domain.Persistence;
using BookStore.Infra.NHibernate;
using NHibernate;

namespace BookStore.Infra.Repositories
{
    internal sealed class BooksRepositorySQL(NHibernateContext context) : IBooksRepository
    {
        public async Task Add(Book entity, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task AddAuthor(Author author, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task AddOrUpdate(Book entity, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<long> Count(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<long> CountAuthors(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Book entity, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAuthor(Author author, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Book?> Get(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Book>> GetAll(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Author>> GetAllAuthors(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Author?> GetAuthor(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Book>> GetBooksWithAuthors(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Book entity, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAuthor(Author author, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
