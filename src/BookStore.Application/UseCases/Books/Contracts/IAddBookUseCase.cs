using BookStore.Application.DTOs.Books.Requests;
using BookStore.Application.DTOs.Books.Responses;
using BookStore.Application.UseCases.Contracts;

namespace BookStore.Application.UseCases.Books.Contracts
{
    public interface IAddBookUseCase : IUseCase<AddBookRequest, AddBookResponse>;
}
