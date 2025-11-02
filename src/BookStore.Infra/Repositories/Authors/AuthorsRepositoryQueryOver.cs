using BookStore.Domain.Models.AuthorModel;
using BookStore.Domain.Persistence.Contracts.Authors;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects;
using BookStore.Infra.Extensions;
using BookStore.Infra.Mappers;
using BookStore.Infra.NHibernate;
using NHibernate;

namespace BookStore.Infra.Repositories.Authors
{
    internal sealed class AuthorsRepositoryQueryOver(NHibernateContext context) : IQueryableAuthorsRepository
    {
        public async Task<PagedResult<Author>> GetAllAsync(QueryRequest request, CancellationToken ct = default)
        {
            // QueryOver does not support Gridify directly, so we're not implementing filtering or ordering
            IQueryOver<Author, Author> query = context.Session.QueryOver<Author>();

            return await query.ToPagedResultAsync(request.ToGridifyQuery(), ct);
        }

        public async Task<Author?> GetAsync(AuthorId id, CancellationToken ct = default)
        {
            IQueryOver<Author, Author> query = context.Session.QueryOver<Author>();

            return await query
                .Where(b => b.IdValue == id.Value)
                .SingleOrDefaultAsync(ct);
        }

        public async Task<long> CountAsync(CancellationToken ct = default)
        {
            IQueryOver<Author, Author> query = context.Session.QueryOver<Author>();

            return await query
                .ToRowCountQuery()
                .SingleOrDefaultAsync<long>(ct);
        }
    }
}
