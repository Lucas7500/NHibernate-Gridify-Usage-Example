using BookStore.Domain.Models;
using BookStore.Domain.Persistence.Common;

namespace BookStore.Domain.Persistence
{
    public interface IWriteableBooksRepository : IWriteableRepository<Book, int>
    {
        Task AddAuthorAsync(Author author, CancellationToken ct = default);
        Task UpdateAuthorAsync(Author author, CancellationToken ct = default);
        Task DeleteAuthorAsync(Author author, CancellationToken ct = default);
    }
}
