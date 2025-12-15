using BookStore.Application.DTOs.Authors.Responses;
using BookStore.Application.UseCases.Contracts;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;

namespace BookStore.Application.UseCases.Authors.Contracts
{
    public interface IGetAuthorsUseCase : IUseCase<QueryRequest, PagedResult<AuthorResponse>>;
}
