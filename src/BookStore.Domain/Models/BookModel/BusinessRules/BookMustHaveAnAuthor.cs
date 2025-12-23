using BookStore.Domain.Validation;
using BookStore.Domain.ValueObjects;

namespace BookStore.Domain.Models.BookModel.BusinessRules
{
    internal class BookMustHaveAnAuthor : IBusinessRule
    {
        private readonly Book _book;
        
        public BookMustHaveAnAuthor(Book book)
        {
            _book = book;
        }
        
        public string Description => "businessrule.book-must-have-an-author";
        
        public bool IsBroken() => _book.Author is null || _book.Author.Id == AuthorId.Empty;
    }
}
