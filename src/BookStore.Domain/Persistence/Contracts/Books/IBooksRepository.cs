using BookStore.Domain.Models.BookModel;
using BookStore.Domain.Persistence.Contracts.Base;
using BookStore.Domain.ValueObjects;

namespace BookStore.Domain.Persistence.Contracts.Books
{
    public interface IBooksRepository : IRepository<Book, BookId, int>;
}
