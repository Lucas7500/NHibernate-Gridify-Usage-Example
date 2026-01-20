using BookStore.Domain.Validation;

namespace BookStore.Domain.Models.BookModel.BusinessRules
{
    internal class BookTitleMustHaveDefinedLength : IBusinessRule
    {
        private readonly Book _book;

        public BookTitleMustHaveDefinedLength(Book book)
        {
            _book = book;
        }

        public string Description => "businessrule.book-title-has-invalid-length";

        public bool IsBroken()
        {
            const int maxLength = 200;
            
            if (string.IsNullOrWhiteSpace(_book.Title))
                return true;
            
            return _book.Title.Length > maxLength;
        }
    }
}
