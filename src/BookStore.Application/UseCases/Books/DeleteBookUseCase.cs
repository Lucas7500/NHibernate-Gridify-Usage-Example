using BookStore.Application.DTOs.Books.Responses;
using BookStore.Application.UseCases.Books.Contracts;
using BookStore.Domain.Models.BookModel;
using BookStore.Domain.Persistence.Contracts.Books;
using BookStore.Domain.ValueObjects;
using ErrorOr;

namespace BookStore.Application.UseCases.Books
{
    internal sealed class DeleteBookUseCase(IBooksRepository booksRepository) : IDeleteBookUseCase
    {
        public async Task<ErrorOr<DeleteBookResponse>> ExecuteAsync(BookId request, CancellationToken cancellationToken = default)
        {
            Book? book = await booksRepository.GetByIdAsync(request, cancellationToken);

            if (book is null)
            {
                return Error.NotFound(description: "The book with the specified ID was not found.");
            }

            await booksRepository.DeleteAsync(request, cancellationToken);

            return new DeleteBookResponse($"Book with ID {request.Value} deleted successfully.");
        }
    }
}
