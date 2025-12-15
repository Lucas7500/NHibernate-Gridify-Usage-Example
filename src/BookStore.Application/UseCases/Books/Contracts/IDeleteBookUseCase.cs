using BookStore.Application.DTOs.Books.Responses;
using BookStore.Application.UseCases.Contracts;
using BookStore.Domain.ValueObjects;

namespace BookStore.Application.UseCases.Books.Contracts
{
    public interface IDeleteBookUseCase : IUseCase<BookId, DeleteBookResponse>;
}
