using BookStore.Domain.Exceptions;
using BookStore.Domain.ValueObjects.Contracts;

namespace BookStore.Domain.ValueObjects
{
    public readonly record struct AuthorId : IStronglyTypedId<Guid>
    {
        public static AuthorId NewId() => new(Guid.NewGuid());
        public static AuthorId Empty => new();

        public AuthorId()
        {
            Value = Guid.Empty;
        }

        public AuthorId(Guid value)
        {
            InvalidGuidException.ThrowIfEmpty(value);

            Value = value;
        }
        
        public AuthorId(string valueStr)
        {
            InvalidGuidException.ThrowIfInvalidGuid(valueStr);

            Value = Guid.Parse(valueStr);
        }

        public Guid Value { get; }

        public bool HasValue => Value != Guid.Empty;

        public override string ToString() => Value.ToString();
    }
}
