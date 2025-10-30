using BookStore.Domain.Models;
using BookStore.Domain.Persistence;
using BookStore.Infra.NHibernate;
using BookStore.Infra.Utils;
using NHibernate;

namespace BookStore.Infra.Repositories
{
    internal sealed class BooksRepositoryHQL(NHibernateContext context) : IQueryableBooksRepository
    {
        public async Task<long> Count(CancellationToken ct = default)
        {
            IQuery query = HQL.CountQuery<Book>(context.Session);
            return await query.UniqueResultAsync<long>(ct);
        }

        public async Task<long> CountAuthors(CancellationToken ct = default)
        {
            IQuery query = HQL.CountQuery<Author>(context.Session);
            return await query.UniqueResultAsync<long>(ct);
        }

        public async Task<Book?> Get(int id, CancellationToken ct = default)
        {
            IQuery query = HQL.GetByIdQuery<Book, int>(context.Session, id);
            return await query.UniqueResultAsync<Book?>(ct);
        }

        public async Task<IEnumerable<Book>> GetAll(CancellationToken ct = default)
        {
            IQuery query = HQL.GetAllQuery<Book>(context.Session);
            return await query.ListAsync<Book>(ct);
        }

        public async Task<Author?> GetAuthor(Guid id, CancellationToken ct = default)
        {
            IQuery query = HQL.GetByIdQuery<Author, Guid>(context.Session, id);
            return await query.UniqueResultAsync<Author?>(ct);
        }

        public async Task<IEnumerable<Author>> GetAllAuthors(CancellationToken ct = default)
        {
            IQuery query = HQL.GetAllQuery<Author>(context.Session);
            return await query.ListAsync<Author>(ct);
        }

        public async Task<IEnumerable<Book>> GetBooksWithAuthors(CancellationToken ct = default)
        {
            IQuery query = context.Session.CreateQuery("from Book b join fetch e.Author");
            return await query.ListAsync<Book>(ct);
        }
    }
}
