using BookStore.Application.DTOs.Authors.Responses;
using BookStore.Application.QueryServices.Contracts;
using BookStore.Domain.Models.AuthorModel;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects;
using BookStore.Infra.NHibernate;
using BookStore.Infra.Utils;
using NHibernate;
using NHibernate.Transform;

namespace BookStore.Infra.QueryServices.Authors
{
    internal sealed class AuthorsHqlQueryService(NHibernateContext context) : IAuthorsQueryService
    {
        public async Task<PagedResult<AuthorResponse>> GetAllAsync(QueryRequest request, CancellationToken ct = default)
        {
            // HQL does not support Gridify directly, so we're not implementing filtering or ordering
            IQuery query = HQL.GetAllQuery<Author>(context.Session);
            IQuery countQuery = HQL.CountQuery<Author>(context.Session);

            IQuery pagedQuery = query
                .SetFirstResult((request.Page - 1) * request.PageSize)
                .SetMaxResults(request.PageSize);

            int totalCount = await countQuery.UniqueResultAsync<int>(ct);

            IList<AuthorResponse> books = await pagedQuery
                .SetResultTransformer(Transformers.AliasToBean<AuthorResponse>())
                .ListAsync<AuthorResponse>(ct);

            return new PagedResult<AuthorResponse>(request.Page, request.PageSize, totalCount, books);
        }

        public async Task<AuthorResponse?> GetByIdAsync(AuthorId id, CancellationToken ct = default)
        {
            IQuery query = HQL.GetByIdQuery<Author, AuthorId, Guid>(context.Session, id);
            return await query
                .SetResultTransformer(Transformers.AliasToBean<AuthorResponse>())
                .UniqueResultAsync<AuthorResponse?>(ct);
        }

        public async Task<long> CountAsync(CancellationToken ct = default)
        {
            IQuery query = HQL.CountQuery<Author>(context.Session);
            return await query.UniqueResultAsync<long>(ct);
        }
    }
}
