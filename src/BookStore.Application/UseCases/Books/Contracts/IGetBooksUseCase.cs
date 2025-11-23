using BookStore.Application.DTOs.Books.Responses;
using BookStore.Application.UseCases.Contracts;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;

namespace BookStore.Application.UseCases.Books.Contracts
{
    public interface IGetBooksUseCase : IUseCase<QueryRequest, PagedResult<BookResponse>>;
}
