using BookStore.Application.DTOs.Books.Responses;
using BookStore.Application.QueryServices.Contracts;
using BookStore.Domain.Models.BookModel;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects;
using BookStore.Infra.Extensions;
using BookStore.Infra.Mappers;
using BookStore.Infra.NHibernate;
using NHibernate;
using NHibernate.Transform;

namespace BookStore.Infra.QueryServices.Books
{
    internal sealed class BooksQueryOverQueryService(NHibernateContext context) : IBooksQueryService
    {
        public async Task<PagedResult<BookOnlyResponse>> GetAllAsync(QueryRequest request, CancellationToken ct = default)
        {
            // QueryOver does not support Gridify directly, so we're not implementing filtering or ordering
            IQueryOver<Book, Book> query = context.Session.QueryOver<Book>();

            return await query.ToPagedResultWithDtoAsync<Book, BookOnlyResponse>(request.ToGridifyQuery(), ct);
        }

        public async Task<PagedResult<BookWithAuthorResponse>> GetAllWithAuthorsFetchedAsync(QueryRequest request, CancellationToken ct = default)
        {
            // QueryOver does not support Gridify directly, so we're not implementing filtering or ordering
            IQueryOver<Book, Book> query = context
                .Session
                .QueryOver<Book>()
                .Fetch(SelectMode.Fetch, b => b.Author);

            return await query.ToPagedResultWithDtoAsync<Book, BookWithAuthorResponse>(request.ToGridifyQuery(), ct);
        }

        public async Task<BookWithAuthorResponse?> GetByIdAsync(BookId id, CancellationToken ct = default)
        {
            IQueryOver<Book, Book> query = context.Session.QueryOver<Book>();

            return await query
                .Where(b => b.IdValue == id.Value)
                .TransformUsing(Transformers.AliasToBean<BookWithAuthorResponse>())
                .SingleOrDefaultAsync<BookWithAuthorResponse>(ct);
        }

        public async Task<long> CountAsync(CancellationToken ct = default)
        {
            IQueryOver<Book, Book> query = context.Session.QueryOver<Book>();

            return await query
                .ToRowCountQuery()
                .SingleOrDefaultAsync<long>(ct);
        }
    }
}
