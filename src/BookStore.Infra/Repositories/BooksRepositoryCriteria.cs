using BookStore.Domain.Models;
using BookStore.Domain.Persistence;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Infra.Extensions;
using BookStore.Infra.NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace BookStore.Infra.Repositories
{
    internal sealed class BooksRepositoryCriteria(NHibernateContext context) : IQueryableBooksRepository
    {
        public async Task<long> CountAsync(CancellationToken ct = default)
        {
            ICriteria criteria = context.Session.CreateCriteria<Book>();

            return await criteria
                .SetProjection(Projections.RowCount())
                .UniqueResultAsync<long>(ct);
        }

        public async Task<long> CountAuthorsAsync(CancellationToken ct = default)
        {
            ICriteria criteria = context.Session.CreateCriteria<Author>();

            return await criteria
                .SetProjection(Projections.RowCount())
                .UniqueResultAsync<long>(ct);
        }

        public async Task<Book?> GetAsync(int id, CancellationToken ct = default)
        {
            ICriteria criteria = context.Session.CreateCriteria<Book>();

            return await criteria
                .Add(Restrictions.Eq(nameof(Book.Id), id))
                .UniqueResultAsync<Book?>(ct);
        }

        public async Task<PagedResult<Book>> GetAllAsync(QueryRequest request, CancellationToken ct = default)
        {
            // Criteria does not support Gridify directly, so we're not implementing filtering or ordering
            ICriteria criteria = context.Session.CreateCriteria<Book>();

            return await criteria.ToPagedResultAsync<Book>(request, ct);
        }

        public async Task<PagedResult<Author>> GetAllAuthorsAsync(QueryRequest request, CancellationToken ct = default)
        {
            // Criteria does not support Gridify directly, so we're not implementing filtering or ordering
            ICriteria criteria = context.Session.CreateCriteria<Author>();

            return await criteria.ToPagedResultAsync<Author>(request, ct);
        }

        public async Task<Author?> GetAuthorByIdAsync(Guid id, CancellationToken ct = default)
        {
            ICriteria criteria = context.Session.CreateCriteria<Author>();

            return await criteria
                .Add(Restrictions.Eq(nameof(Author.Id), id))
                .UniqueResultAsync<Author?>(ct);
        }

        public async Task<PagedResult<Book>> GetBooksWithAuthorsFetchedAsync(QueryRequest request, CancellationToken ct = default)
        {
            // Criteria does not support Gridify directly, so we're not implementing filtering or ordering
            const string BookAlias = "b";
            const string AuthorAlias = "a";

            ICriteria criteria = context.Session.CreateCriteria<Book>(BookAlias);
            ICriteria resultQuery = criteria
                .CreateAlias($"{BookAlias}.{nameof(Book.Author)}", AuthorAlias)
                .Fetch(nameof(Author));

            return await resultQuery.ToPagedResultAsync<Book>(request, ct);
        }
    }
}
