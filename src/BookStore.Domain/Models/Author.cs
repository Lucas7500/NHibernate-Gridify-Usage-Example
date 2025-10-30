using BookStore.Domain.Models.Base;

namespace BookStore.Domain.Models
{
    public class Author : Entity<Guid>
    {
        public Author(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
        
        public Author(Guid id, string name)
            : this(name)
        {
            Id = id;
        }

        // No argument constructor for ORM
        protected Author() { }
        
        public virtual string Name { get; protected set; } = string.Empty;
    }
}