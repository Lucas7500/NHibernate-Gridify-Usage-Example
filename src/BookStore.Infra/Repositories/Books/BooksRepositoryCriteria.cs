using BookStore.Domain.Models.AuthorModel;
using BookStore.Domain.Models.BookModel;
using BookStore.Domain.Persistence;
using BookStore.Domain.Persistence.Contracts.Books;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects;
using BookStore.Infra.Extensions;
using BookStore.Infra.Mappers;
using BookStore.Infra.NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace BookStore.Infra.Repositories.Books
{
    internal sealed class BooksRepositoryCriteria(NHibernateContext context) : IQueryableBooksRepository
    {
        public async Task<PagedResult<Book>> GetAllAsync(QueryRequest request, CancellationToken ct = default)
        {
            // Criteria does not support Gridify directly, so we're not implementing filtering or ordering
            ICriteria criteria = context.Session.CreateCriteria<Book>();

            return await criteria.ToPagedResultAsync<Book>(request.ToGridifyQuery(), ct);
        }

        public async Task<PagedResult<Book>> GetAllWithAuthorsFetchedAsync(QueryRequest request, CancellationToken ct = default)
        {
            // Criteria does not support Gridify directly, so we're not implementing filtering or ordering
            const string BookAlias = "b";
            const string AuthorAlias = "a";

            ICriteria criteria = context.Session.CreateCriteria<Book>(BookAlias);
            ICriteria resultQuery = criteria
                .CreateAlias($"{BookAlias}.{nameof(Book.Author)}", AuthorAlias)
                .Fetch(nameof(Author));

            return await resultQuery.ToPagedResultAsync<Book>(request.ToGridifyQuery(), ct);
        }

        public async Task<Book?> GetAsync(BookId id, CancellationToken ct = default)
        {
            ICriteria criteria = context.Session.CreateCriteria<Book>();

            return await criteria
                .Add(Restrictions.Eq(nameof(Book.IdValue), id.Value))
                .UniqueResultAsync<Book?>(ct);
        }

        public async Task<long> CountAsync(CancellationToken ct = default)
        {
            ICriteria criteria = context.Session.CreateCriteria<Book>();

            return await criteria
                .SetProjection(Projections.RowCount())
                .UniqueResultAsync<long>(ct);
        }
    }
}
