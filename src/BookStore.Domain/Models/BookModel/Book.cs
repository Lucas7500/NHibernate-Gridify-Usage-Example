using BookStore.Domain.Models.AuthorModel;
using BookStore.Domain.Models.Base;
using BookStore.Domain.Models.Base.BusinessRules;
using BookStore.Domain.Models.BookModel.BusinessRules;
using BookStore.Domain.ValueObjects;

namespace BookStore.Domain.Models.BookModel
{
    public class Book : AggregateRoot<BookId, int>
    {
        public Book(string title, decimal price, Author author)
        {
            Title = title;
            Price = price;
            IsAvailable = true;
            Author = author;

            CheckRule(new BookTitleMustHaveDefinedLength(this));
            CheckRule(new BookPriceMustBePositive(this));
            CheckRule(new BookMustHaveAnAuthor(this));
        }

        public Book(BookId id, string title, decimal price, bool isAvailable, Author author)
            : base(id)
        {
            Title = title;
            Price = price;
            IsAvailable = isAvailable;
            Author = author;

            CheckRule(new BookTitleMustHaveDefinedLength(this));
            CheckRule(new BookPriceMustBePositive(this));
            CheckRule(new BookMustHaveAnAuthor(this));
        }

        // No args constructor for ORM
        protected Book() { }

        public virtual string Title { get; protected set; } = string.Empty;
        public virtual decimal Price { get; protected set; }
        public virtual bool IsAvailable { get; protected set; }
        public virtual Author Author { get; protected set; } = null!;

        public virtual void ChangeTitle(string newTitle)
        {
            Title = newTitle;
            CheckRule(new BookTitleMustHaveDefinedLength(this));
        }

        public virtual void ChangePrice(decimal newPrice)
        {
            Price = newPrice;
            CheckRule(new BookPriceMustBePositive(this));
        }

        public virtual void MarkAsUnavailable()
        {
            IsAvailable = false;
        }
        
        public virtual void MarkAsAvailable()
        {
            IsAvailable = true;
        }

        public virtual void ChangeAuthor(Author newAuthor)
        {
            Author = newAuthor;
            CheckRule(new BookMustHaveAnAuthor(this));
        }

        protected override void SetId(int value)
        {
            BookId id = new(value);
            CheckRule(new ProvidedIdCantBeEmpty<BookId, int>(id));
            Id = id;
        }
    }
}
