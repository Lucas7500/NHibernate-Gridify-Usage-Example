using BookStore.Domain.Persistence.Contracts.Authors;
using BookStore.Domain.Persistence.Contracts.Books;

namespace BookStore.Domain.Persistence.Contracts
{
    public interface IUnitOfWork
    {
        IBooksRepository BooksRepository { get; }
        IAuthorsRepository AuthorsRepository { get; }

        Task CommitAsync(CancellationToken ct = default);
        Task RollbackAsync(CancellationToken ct = default);
    }
}
