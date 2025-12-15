using BookStore.Application.DTOs.Authors.Responses;
using BookStore.Application.QueryServices.Contracts;
using BookStore.Application.UseCases.Authors.Contracts;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using ErrorOr;

namespace BookStore.Application.UseCases.Authors
{
    internal sealed class GetAuthorsUseCase(IAuthorsQueryService authorsQueryService) : IGetAuthorsUseCase
    {
        public async Task<ErrorOr<PagedResult<AuthorResponse>>> ExecuteAsync(QueryRequest request, CancellationToken cancellationToken = default)
        {
            return await authorsQueryService.GetAllAsync(request, cancellationToken);
        }
    }
}
