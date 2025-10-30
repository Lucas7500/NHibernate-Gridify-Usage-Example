using BookStore.Domain.Models;
using BookStore.Domain.Persistence.Common;

namespace BookStore.Domain.Persistence
{
    public interface IQueryableBooksRepository : IQueryableRepository<Book, int>
    {
        Task<IEnumerable<Book>> GetBooksWithAuthors(CancellationToken ct = default);
        Task<IEnumerable<Author>> GetAllAuthors(CancellationToken ct = default); 
        Task<Author?> GetAuthor(Guid id, CancellationToken ct = default); 
        
        Task<long> CountAuthors(CancellationToken ct = default);
    }
}
