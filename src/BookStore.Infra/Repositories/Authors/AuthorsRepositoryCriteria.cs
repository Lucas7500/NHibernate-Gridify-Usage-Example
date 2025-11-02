using BookStore.Domain.Models.AuthorModel;
using BookStore.Domain.Persistence.Contracts.Authors;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects;
using BookStore.Infra.Extensions;
using BookStore.Infra.Mappers;
using BookStore.Infra.NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace BookStore.Infra.Repositories.Authors
{
    internal sealed class AuthorsRepositoryCriteria(NHibernateContext context) : IQueryableAuthorsRepository
    {
        public async Task<PagedResult<Author>> GetAllAsync(QueryRequest request, CancellationToken ct = default)
        {
            // Criteria does not support Gridify directly, so we're not implementing filtering or ordering
            ICriteria criteria = context.Session.CreateCriteria<Author>();

            return await criteria.ToPagedResultAsync<Author>(request.ToGridifyQuery(), ct);
        }

        public async Task<Author?> GetAsync(AuthorId id, CancellationToken ct = default)
        {
            ICriteria criteria = context.Session.CreateCriteria<Author>();

            return await criteria
                .Add(Restrictions.Eq(nameof(Author.IdValue), id.Value))
                .UniqueResultAsync<Author?>(ct);
        }

        public async Task<long> CountAsync(CancellationToken ct = default)
        {
            ICriteria criteria = context.Session.CreateCriteria<Author>();

            return await criteria
                .SetProjection(Projections.RowCount())
                .UniqueResultAsync<long>(ct);
        }
    }
}
