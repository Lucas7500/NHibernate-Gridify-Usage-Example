using BookStore.Application.UseCases.Books.Contracts;
using BookStore.Domain.Persistence.Contracts.Books;

namespace BookStore.Application.UseCases.Books
{
    internal sealed class UpdateBookUseCase(IWriteableBooksRepository booksRepository) : IUpdateBookUseCase
    {
    }
}
