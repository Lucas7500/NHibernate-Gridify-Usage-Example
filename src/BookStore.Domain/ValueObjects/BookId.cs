using BookStore.Domain.Exceptions;
using BookStore.Domain.ValueObjects.Contracts;

namespace BookStore.Domain.ValueObjects
{
    public readonly record struct BookId : IStronglyTypedId<int>
    {
        public static BookId Empty => new();

        public BookId()
        {
            Value = default;
        }

        public BookId(int value)
        {
            InvalidIntegerIdException.ThrowIfEmpty(value);

            Value = value;
        }

        public BookId(string valueStr)
        {
            InvalidIntegerIdException.ThrowIfInvalidIntegerId(valueStr);

            Value = int.Parse(valueStr);
        }

        public int Value { get; }

        public bool HasValue => Value != default;

        public override string ToString() => Value.ToString();
    }
}
