using BookStore.Application.UseCases.Authors.Contracts;
using BookStore.Domain.Persistence.Contracts.Authors;

namespace BookStore.Application.UseCases.Authors
{
    internal sealed class GetAuthorsCountUseCase(IQueryableAuthorsRepository authorsRepository) : IGetAuthorsCountUseCase
    {
    }
}
