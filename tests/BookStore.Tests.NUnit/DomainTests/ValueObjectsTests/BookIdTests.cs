using BookStore.Domain.Exceptions;
using BookStore.Domain.ValueObjects;
using BookStore.Domain.ValueObjects.Contracts;

namespace BookStore.Tests.NUnit.DomainTests.ValueObjectsTests
{
    [TestFixture]
    public sealed class BookIdTests
    {
        public Faker _faker;

        [SetUp]
        public void Setup()
        {
            _faker = new Faker();
        }

        [Test]
        public void BookId_ImplementsIStronglyTypedIdInterface()
        {
            Type bookIdType = typeof(BookId);
            Type stronglyTypedIdInterfaceType = typeof(IStronglyTypedId<int>);

            Assert.That(stronglyTypedIdInterfaceType.IsAssignableFrom(bookIdType), Is.True,
                message: $"{bookIdType.Name} does not implement {stronglyTypedIdInterfaceType.Name} interface.");
        }

        [Test]
        public void Empty_ShouldReturnBookIdWithIntegerDefaultValue()
        {
            BookId emptyBookId = BookId.Empty;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(emptyBookId.Value, Is.Default);
                Assert.That(emptyBookId.HasValue, Is.False);
            }
        }

        [Test]
        public void Constructor_WithoutParameters_ShouldCreateEmptyBookId()
        {
            BookId bookId = new();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(bookId.Value, Is.Default);
                Assert.That(bookId.HasValue, Is.False);
            }
        }

        [Test]
        public void Constructor_WithIntParameter_ShouldCreateBookIdWithGivenValue()
        {
            int integer = _faker.Random.Int(min: 1);
            BookId bookId = new(integer);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(bookId.Value, Is.EqualTo(integer));
                Assert.That(bookId.HasValue, Is.True);
            }
        }

        [Test]
        public void Constructor_WithIntegerDefaultValue_ShouldThrowInvalidIntegerIdException()
        {
            int emptyint = default;

            Assert.Throws<InvalidIntegerIdException>(() => new BookId(emptyint));
        }

        [Test]
        public void Constructor_WithStringParameter_ShouldCreateBookIdWithGivenValue()
        {
            int integer = _faker.Random.Int(min: 1);
            string intStr = integer.ToString();
            BookId bookId = new(intStr);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(bookId.Value, Is.EqualTo(integer));
                Assert.That(bookId.HasValue, Is.True);
            }
        }

        [Test]
        public void Constructor_WithInvalidIntegerString_ShouldThrowInvalidIntegerIdException()
        {
            string invalidintStr = _faker.Random.Word();

            Assert.Throws<InvalidIntegerIdException>(() => new BookId(invalidintStr));
        }

        [Test]
        public void ToString_ShouldReturnIntegerStringRepresentation()
        {
            int integer = _faker.Random.Int(min: 1);
            BookId bookId = new(integer);

            Assert.That(bookId.ToString(), Is.EqualTo(integer.ToString()));
        }

        [Test]
        public void BookId_EqualityCheck_ShouldBeTrueForSameValues()
        {
            int integer = _faker.Random.Int(min: 1);

            BookId bookId1 = new(integer);
            BookId bookId2 = new(integer);

            Assert.That(bookId1, Is.EqualTo(bookId2));
        }

        [Test]
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

            Assert.That(bookId1, Is.Not.EqualTo(bookId2));
        }

        [Test]
        public void BookId_HashCode_ShouldBeSameForSameValues()
        {
            int integer = _faker.Random.Int(min: 1);

            BookId bookId1 = new(integer);
            BookId bookId2 = new(integer);

            Assert.That(bookId1.GetHashCode(), Is.EqualTo(bookId2.GetHashCode()));
        }

        [Test]
        public void BookId_HashCode_ShouldBeDifferentForDifferentValues()
        {
            int integer1 = _faker.Random.Int(min: 1);
            int integer2 = _faker.Random.Int(min: 1);

            BookId bookId1 = new(integer1);
            BookId bookId2 = new(integer2);

            Assert.That(bookId1.GetHashCode(), Is.Not.EqualTo(bookId2.GetHashCode()));
        }
    }
}
