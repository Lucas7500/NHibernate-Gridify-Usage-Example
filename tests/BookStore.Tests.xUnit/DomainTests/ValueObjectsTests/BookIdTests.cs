using BookStore.Domain.Exceptions;
using BookStore.Domain.ValueObjects;
using BookStore.Domain.ValueObjects.Contracts;

namespace BookStore.Tests.xUnit.DomainTests.ValueObjectsTests
{
    public static class BookIdTests
    {
        private static readonly Faker _faker = new();

        public sealed class UsingStandardAssertions
        {
            [Fact]
            public void BookId_ImplementsIStronglyTypedIdInterface()
            {
                Type bookIdType = typeof(BookId);
                Type stronglyTypedIdInterfaceType = typeof(IStronglyTypedId<int>);

                Assert.True(stronglyTypedIdInterfaceType.IsAssignableFrom(bookIdType),
                    $"{bookIdType.Name} does not implement {stronglyTypedIdInterfaceType.Name} interface.");
            }

            [Fact]
            public void Empty_ShouldReturnBookIdWithIntegerDefaultValue()
            {
                BookId emptyBookId = BookId.Empty;

                Assert.Equal(default, emptyBookId.Value);
                Assert.False(emptyBookId.HasValue);
            }

            [Fact]
            public void Constructor_WithoutParameters_ShouldCreateEmptyBookId()
            {
                BookId bookId = new();

                Assert.Equal(default, bookId.Value);
                Assert.False(bookId.HasValue);
            }

            [Fact]
            public void Constructor_WithIntParameter_ShouldCreateBookIdWithGivenValue()
            {
                int integer = _faker.Random.Int(min: 1);
                BookId bookId = new(integer);

                Assert.Equal(integer, bookId.Value);
                Assert.True(bookId.HasValue);
            }

            [Fact]
            public void Constructor_WithIntegerDefaultValue_ShouldThrowInvalidIntegerIdException()
            {
                int emptyint = default;

                Assert.Throws<InvalidIntegerIdException>(() => new BookId(emptyint));
            }

            [Fact]
            public void Constructor_WithStringParameter_ShouldCreateBookIdWithGivenValue()
            {
                int integer = _faker.Random.Int(min: 1);
                string intStr = integer.ToString();
                BookId bookId = new(intStr);

                Assert.Equal(integer, bookId.Value);
                Assert.True(bookId.HasValue);
            }

            [Fact]
            public void Constructor_WithInvalidIntegerString_ShouldThrowInvalidIntegerIdException()
            {
                string invalidintStr = _faker.Random.Word();

                Assert.Throws<InvalidIntegerIdException>(() => new BookId(invalidintStr));
            }

            [Fact]
            public void ToString_ShouldReturnIntegerStringRepresentation()
            {
                int integer = _faker.Random.Int(min: 1);
                BookId bookId = new(integer);

                Assert.Equal(integer.ToString(), bookId.ToString());
            }

            [Fact]
            public void BookId_EqualityCheck_ShouldBeTrueForSameValues()
            {
                int integer = _faker.Random.Int(min: 1);

                BookId bookId1 = new(integer);
                BookId bookId2 = new(integer);

                Assert.Equal(bookId1, bookId2);
            }

            [Fact]
            public void BookId_EqualityCheck_ShouldBeFalseForDifferentValues()
            {
                int integer1 = _faker.Random.Int(min: 1);
                int integer2 = _faker.Random.Int(min: 1);

                if (integer1 == integer2)
                {
                    integer2 = integer1 + 1;
                }

                BookId bookId1 = new(integer1);
                BookId bookId2 = new(integer2);

                Assert.NotEqual(bookId1, bookId2);
            }

            [Fact]
            public void BookId_HashCode_ShouldBeSameForSameValues()
            {
                int integer = _faker.Random.Int(min: 1);

                BookId bookId1 = new(integer);
                BookId bookId2 = new(integer);

                Assert.Equal(bookId1.GetHashCode(), bookId2.GetHashCode());
            }

