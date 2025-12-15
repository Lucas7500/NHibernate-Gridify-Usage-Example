using BookStore.Application.DTOs.Authors.Requests;
using BookStore.Application.QueryServices.Contracts;
using BookStore.Application.UseCases.Authors.Contracts;
using ErrorOr;

namespace BookStore.Application.UseCases.Authors
{
    internal sealed class GetAuthorsCountUseCase(IAuthorsQueryService authorsQueryService) : IGetAuthorsCountUseCase
    {
        public async Task<ErrorOr<long>> ExecuteAsync(GetAuthorsCountRequest request, CancellationToken cancellationToken = default)
        {
            long authorsCount = await authorsQueryService.CountAsync(cancellationToken);

            if (authorsCount < 0)
            {
                return Error.Failure(description: "Failed to retrieve the authors count.");
            }

            return authorsCount;
        }
    }
}
