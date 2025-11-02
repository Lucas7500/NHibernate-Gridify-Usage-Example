using BookStore.Domain.Models.AuthorModel;
using BookStore.Domain.Persistence.Contracts.Authors;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects;
using BookStore.Infra.Extensions;
using BookStore.Infra.Mappers;
using BookStore.Infra.NHibernate;
using Gridify;
using NHibernate.Linq;

namespace BookStore.Infra.Repositories.Authors
{
    internal sealed class AuthorsRepositoryLINQ(NHibernateContext context) : IQueryableAuthorsRepository, IWriteableAuthorsRepository
    {
        public async Task<PagedResult<Author>> GetAllAsync(QueryRequest request, CancellationToken ct = default)
        {
            IGridifyQuery gridifyQuery = request.ToGridifyQuery();

            IQueryable<Author> query = context.Session.Query<Author>();
            IQueryable<Author> resultQuery = query.ApplyFilteringAndOrdering(gridifyQuery);

            return await resultQuery.ToPagedResultAsync(gridifyQuery, ct);
        }

        public async Task<Author?> GetAsync(AuthorId id, CancellationToken ct = default)
        {
            IQueryable<Author> query = context
                .Session
                .Query<Author>()
                .Where(a => a.IdValue == id.Value);

            return await query.FirstOrDefaultAsync(ct);
        }

        public async Task<long> CountAsync(CancellationToken ct = default)
        {
            IQueryable<Author> query = context.Session.Query<Author>();
            return await query.LongCountAsync(ct);
        }

        public async Task AddAsync(Author entity, CancellationToken ct = default)
        {
            await context.Session.SaveAsync(entity, ct);
        }

        public async Task AddOrUpdateAsync(Author entity, CancellationToken ct = default)
        {
            await context.Session.SaveOrUpdateAsync(entity, ct);
        }

        public async Task UpdateAsync(Author entity, CancellationToken ct = default)
        {
            await context.Session.UpdateAsync(entity, ct);
        }

        public async Task DeleteAsync(AuthorId id, CancellationToken ct = default)
        {
            await context.Session.DeleteAsync(id.Value, ct);
        }

        public async Task DeleteAsync(Author entity, CancellationToken ct = default)
        {
            await context.Session.DeleteAsync(entity, ct);
        }
    }
}
