using BookStore.Application.DTOs.Books.Responses;
using BookStore.Application.UseCases.Books.Contracts;
using BookStore.Domain.Models.BookModel;
using BookStore.Domain.Persistence.Contracts;
using BookStore.Domain.ValueObjects;
using ErrorOr;

namespace BookStore.Application.UseCases.Books
{
    internal sealed class DeleteBookUseCase(IUnitOfWork unitOfWork) : IDeleteBookUseCase
    {
        public async Task<ErrorOr<DeleteBookResponse>> ExecuteAsync(BookId request, CancellationToken cancellationToken = default)
        {
            try
            {
                Book? book = await unitOfWork.BooksRepository.GetByIdAsync(request, cancellationToken);

                if (book is null)
                {
                    return Error.NotFound(description: "The book with the specified Id was not found.");
                }

                await unitOfWork.BooksRepository.DeleteAsync(request, cancellationToken);
                await unitOfWork.CommitAsync(cancellationToken);

                return new DeleteBookResponse($"Book with Id {request.Value} deleted successfully.");
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync(cancellationToken);
                return Error.Failure(description: $"An error occurred while deleting the book: {ex.Message}");
            }
        }
    }
}
