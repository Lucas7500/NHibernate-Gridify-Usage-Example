using BookStore.Domain.Validation;

namespace BookStore.Domain.Models.BookModel.BusinessRules
{
    internal class BookPriceMustBePositive : IBusinessRule
    {
        private readonly Book _book;

        public BookPriceMustBePositive(Book book)
        {
            _book = book;
        }

        public string Description => "businessrule.book-price-must-be-positive";
        
        public bool IsBroken() => _book.Price < 0;
    }
}
