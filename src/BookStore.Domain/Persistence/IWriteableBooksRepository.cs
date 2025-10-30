using BookStore.Domain.Models;
using BookStore.Domain.Persistence.Common;

namespace BookStore.Domain.Persistence
{
    public interface IWriteableBooksRepository : IWriteableRepository<Book, int>
    {
        Task AddAuthor(Author author, CancellationToken ct = default);
        Task UpdateAuthor(Author author, CancellationToken ct = default);
        Task DeleteAuthor(Author author, CancellationToken ct = default);
    }
}
