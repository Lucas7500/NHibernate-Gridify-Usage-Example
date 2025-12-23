using BookStore.Domain.Models.AuthorModel.BusinessRules;
using BookStore.Domain.Models.Base;
using BookStore.Domain.Models.Base.BusinessRules;
using BookStore.Domain.Models.BookModel;
using BookStore.Domain.ValueObjects;

namespace BookStore.Domain.Models.AuthorModel
{
    public class Author : AggregateRoot<AuthorId, Guid>
    {
        public Author(string name) : base(AuthorId.NewId())
        {
            Name = name;

            CheckRule(new AuthorNameMustHaveDefinedLength(this));
        }

        public Author(AuthorId id, string name) : base(id)
        {
            Name = name;
        }

        // No args constructor for ORM
        protected Author() : base(){ }
        
        public virtual string Name { get; protected set; } = string.Empty;
        public virtual ICollection<Book> Books { get; protected set; } = [];

        public virtual void ChangeName(string name)
        {
            Name = name;

            CheckRule(new AuthorNameMustHaveDefinedLength(this));
        }

        protected override void SetId(Guid value)
        {
            var id = new AuthorId(value);
            CheckRule(new ProvidedIdCantBeEmpty<AuthorId, Guid>(id));
            Id = id;
        }
    }
}