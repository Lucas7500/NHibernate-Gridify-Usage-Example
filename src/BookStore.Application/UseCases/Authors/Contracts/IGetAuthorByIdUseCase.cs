using BookStore.Application.DTOs.Authors.Responses;
using BookStore.Application.UseCases.Contracts;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects;

namespace BookStore.Application.UseCases.Authors.Contracts
{
    public interface IGetAuthorByIdUseCase : IUseCase<AuthorId, AuthorResponse>;
}
