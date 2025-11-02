using BookStore.Domain.Models.AuthorModel;
using BookStore.Domain.Persistence.Contracts.Authors;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects;
using BookStore.Infra.NHibernate;
using BookStore.Infra.Utils;
using NHibernate;

namespace BookStore.Infra.Repositories.Authors
{
    internal sealed class AuthorsRepositoryHQL(NHibernateContext context) : IQueryableAuthorsRepository
    {
        public async Task<PagedResult<Author>> GetAllAsync(QueryRequest request, CancellationToken ct = default)
        {
            // HQL does not support Gridify directly, so we're not implementing filtering or ordering
            IQuery query = HQL.GetAllQuery<Author>(context.Session);
            IQuery countQuery = HQL.CountQuery<Author>(context.Session);

            IQuery pagedQuery = query
                .SetFirstResult((request.Page - 1) * request.PageSize)
                .SetMaxResults(request.PageSize);

            int totalCount = await countQuery.UniqueResultAsync<int>(ct);
            IList<Author> books = await pagedQuery.ListAsync<Author>(ct);

            return new PagedResult<Author>(request.Page, request.PageSize, totalCount, books);
        }

        public async Task<Author?> GetAsync(AuthorId id, CancellationToken ct = default)
        {
            IQuery query = HQL.GetByIdQuery<Author, AuthorId, Guid>(context.Session, id);
            return await query.UniqueResultAsync<Author?>(ct);
        }

        public async Task<long> CountAsync(CancellationToken ct = default)
        {
            IQuery query = HQL.CountQuery<Author>(context.Session);
            return await query.UniqueResultAsync<long>(ct);
        }
    }
}
