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
using NHibernate.Transform;

namespace BookStore.Infra.QueryServices.Authors
{
    internal sealed class AuthorsQueryOverQueryService(NHibernateContext context) : IAuthorsQueryService
    {
        public async Task<PagedResult<AuthorResponse>> GetAllAsync(QueryRequest request, CancellationToken ct = default)
        {
            // QueryOver does not support Gridify directly, so we're not implementing filtering or ordering
            IQueryOver<Author, Author> query = context.Session.QueryOver<Author>();

            return await query.ToPagedResultWithDtoAsync<Author, AuthorResponse>(request.ToGridifyQuery(), ct);
        }

        public async Task<AuthorResponse?> GetByIdAsync(AuthorId id, CancellationToken ct = default)
        {
            IQueryOver<Author, Author> query = context.Session.QueryOver<Author>();

            return await query
                .Where(b => b.IdValue == id.Value)
                .TransformUsing(Transformers.AliasToBean<AuthorResponse>())
                .SingleOrDefaultAsync<AuthorResponse>(ct);
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
