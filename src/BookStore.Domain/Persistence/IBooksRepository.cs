using BookStore.Domain.Models;
using BookStore.Domain.Persistence.Common;

namespace BookStore.Domain.Persistence
{
    public interface IBooksRepository : IRepository<Book, int>;
}
