using BookStore.Application.DTOs.Authors.Responses;
using BookStore.Application.QueryServices.Contracts;
using BookStore.Application.UseCases.Authors.Contracts;
using BookStore.Domain.ValueObjects;
using ErrorOr;

namespace BookStore.Application.UseCases.Authors
{
    internal sealed class GetAuthorByIdUseCase(IAuthorsQueryService authorsQueryService) : IGetAuthorByIdUseCase
    {
        public async Task<ErrorOr<AuthorResponse>> ExecuteAsync(AuthorId request, CancellationToken cancellationToken = default)
        {
            AuthorResponse? author = await authorsQueryService.GetByIdAsync(request, cancellationToken);

            if (author is null)
            {
                return Error.NotFound(description: $"Author with Id '{request}' was not found.");
            }

            return author;
        }
    }
}
