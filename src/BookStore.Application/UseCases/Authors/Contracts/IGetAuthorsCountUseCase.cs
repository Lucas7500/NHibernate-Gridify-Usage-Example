using BookStore.Application.DTOs.Authors.Requests;
using BookStore.Application.UseCases.Contracts;

namespace BookStore.Application.UseCases.Authors.Contracts
{
    public interface IGetAuthorsCountUseCase : IUseCase<GetAuthorsCountRequest, long>;
}
