using BookStore.Domain.Models.AuthorModel;
using BookStore.Domain.Persistence.Contracts.Authors;
using BookStore.Domain.ValueObjects;
using NHibernate;
using NHibernate.Linq;

namespace BookStore.Infra.Repositories.Authors
{
    internal sealed class AuthorsRepository(ISession session) : IAuthorsRepository
    {
        public async Task<Author?> GetByIdAsync(AuthorId id, CancellationToken ct = default)
        {
            IQueryable<Author> query = session
                .Query<Author>()
                .Where(a => a.IdValue == id.Value);

            return await query.FirstOrDefaultAsync(ct);
        }

        public async Task AddAsync(Author entity, CancellationToken ct = default)
        {
            await session.SaveAsync(entity, ct);
        }

        public async Task AddOrUpdateAsync(Author entity, CancellationToken ct = default)
        {
            await session.SaveOrUpdateAsync(entity, ct);
        }

        public async Task UpdateAsync(Author entity, CancellationToken ct = default)
        {
            await session.UpdateAsync(entity, ct);
        }

        public async Task DeleteAsync(AuthorId id, CancellationToken ct = default)
        {
            await session.DeleteAsync(id.Value, ct);
        }

        public async Task DeleteAsync(Author entity, CancellationToken ct = default)
        {
            await session.DeleteAsync(entity, ct);
        }
    }
}
