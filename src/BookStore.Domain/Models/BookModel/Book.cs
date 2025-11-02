using BookStore.Domain.Models.AuthorModel;
using BookStore.Domain.Models.Base;
using BookStore.Domain.Models.Base.BusinessRules;
using BookStore.Domain.ValueObjects;

namespace BookStore.Domain.Models.BookModel
{
    public class Book : AggregateRoot<BookId, int>
    {
        public Book(string title, Author author, decimal price)
        {
            Title = title;
            Author = author;
            Price = price;
            IsAvailable = true;
        }

        public Book(BookId id, string title, Author author, decimal price, bool isAvailable)
            : base(id)
        {
            Title = title;
            Author = author;
            Price = price;
            IsAvailable = isAvailable;
        }

        // No args constructor for ORM
        protected Book() { }

        public virtual string Title { get; protected set; } = string.Empty;
        public virtual Author Author { get; protected set; } = null!;
        public virtual decimal Price { get; protected set; }
        public virtual bool IsAvailable { get; protected set; }

        protected override void SetId(int value)
        {
            var id = new BookId(value);

            CheckRule(new ProvidedIdCantBeEmpty<BookId, int>(id));

            Id = id;
        }
    }
}
