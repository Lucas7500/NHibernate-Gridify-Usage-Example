using BookStore.Application.DTOs.Authors.Responses;
using BookStore.Application.QueryServices.Contracts;
using BookStore.Domain.Models.AuthorModel;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects;
using BookStore.Infra.Extensions;
using BookStore.Infra.Mappers;
using BookStore.Infra.NHibernate;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace BookStore.Infra.QueryServices.Authors
{
    internal sealed class AuthorsCriteriaQueryService(NHibernateContext context) : IAuthorsQueryService
    {
        public async Task<PagedResult<AuthorResponse>> GetAllAsync(QueryRequest request, CancellationToken ct = default)
        {
            // Criteria does not support Gridify directly, so we're not implementing filtering or ordering
            ICriteria criteria = context.Session.CreateCriteria<Author>();
            
            return await criteria.ToPagedResultWithDtoAsync<AuthorResponse>(request.ToGridifyQuery(), ct);
        }

        public async Task<AuthorResponse?> GetByIdAsync(AuthorId id, CancellationToken ct = default)
        {
            ICriteria criteria = context.Session.CreateCriteria<Author>();

            return await criteria
                .Add(Restrictions.Eq(nameof(Author.IdValue), id.Value))
                .SetResultTransformer(Transformers.AliasToBean<AuthorResponse>())
                .UniqueResultAsync<AuthorResponse?>(ct);
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
