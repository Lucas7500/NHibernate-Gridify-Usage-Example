using BookStore.Domain.Models.Base;

namespace BookStore.Domain.Models
{
    public class Book : AggregateRoot<int>
    {
        public Book(string title, Author author, decimal price)
        {
            Id = default;
            Title = title;
            Author = author;
            Price = price;
            IsAvailable = true;
        }

        public Book(int id, string title, Author author, decimal price, bool isAvailable)
            : this(title, author, price)
        {
            Id = id;
        }

        // No argument constructor for ORM
        protected Book() 
        {
            Author = null!;
        }

        public virtual string Title { get; protected set; } = string.Empty;
        public virtual Author Author { get; protected set; }
        public virtual decimal Price { get; protected set; }
        public virtual bool IsAvailable { get; protected set; }

        public virtual void Activate()
        {
            CheckRule(IsAvailable, "Book is already available.");
            IsAvailable = true;
        }

        public virtual void Deactivate()
        {
            CheckRule(!IsAvailable, "Book is already unavailable.");
            IsAvailable = false;
        }
    }
}