            [Fact]
            public void BookId_HashCode_ShouldBeDifferentForDifferentValues()
            {
                int integer1 = _faker.Random.Int(min: 1);
                int integer2 = _faker.Random.Int(min: 1);

                BookId bookId1 = new(integer1);
                BookId bookId2 = new(integer2);

                Assert.NotEqual(bookId1.GetHashCode(), bookId2.GetHashCode());
            }
        }

        public sealed class UsingFluentAssertions
        {
            [Fact]
            public void BookId_ShouldImplementIStronglyTypedIdInterface()
            {
                Type bookIdType = typeof(BookId);
                Type stronglyTypedIdInterfaceType = typeof(IStronglyTypedId<int>);

                bookIdType.Should().Implement(stronglyTypedIdInterfaceType,
                    $"{bookIdType.Name} should implement {stronglyTypedIdInterfaceType.Name} interface.");
            }

            [Fact]
            public void Empty_ShouldReturnBookIdWithIntegerDefaultValue()
            {
                BookId emptyBookId = BookId.Empty;

                emptyBookId.Value.Should().Be(default);
                emptyBookId.HasValue.Should().BeFalse();
            }

            [Fact]
            public void Constructor_WithoutParameters_ShouldCreateEmptyBookId()
            {
                BookId bookId = new();

                bookId.Value.Should().Be(default);
                bookId.HasValue.Should().BeFalse();
            }

            [Fact]
            public void Constructor_WithIntParameter_ShouldCreateBookIdWithGivenValue()
            {
                int integer = _faker.Random.Int(min: 1);
                BookId bookId = new(integer);

                bookId.Value.Should().Be(integer);
                bookId.HasValue.Should().BeTrue();
            }

            [Fact]
            public void Constructor_WithIntegerDefaultValue_ShouldThrowInvalidIntegerIdException()
            {
                int emptyint = default;
                Action act = () => new BookId(emptyint);
                act.Should().ThrowExactly<InvalidIntegerIdException>();
            }

            [Fact]
            public void Constructor_WithStringParameter_ShouldCreateBookIdWithGivenValue()
            {
                int integer = _faker.Random.Int(min: 1);
                string intStr = integer.ToString();
                BookId bookId = new(intStr);

                bookId.Value.Should().Be(integer);
                bookId.HasValue.Should().BeTrue();
            }

            [Fact]
            public void Constructor_WithInvalidIntegerString_ShouldThrowInvalidIntegerIdException()
            {
                string invalidIntStr = _faker.Random.Word();
                Action act = () => new BookId(invalidIntStr);
                act.Should().ThrowExactly<InvalidIntegerIdException>();
            }

            [Fact]
            public void ToString_ShouldReturnIntegerStringRepresentation()
            {
                int integer = _faker.Random.Int(min: 1);
                BookId bookId = new(integer);
                bookId.ToString().Should().Be(integer.ToString());
            }

            [Fact]
            public void BookId_EqualityCheck_ShouldBeTrueForSameValues()
            {
                int integer = _faker.Random.Int(min: 1);

                BookId bookId1 = new(integer);
                BookId bookId2 = new(integer);

                bookId1.Should().Be(bookId2);
            }

            [Fact]
            public void BookId_EqualityCheck_ShouldBeFalseForDifferentValues()
            {
                int integer1 = _faker.Random.Int(min: 1);
                int integer2 = _faker.Random.Int(min: 1);

                if (integer1 == integer2)
                {
                    integer2 = integer1 + 1;
                }

                BookId bookId1 = new(integer1);
                BookId bookId2 = new(integer2);

                bookId1.Should().NotBe(bookId2);
            }

            [Fact]
            public void BookId_HashCode_ShouldBeSameForSameValues()
            {
                int integer = _faker.Random.Int(min: 1);

                BookId bookId1 = new(integer);
                BookId bookId2 = new(integer);

                bookId1.GetHashCode().Should().Be(bookId2.GetHashCode());
            }

            [Fact]
            public void BookId_HashCode_ShouldBeDifferentForDifferentValues()
            {
                int integer1 = _faker.Random.Int(min: 1);
                int integer2 = _faker.Random.Int(min: 1);

                BookId bookId1 = new(integer1);
                BookId bookId2 = new(integer2);

                bookId1.GetHashCode().Should().NotBe(bookId2.GetHashCode());
            }
        }
    }
}
