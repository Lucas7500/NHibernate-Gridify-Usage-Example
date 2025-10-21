using BookStore.Domain.Models.Base;

namespace BookStore.Domain.Models
{
    public class Author
    {
        public Author(string name)
        {
            Id = default;
            Name = name;
        }
        
        public Author(int id, string name)
            : this(name)
        {
            Id = id;
        }

        // No argument constructor for ORM
        protected Author() { }
        
        public virtual int Id { get; protected set; }
        public virtual string Name { get; protected set; } = string.Empty;
    }
}