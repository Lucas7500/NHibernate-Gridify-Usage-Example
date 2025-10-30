using BookStore.Domain.Models;
using BookStore.Domain.Persistence;
using BookStore.Infra.NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace BookStore.Infra.Repositories
{
    internal sealed class BooksRepositoryCriteria(NHibernateContext context) : IQueryableBooksRepository
    {
        public async Task<long> Count(CancellationToken ct = default)
        {
            ICriteria criteria = context.Session.CreateCriteria<Book>();

            return await criteria
                .SetProjection(Projections.RowCount())
                .UniqueResultAsync<long>(ct);
        }

        public async Task<long> CountAuthors(CancellationToken ct = default)
        {
            ICriteria criteria = context.Session.CreateCriteria<Author>();

            return await criteria
                .SetProjection(Projections.RowCount())
                .UniqueResultAsync<long>(ct);
        }

        public async Task<Book?> Get(int id, CancellationToken ct = default)
        {
            ICriteria criteria = context.Session.CreateCriteria<Book>();

            return await criteria
                .Add(Restrictions.Eq(nameof(Book.Id), id))
                .UniqueResultAsync<Book?>(ct);
        }

        public async Task<IEnumerable<Book>> GetAll(CancellationToken ct = default)
        {
            ICriteria criteria = context.Session.CreateCriteria<Book>();

            return await criteria.ListAsync<Book>(ct);
        }

        public async Task<IEnumerable<Author>> GetAllAuthors(CancellationToken ct = default)
        {
            ICriteria criteria = context.Session.CreateCriteria<Author>();

            return await criteria.ListAsync<Author>(ct);
        }

        public async Task<Author?> GetAuthor(Guid id, CancellationToken ct = default)
        {
            ICriteria criteria = context.Session.CreateCriteria<Author>();

            return await criteria
                .Add(Restrictions.Eq(nameof(Author.Id), id))
                .UniqueResultAsync<Author?>(ct);
        }

        public async Task<IEnumerable<Book>> GetBooksWithAuthors(CancellationToken ct = default)
        {
            const string BookAlias = "b";
            const string AuthorAlias = "a";

            ICriteria criteria = context.Session.CreateCriteria<Book>(BookAlias);

            return await criteria
                .CreateAlias($"{BookAlias}.{nameof(Book.Author)}", AuthorAlias)
                .Fetch(nameof(Author))
                .ListAsync<Book>(ct);
        }
    }
}
