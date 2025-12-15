using BookStore.Application.DTOs.Books.Requests;
using BookStore.Application.UseCases.Contracts;

namespace BookStore.Application.UseCases.Books.Contracts
{
    public interface IGetBooksCountUseCase : IUseCase<GetBooksCountRequest, long>;
}
