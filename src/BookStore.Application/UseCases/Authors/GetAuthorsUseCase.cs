using BookStore.Application.QueryServices.Contracts;
using BookStore.Application.UseCases.Authors.Contracts;

namespace BookStore.Application.UseCases.Authors
{
    internal sealed class GetAuthorsUseCase(IAuthorsQueryService authorsRepository) : IGetAuthorsUseCase
    {
    }
}
